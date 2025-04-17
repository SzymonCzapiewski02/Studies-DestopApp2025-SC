using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities.Validatory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NoweFakturyViewModel:JedenViewModel<Faktury>, IDataErrorInfo
    {
        #region Konstruktor
        public NoweFakturyViewModel()
            : base("Faktura")
        {
            item = new Faktury();
            Messenger.Default.Register<ZamowienieForAllView>(this, getWybranyZamowienia);
        }
        #endregion
        #region Command
        private BaseCommand _ShowZamowienia;
        public ICommand ShowZamowienia
        {
            get
            {
                if (_ShowZamowienia == null)
                    _ShowZamowienia = new BaseCommand(() => showZamowienia());
                return _ShowZamowienia;
            }
        }
        private void showZamowienia()
        {
            Messenger.Default.Send("ZamowieniaAll");
        }
        #endregion
        #region Pola
        public int? IdZamowienia
        {
            get
            {
                return item.IdZamowienia;
            }
            set
            {
                item.IdZamowienia = value;
                OnPropertyChanged(() => IdZamowienia);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.DataWystawienia;
            }
            set
            {
                item.DataWystawienia = value;
                OnPropertyChanged(() => DataWystawienia);
            }
        }
        public Decimal? KwotaCalkowita
        {
            get
            {
                return item.KwotaCalkowita;
            }
            set
            {
                item.KwotaCalkowita = value;
                OnPropertyChanged(() => KwotaCalkowita);
            }
        }
        public DateTime? TerminPlatnosci
        {
            get
            {
                return item.TerminPlatnosci;
            }
            set
            {
                item.TerminPlatnosci = value;
                OnPropertyChanged(() => TerminPlatnosci);
            }
        }
        public string NumerFaktury
        {
            get
            {
                return item.NumerFaktury;
            }
            set
            {
                item.NumerFaktury = value;
                OnPropertyChanged(() => NumerFaktury);
            }
        }
        public int? IdPracownika
        {
            get
            {
                return item.IdPracownika;
            }
            set
            {
                item.IdPracownika = value;
                OnPropertyChanged(() => IdPracownika);
            }
        }
        public string TypFaktury
        {
            get
            {
                return item.TypFaktury;
            }
            set
            {
                item.TypFaktury = value;
                OnPropertyChanged(() => TypFaktury);
            }
        }
        public string Waluta
        {
            get
            {
                return item.Waluta;
            }
            set
            {
                item.Waluta = value;
                OnPropertyChanged(() => Waluta);
            }
        }
        public string ZamowieniaNazwa { get; set; }
        #endregion
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string komunikat = null;
                if (name == "DataWystawienia")
                    komunikat = DataValidator.SprawdzDate(this.DataWystawienia);
                if (name == "TerminPlatnosci")
                    komunikat = DataValidator.SprawdzDate(this.TerminPlatnosci);
                if (name == "KwotaCalkowita")
                    komunikat = CenaValidator.CenaCzyPoprawna(this.KwotaCalkowita);
                if (name == "IdPracownika")
                    komunikat = ComboBoxValidator.SprawdzCzyWybrany(this.IdPracownika);
                if (name == "Waluta")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Waluta);
                if (name == "TypFaktury")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.TypFaktury);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["DataWystawienia"] == null && this["TerminPlatnosci"] == null && this["KwotaCalkowita"] == null
                && this["CenaCalkowita"] == null && this["TypFaktury"] == null && this["IdPracownika"] == null
                && this["Waluta"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> ZamowienieKeyAndValue
        {
            get
            {
                return new ZamowieniaB(SklepEntities).GetZamowienieKeyAndValueItemsNazwa();
            }
        }
        public IQueryable<KeyAndValue> PracownikKeyAndValue
        {
            get
            {
                return new PracownikB(SklepEntities).GetPracownikKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyZamowienia(ZamowienieForAllView zamowienie)
        {
            IdZamowienia = zamowienie.IdZamowienia;
            ZamowieniaNazwa = zamowienie.NumerReferencyjny;
        }
        public override void Save()
        {
            SklepEntities.Faktury.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
