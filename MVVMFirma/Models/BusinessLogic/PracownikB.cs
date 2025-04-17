using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class PracownikB:DatabaseClass
    {
        #region Konstruktor
        public PracownikB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetPracownikKeyAndValueItemsNazwa()
        {
            return
                (
                    from k in db.Pracownicy
                    select new KeyAndValue
                    {
                        Key = k.IdPracownika,
                        Value = k.Imie + " " + k.Nazwisko
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
