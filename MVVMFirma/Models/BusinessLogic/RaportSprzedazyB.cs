using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVVMFirma.Models.BusinessLogic.RaportSprzedazyB;
using System.Windows.Documents;
using System.Xml.Linq;

namespace MVVMFirma.Models.BusinessLogic
{
    public class RaportSprzedazyB : DatabaseClass
    {
        #region Konstruktor
        public RaportSprzedazyB(SklepSamochodowyEntities db) : base(db) { }
        #endregion

        #region Funkcja Biznesowa
        public IQueryable<RaportSprzedazyP>GenerowanieRaportu(int? IdKlienta,
        DateTime? dataOd, DateTime? dataDo,
        int? IdProduktu, decimal? minimalnaCena, int? IdKategorii, decimal? Vat, decimal? Rabat, string statusZamowienia)
        {
            var wynik = 
                from szczegoly in db.SzczegolyZamowienia
                where
                    (IdKlienta == null || szczegoly.Zamowienia.IdKlienta == IdKlienta) &&
                    (dataOd == null || szczegoly.Zamowienia.DataZamowienia >= dataOd) &&
                    (dataDo == null || szczegoly.Zamowienia.DataZamowienia <= dataDo) &&
                    (IdProduktu == null || szczegoly.IdProduktu == IdProduktu) &&
                    (minimalnaCena == null || szczegoly.Ilosc * szczegoly.Produkty.Cena >= minimalnaCena) &&
                    (IdKategorii == null || szczegoly.Produkty.IdKategori == IdKategorii) &&
                    (Vat == null || szczegoly.Ilosc * szczegoly.Produkty.Cena * 0.23m >= Vat) &&
                    (Rabat == null || (szczegoly.Ilosc >= 10 ? (szczegoly.Ilosc * szczegoly.Produkty.Cena * 0.1m) : 0) >= Rabat) &&
                    (string.IsNullOrEmpty(statusZamowienia) || szczegoly.Zamowienia.Status == statusZamowienia)
                select new RaportSprzedazyP
                {
                    IdZamowienia = szczegoly.Zamowienia.IdZamowienia,
                    DataZamowienia = szczegoly.Zamowienia.DataZamowienia,
                    NazwaProduktu = szczegoly.Produkty.Nazwa,
                    NumerCzesci = szczegoly.Produkty.NumerCzesci,
                    Kategoria = szczegoly.Produkty.Kategorie.Nazwa,
                    Ilosc = szczegoly.Ilosc,
                    CenaJednoskowa = szczegoly.Produkty.Cena,
                    CenaCalkowita = szczegoly.Ilosc * szczegoly.Produkty.Cena,
                    Klient = szczegoly.Zamowienia.Klienci.Imie + " " + szczegoly.Zamowienia.Klienci.Nazwisko,
                    CenaPoRabat = Math.Round(
                            (double)((szczegoly.Ilosc * szczegoly.Produkty.Cena -
                            (szczegoly.Ilosc >= 10 ? (szczegoly.Ilosc * szczegoly.Produkty.Cena * 0.1m) : 0)) 
                            * (1 - (Rabat ?? 0) / 100)),2),
                    CenaBrutto = Math.Round((double)(szczegoly.Ilosc * szczegoly.Produkty.Cena * (1 + (Vat ?? 0) / 100) -
                            ((szczegoly.Ilosc * szczegoly.Produkty.Cena * (1 + (Vat ?? 0) / 100)) * ((Rabat ?? 0) / 100))), 2),
                    StatusZamowienia = szczegoly.Zamowienia.Status
                };
            return wynik;
        }

        #endregion

        #region Pola
        public class RaportSprzedazyP
        {
            public int IdZamowienia { get; set; }
            public DateTime? DataZamowienia { set; get; }
            public string NazwaProduktu { get; set; }
            public string NumerCzesci { get; set; }
            public string Kategoria { get; set; }
            public int? Ilosc { get; set; }
            public decimal? CenaJednoskowa { get; set; }
            public decimal? CenaCalkowita { get; set; }
            public string Klient { get; set; }
            public double? CenaBrutto {  get; set; }
            public double? CenaPoRabat { get; set; }
            public string StatusZamowienia { get; set; }
        }
        #endregion
    }
}
