using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.Validatory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class NowyPracownikViewModel : JedenViewModel<Pracownicy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyPracownikViewModel()
            :base("Pracownik")
        {
            item = new Pracownicy();
        }
        #endregion
        #region Properties
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
        public string Stanowisko
        {
            get
            {
                return item.Stanowisko;
            }
            set
            {
                item.Stanowisko = value;
                OnPropertyChanged(() => Stanowisko);
            }
        }
        public DateTime? DataZatrudnienia
        {
            get
            {
                return item.DataZatrudnienia;
            }
            set
            {
                item.DataZatrudnienia = value;
                OnPropertyChanged(() => DataZatrudnienia);
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
                if (name == "Stanowisko")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Stanowisko);
                if (name == "DataZatrudnienia")
                    komunikat = DataValidator.SprawdzDate(this.DataZatrudnienia);
                if (name == "Telefon")
                    komunikat = TelefonValidator.CzyDobryNumer(this.Telefon);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Imie"] == null && this["DataZatrudnienia"] == null && this["Telefon"] == null
                && this["Email"] == null && this["Nazwisko"] == null && this["Stanowisko"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            SklepEntities.Pracownicy.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
