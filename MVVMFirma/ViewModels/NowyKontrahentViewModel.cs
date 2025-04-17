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
    public class NowyKontrahentViewModel:JedenViewModel<Kontrahenci>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyKontrahentViewModel()
            : base("Kontrahent")
        {
            item = new Kontrahenci();
            Messenger.Default.Register<Adres>(this, getWybranyAdres);
        }
        #endregion
        #region Command
        private BaseCommand _ShowAdresy;
        public ICommand ShowAdresy
        {
            get
            {
                if (_ShowAdresy == null)
                    _ShowAdresy = new BaseCommand(() => showAdresy());
                return _ShowAdresy;
            }
        }
        private void showAdresy()
        {
            Messenger.Default.Send("AdresyAll");
        }
        #endregion
        #region Pola
        public string AdresUlica { get; set; }
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }
            set
            {
                item.Nazwa = value;
                OnPropertyChanged(() => Nazwa);
            }
        }
        public string Kontakt
        {
            get
            {
                return item.Kontakt;
            }
            set
            {
                item.Kontakt = value;
                OnPropertyChanged(() => Kontakt);
            }
        }
        public string Email
        {
            get
            {
                return item.Email;
            }
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }
        public string Telefon
        {
            get
            {
                return item.Telefon;
            }
            set
            {
                item.Telefon = value;
                OnPropertyChanged(() => Telefon);
            }
        }
        public int? IdAdresu
        {
            get
            {
                return item.IdAdresu;
            }
            set
            {
                item.IdAdresu = value;
                OnPropertyChanged(() => IdAdresu);
            }
        }
        public string NIP
        {
            get
            {
                return item.NIP;
            }
            set
            {
                item.NIP = value;
                OnPropertyChanged(() => NIP);
            }
        }
        public string Regon
        {
            get
            {
                return item.Regon;
            }
            set
            {
                item.Regon = value;
                OnPropertyChanged(() => Regon);
            }
        }
        public string TypKontrahenta
        {
            get
            {
                return item.TypKontrahenta;
            }
            set
            {
                item.TypKontrahenta = value;
                OnPropertyChanged(() => TypKontrahenta);
            }
        }
        public DateTime? DataUtworzenia
        {
            get
            {
                return item.DataUtworzenia;
            }
            set
            {
                item.DataUtworzenia = value;
                OnPropertyChanged(() => DataUtworzenia);
            }
        }
        public string StatusKontrahenta
        {
            get
            {
                return item.StatusKontrahenta;
            }
            set
            {
                item.StatusKontrahenta = value;
                OnPropertyChanged(() => StatusKontrahenta);
            }
        }
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
                if (name == "DataUtworzenia")
                    komunikat = DataValidator.SprawdzDate(this.DataUtworzenia);
                if (name == "Telefon")
                    komunikat = TelefonValidator.CzyDobryNumer(this.Telefon);
                if (name == "NIP")
                    komunikat = LiczbaValidator.SprawdzCzy5Cyfr(this.NIP);
                if (name == "Nazwa")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Nazwa);
                if (name == "Kontakt")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Kontakt);
                if (name == "Email")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Email);
                if (name == "TypKontrahenta")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.TypKontrahenta);
                if (name == "StatusKontrahenta")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.TypKontrahenta);
                if (name == "Regon")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Regon);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Nazwa"] == null && this["Email"] == null && this["Kontakt"] == null
                && this["TypKontrahenta"] == null && this["DataUtworzenia"] == null && this["StatusKontrahenta"] == null
                && this["Regon"] == null && this["NIP"] == null && this["Telefon"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> AdresyKeyAndValue
        {
            get
            {
                return new AdresyB(SklepEntities).GetAdesyKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyAdres(Adres adres)
        {
            IdAdresu = adres.IdAdresu;
            AdresUlica = adres.Ulica + " " + adres.KodPocztowy;
        }
        public override void Save()
        {
            SklepEntities.Kontrahenci.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
