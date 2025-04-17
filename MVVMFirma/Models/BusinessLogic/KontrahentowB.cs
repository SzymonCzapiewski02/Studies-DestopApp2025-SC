using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class KontrahentowB:DatabaseClass
    {
        #region Konstruktor
        public KontrahentowB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetKontrahenciKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Kontrahenci
                    select new KeyAndValue
                    {
                        Key = k.IdKontrahenta,
                        Value = k.Nazwa + " " + k.Kontakt
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
