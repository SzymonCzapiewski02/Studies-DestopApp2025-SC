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
    public class NowyAdresViewModel:JedenViewModel<Adres>, IDataErrorInfo
    {
        #region Construktor
        public NowyAdresViewModel()
            : base("Adres")
        {
            item = new Adres();
        }
        #endregion
        #region Properties
        public string Ulica
        {
            get
            {
                return item.Ulica;
            }
            set
            {
                item.Ulica = value;
                OnPropertyChanged(()=> Ulica);
            }
        }
        public string Miasto
        {
            get
            {
                return item.Miasto;
            }
            set
            {
                item.Miasto = value;
                OnPropertyChanged(() => Miasto);
            }
        }
        public string KodPocztowy
        {
            get
            {
                return item.KodPocztowy;
            }
            set
            {
                item.KodPocztowy = value;
                OnPropertyChanged(() => KodPocztowy);
            }
        }
        public string Kraj
        {
            get
            {
                return item.Kraj;
            }
            set
            {
                item.Kraj = value;
                OnPropertyChanged(() => Kraj);
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
                if (name == "Ulica")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Ulica);
                if (name == "Miasto")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Miasto);
                if (name == "Kraj")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Kraj);
                if (name == "KodPocztowy")
                    komunikat = LiczbaValidator.SprawdzCzy5Cyfr(this.KodPocztowy);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Ulica"] == null && this["Miasto"] == null && this["Kraj"] == null && this["KodPocztowy"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            SklepEntities.Adres.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
