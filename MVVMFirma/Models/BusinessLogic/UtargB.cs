using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class UtargB : DatabaseClass
    {
        #region Konstruktor
        public UtargB(SklepSamochodowyEntities db) : base(db)
        {
        }
        #endregion

        #region Funkcje biznesowe
        public decimal? UtargTowaruOkresu(int idProduktu, DateTime dataOd, DateTime dataDo)
        {
            return
                (
                    from szczegoly in db.SzczegolyZamowienia
                    join zamowienie in db.Zamowienia
                        on szczegoly.IdZamowienia equals zamowienie.IdZamowienia
                    where
                        szczegoly.IdProduktu == idProduktu &&
                        zamowienie.DataZamowienia >= dataOd &&
                        zamowienie.DataZamowienia <= dataDo
                    select szczegoly.Ilosc * szczegoly.Produkty.Cena
                ).Sum();
        }
        #endregion
    }
}
