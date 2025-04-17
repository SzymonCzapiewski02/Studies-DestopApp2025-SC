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
    public class NowaZamowienieViewModel:JedenViewModel<Zamowienia>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaZamowienieViewModel()
            :base("Zamowienia")
        {
            item = new Zamowienia();
            Messenger.Default.Register<KlienciForAllView>(this, getWybranyKlient);
        }
        #endregion
        #region Command
        private BaseCommand _ShowKlienci;
        public ICommand ShowKlientci
        {
            get
            {
                if (_ShowKlienci == null)
                    _ShowKlienci = new BaseCommand(() => showKlientci());
                return _ShowKlienci;
            }
        }
        private void showKlientci()
        {
            Messenger.Default.Send("KlienciAll");
        }
        #endregion
        #region Pola
        public int? IdKlienta
        {
            get
            {
                return item.IdKlienta;
            } 
            set
            {
                item.IdKlienta = value;
                OnPropertyChanged(() => IdKlienta);
            }
        }
        public DateTime? DataZamowienia
        {
            get
            {
                return item.DataZamowienia;
            }
            set
            {
                item.DataZamowienia = value;
                OnPropertyChanged(() => DataZamowienia);
            }
        }
        public string Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public decimal? CenaCalkowita
        {
            get
            {
                return item.CenaCalkowita;
            }
            set
            {
                item.CenaCalkowita = value;
                OnPropertyChanged(() => CenaCalkowita);
            }
        }
        public string NumerReferencyjny
        {
            get
            {
                return item.NumerReferencyjny;
            }
            set
            {
                item.NumerReferencyjny = value;
                OnPropertyChanged(() => NumerReferencyjny);
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
        public string KanalZamowienia
        {
            get
            {
                return item.KanalZamowienia;
            }
            set
            {
                item.KanalZamowienia = value;
                OnPropertyChanged(() => KanalZamowienia);
            }
        }
        public string Komentarze
        {
            get
            {
                return item.Komentarze;
            }
            set
            {
                item.Komentarze = value;
                OnPropertyChanged(() => Komentarze);
            }
        }
        public string MetodaPlatnosci
        {
            get
            {
                return item.MetodaPlatnosci;
            }
            set
            {
                item.MetodaPlatnosci = value;
                OnPropertyChanged(() => MetodaPlatnosci);
            }
        }
        public string KlientNazwa { get; set; }
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
                if (name == "IdKlienta")
                    komunikat = ComboBoxValidator.SprawdzCzyWybrany(this.IdKlienta);
                if (name == "DataZamowienia")
                    komunikat = DataValidator.SprawdzDate(this.DataZamowienia);
                if (name == "Status")
                    komunikat = StatusValidator.StatusZamowienia(this.Status);
                if (name == "CenaCalkowita")
                    komunikat = CenaValidator.CenaCzyPoprawna(this.CenaCalkowita);
                if (name == "NumerReferencyjny")
                    komunikat = LiczbaValidator.SprawdzCzy5Cyfr(this.NumerReferencyjny);
                if (name == "IdPracownika")
                    komunikat = ComboBoxValidator.SprawdzCzyWybrany(this.IdPracownika);
                if (name == "KanalZamowienia")
                    komunikat = KanalValidator.SprawdzPierwszaLiteraIKod(this.KanalZamowienia);
                if (name == "Komentarze")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Komentarze);
                if (name == "MetodaPlatnosci")
                    komunikat = PlatnoscValidator.PlatnoscZamowienia(this.MetodaPlatnosci);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["IdKlienta"] == null && this["DataZamowienia"] == null && this["Status"] == null 
                && this["CenaCalkowita"] == null && this["NumerReferencyjny"] == null && this["IdPracownika"] == null
                && this["KanalZamowienia"] == null && this["Komentarze"] == null && this["MetodaPlatnosci"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> KlientKeyAndValue
        {
            get
            {
                return new KlienciB(SklepEntities).GetKLientKeyAndValueItemsImie();
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
        private void getWybranyKlient(KlienciForAllView klient)
        {
            IdKlienta = klient.IdKlienta;
            KlientNazwa = klient.Imie + " " + klient.Nazwisko;
        }
        public override void Save()
        {
            SklepEntities.Zamowienia.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
