using MVVMFirma.Models.Entities;
using System.Linq;

namespace MVVMFirma.Models.BusinessLogic
{
    public class AdminiB : DatabaseClass
    {
        public AdminiB(SklepSamochodowyEntities db) : base(db) { }

        public IQueryable<Admin> LogowanieAdmina(string login, string password)
        {
            return db.Admin.Where(admin => admin.Login == login && admin.Haslo == password);
        }
    }
}
