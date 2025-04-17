using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class MagazynyB:DatabaseClass
    {
        #region Konstruktor
        public MagazynyB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetMagazynyKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Magazyny
                    select new KeyAndValue
                    {
                        Key = k.IdMagazynu,
                        Value = k.Nazwa + " " + k.Lokalizacja
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
