using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class KlienciB:DatabaseClass
    {
        #region Konstruktor
        public KlienciB(SklepSamochodowyEntities db)
            :base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetKLientKeyAndValueItemsImie()
        {
            return
                (
                    from klient in db.Klienci
                    select new KeyAndValue
                    {
                        Key = klient.IdKlienta,
                        Value = klient.Imie + " " + klient.Nazwisko + " " + klient.Email
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
