using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class AdresyB:DatabaseClass
    {
            #region Konstruktor
            public AdresyB(SklepSamochodowyEntities db)
                : base(db)
            {

            }
            #endregion
            #region Funkcja biznesowa
            public IQueryable<KeyAndValue> GetAdesyKeyAndValueItemsNazwa()
            {
                return
                    (
                        from k in db.Adres
                        select new KeyAndValue
                        {
                            Key = k.IdAdresu,
                            Value = k.Kraj + " " + k.Miasto + " " + k.Ulica
                        }
                    ).ToList().AsQueryable();
            }
            #endregion
        }
}
