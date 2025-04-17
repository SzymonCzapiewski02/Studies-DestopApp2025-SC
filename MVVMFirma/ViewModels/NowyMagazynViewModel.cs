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
    public class NowyMagazynViewModel:JedenViewModel<Magazyny>, IDataErrorInfo
    {
        #region Construktor
        public NowyMagazynViewModel()
            :base("Magazyn")
        {
            item = new Magazyny();
        }
        #endregion
        #region Properties
        public string Lokalizacja
        {
            get
            {
                return item.Lokalizacja;
            }
            set
            {
                item.Lokalizacja = value;
                OnPropertyChanged(() => Lokalizacja);
            }
        }
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
                if (name == "Lokalizacja")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Lokalizacja);
                if (name == "Nazwa")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Nazwa);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Nazwa"] == null && this["Lokalizacja"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            SklepEntities.Magazyny.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
