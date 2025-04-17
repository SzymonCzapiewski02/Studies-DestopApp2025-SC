using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.Validatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class NowyAdminViewModel: JedenViewModel<Admin>
    {
        #region Construktor
        public NowyAdminViewModel()
            : base("Admin")
        {
            item = new Admin();
        }
        #endregion
        #region Properties
        public string Login
        {
            get
            {
                return item.Login;
            }
            set
            {
                item.Login = value;
                OnPropertyChanged(() => Login);
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
                if (name == "Login")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Login);
                if (name == "Haslo")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Haslo);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Login"] == null && this["Haslo"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            SklepEntities.Admin.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
