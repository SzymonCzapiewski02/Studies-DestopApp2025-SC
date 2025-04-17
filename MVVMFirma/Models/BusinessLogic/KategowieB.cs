using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class KategowieB:DatabaseClass
    {
        #region Konstruktor
        public KategowieB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetKategorieKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Kategorie
                    select new KeyAndValue
                    {
                        Key = k.IdKategorii,
                        Value = k.Nazwa
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
