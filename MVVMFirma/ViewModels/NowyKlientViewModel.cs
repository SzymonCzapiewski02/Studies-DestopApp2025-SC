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
    public class NowyKlientViewModel:JedenViewModel<Klienci>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyKlientViewModel()
            : base("Klient")
        {
            item = new Klienci();
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
        public string AdresUlica {  get; set; }
        public string Imie
        {
            get
            {
                return item.Imie;
            }
            set
            {
                item.Imie = value;
                OnPropertyChanged(() => Imie);
            }
        }
        public string Nazwisko
        {
            get
            {
                return item.Nazwisko;
            }
            set
            {
                item.Nazwisko = value;
                OnPropertyChanged(() => Nazwisko);
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
        public DateTime? DataRejestracji
        {
            get
            {
                return item.DataRejestracji;
            }
            set
            {
                item.DataRejestracji = value;
                OnPropertyChanged(() => DataRejestracji);
            }
        }
        public string Haslo
        {
            get
            {
                return item.Haslo;
            }
            set
            {
                item.Haslo = value;
                OnPropertyChanged(() => Haslo);
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
                if (name == "Imie")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Imie);
                if (name == "Nazwisko")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Nazwisko);
                if (name == "Email")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Email);
                if (name == "DataRejestracji")
                    komunikat = DataValidator.SprawdzDate(this.DataRejestracji);
                if (name == "Telefon")
                    komunikat = TelefonValidator.CzyDobryNumer(this.Telefon);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Imie"] == null && this["DataRejestracji"] == null && this["Telefon"] == null
                && this["Email"] == null && this["Nazwisko"] == null)
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
            SklepEntities.Klienci.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
