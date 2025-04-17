using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class ProduktyB:DatabaseClass
    {
        #region Konstruktor
        public ProduktyB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetProduktyKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Produkty
                    select new KeyAndValue
                    {
                        Key = k.IdProduktu,
                        Value = k.Nazwa + " " + k.NumerCzesci
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
