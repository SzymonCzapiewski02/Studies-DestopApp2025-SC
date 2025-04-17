using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class ZamowieniaB:DatabaseClass
    {
        #region Konstruktor
        public ZamowieniaB(SklepSamochodowyEntities db)
            : base(db)
        {

        }
        #endregion
        #region Funkcja biznesowa
        public IQueryable<KeyAndValue> GetZamowienieKeyAndValueItemsNazwa()
        {
            return
                (
                    from Z in db.Zamowienia
                    select new KeyAndValue
                    {
                        Key = Z.IdZamowienia,
                        Value = Z.Klienci.Email + " " + Z.DataZamowienia
                    }
                ).ToList().AsQueryable();
        }
        public IQueryable<KeyAndValue> GetZamowienieKeyAndValueItemsStatus()
        {
            return
                (
                    from Z in db.Zamowienia
                    select new KeyAndValue
                    {
                        Key = Z.IdZamowienia,
                        Value = Z.Status
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
