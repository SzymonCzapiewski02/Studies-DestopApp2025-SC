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
    public class NowaKategoriaViewModel: JedenViewModel<Kategorie>, IDataErrorInfo
    {
        #region Construktor
        public NowaKategoriaViewModel()
            :base("Kategorie")
        {
            item = new Kategorie();
        }
        #endregion

        #region Properties
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }
            set
            {
                item.Nazwa = value;
                OnPropertyChanged(()=>  Nazwa);
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
                if (name == "Nazwa")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Nazwa);
                if (name == "Opis")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Opis);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Nazwa"] == null && this["Opis"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            SklepEntities.Kategorie.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
