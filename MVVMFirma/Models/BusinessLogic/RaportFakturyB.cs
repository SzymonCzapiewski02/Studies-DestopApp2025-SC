using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class RaportFakturB : DatabaseClass
    {
        #region Konstruktor
        public RaportFakturB(SklepSamochodowyEntities db) : base(db) { }
        #endregion

        #region Funkcja Biznesowa
        public IQueryable<RaportFakturP> GenerowanieRaportuFaktur(bool? czyOplacone,
        DateTime? dataOd, DateTime? dataDo, decimal? minimalnaKwota, decimal? maksymalnaKwota,
        int? idKategorii, int? idMagazynu, decimal? vat)
        {
            var wynik =
                from faktury in db.Faktury
                where
                    (czyOplacone == null || faktury.StatusFaktury == czyOplacone) &&
                    (dataOd == null || faktury.Zamowienia.DataZamowienia >= dataOd) &&
                    (dataDo == null || faktury.Zamowienia.DataZamowienia <= dataDo) &&
                    (minimalnaKwota == null || faktury.KwotaCalkowita >= minimalnaKwota) &&
                    (maksymalnaKwota == null || faktury.KwotaCalkowita <= maksymalnaKwota) &&
                    (idKategorii == null || faktury.Zamowienia.SzczegolyZamowienia
                        .Any(sz => sz.Produkty.IdKategori == idKategorii)) &&
                    (idMagazynu == null || faktury.Zamowienia.SzczegolyZamowienia
                        .Any(sz => sz.Produkty.ZapasyMagazynowe
                        .Any(zm => zm.IdMagazynu == idMagazynu)))
                select new RaportFakturP
                {
                    IdFaktury = faktury.IdFaktury,
                    DataWystawienia = faktury.DataWystawienia,
                    StatusPlatnosci = faktury.StatusFaktury,
                    KwotaCalkowita = faktury.KwotaCalkowita,
                    NazwaProduktu = faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Produkty.Nazwa).FirstOrDefault(),
                    Ilosc = faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Ilosc).FirstOrDefault(),
                    CenaJednostkowa = faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Produkty.Cena).FirstOrDefault(),
                    KategoriaProduktu = faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Produkty.Kategorie.Nazwa).FirstOrDefault(),
                    Magazyn = faktury.Zamowienia.SzczegolyZamowienia
                    .SelectMany(sz => sz.Produkty.ZapasyMagazynowe)
                    .Select(zm => zm.Magazyny.Nazwa).FirstOrDefault(),
                    CenaNetto = faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Produkty.Cena).FirstOrDefault(),
                    CenaBrutto = Math.Round((double)(faktury.Zamowienia.SzczegolyZamowienia
                    .Select(sz => sz.Produkty.Cena).FirstOrDefault() * (1 + (vat ?? 0) / 100)), 2)
                };
            return wynik;
        }


        public IQueryable<RaportKlientow> GenerujRaportKlientow(int? idKlienta, DateTime? dataOd, DateTime? dataDo)
        {
            var wynik =
                from faktury in db.Faktury
                where
                    (idKlienta == null || faktury.Zamowienia.Klienci.IdKlienta == idKlienta) &&
                    (dataOd == null || faktury.Zamowienia.DataZamowienia >= dataOd) &&
                    (dataDo == null || faktury.Zamowienia.DataZamowienia <= dataDo)
                group faktury by new { faktury.Zamowienia.Klienci.IdKlienta,
                    faktury.Zamowienia.Klienci.Imie, faktury.Zamowienia.Klienci.Nazwisko } into grupa
                select new RaportKlientow
                {
                    IdKlienta = grupa.Key.IdKlienta,
                    Imie = grupa.Key.Imie,
                    Nazwisko = grupa.Key.Nazwisko,
                    SumaKwot = grupa.Sum(f => f.KwotaCalkowita)
                };

            return wynik;
        }
        #endregion

        #region Pola
        public class RaportFakturP
        {
            public int IdFaktury { get; set; }
            public DateTime? DataWystawienia { get; set; }
            public bool? StatusPlatnosci { get; set; }
            public decimal? KwotaCalkowita { get; set; }
            public string NazwaProduktu { get; set; }
            public int? Ilosc { get; set; }
            public decimal? CenaJednostkowa { get; set; }
            public string KategoriaProduktu { get; set; }
            public string Magazyn { get; set; }
            public decimal? CenaNetto { get; set; }
            public double? CenaBrutto { get; set; }
        }
        public class RaportKlientow
        {
            public int IdKlienta {  get; set; }
            public string Imie {  get; set; }
            public string Nazwisko { get; set; }
            public decimal? SumaKwot {  get; set; }
        }
        #endregion
    }
}
