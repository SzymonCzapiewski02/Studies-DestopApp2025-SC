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
    public class NowaReklamacjaViewModel:JedenViewModel<Reklamacje>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaReklamacjaViewModel()
            : base("Reklamacja")
        {
            item = new Reklamacje();
            Messenger.Default.Register<ZamowienieForAllView>(this, getWybranyZamowienia);
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
        public DateTime? DataReklamacji
        {
            get
            {
                return item.DataReklamacji;
            }
            set
            {
                item.DataReklamacji = value;
                OnPropertyChanged(() => DataReklamacji);
            }
        }
        public string Opis
        {
            get
            {
                return item.Opis;
            }
            set
            {
                item.Opis = value;
                OnPropertyChanged(() => Opis);
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
        public string KlientNazwa { get; set; }
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
                if (name == "IdKlienta")
                    komunikat = ComboBoxValidator.SprawdzCzyWybrany(this.IdKlienta);
                if (name == "DataReklamacji")
                    komunikat = DataValidator.SprawdzDate(this.DataReklamacji);
                if (name == "Opis")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Opis);
                if (name == "Status")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Status);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["IdKlienta"] == null && this["DataZamowienia"] == null && this["Status"] == null && this["Opis"] == null)
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
        public IQueryable<KeyAndValue> ZamowieniaKeyAndValue
        {
            get
            {
                return new ZamowieniaB(SklepEntities).GetZamowienieKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyKlient(KlienciForAllView klient)
        {
            IdKlienta = klient.IdKlienta;
            KlientNazwa = klient.Imie + " " + klient.Nazwisko;
        }
        private void getWybranyZamowienia(ZamowienieForAllView zamowienie)
        {
            IdZamowienia = zamowienie.IdZamowienia;
            ZamowieniaNazwa = zamowienie.NumerReferencyjny;
        }
        public override void Save()
        {
            SklepEntities.Reklamacje.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
