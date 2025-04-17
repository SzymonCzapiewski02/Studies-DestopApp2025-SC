using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class DostawyB:DatabaseClass
    {
        #region Konstruktor
        public DostawyB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetDostawyKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Dostawy
                    select new KeyAndValue
                    {
                        Key = k.IdDostawy,
                        Value = k.DataDostawy + " " + k.Kontrahenci.Nazwa
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
