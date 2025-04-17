using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMFirma.ViewModels
{
    public class WszystkieZamowieniaViewModel: WszystkieViewModel<ZamowienieForAllView>
    {
        #region Construktor
        public WszystkieZamowieniaViewModel()
            :base()
        {
            base.DisplayName = "Zamowienia";
        }
        #endregion
        #region Properties
        private ZamowienieForAllView _WybranyZamowienia;
        public ZamowienieForAllView WybranyZamowienia
        {
            get
            {
                return _WybranyZamowienia;
            }

            set
            {
                _WybranyZamowienia = value;
                Messenger.Default.Send(_WybranyZamowienia);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Status", "Numer Referecyjny", "Metoda Płatnosci", "Kanał zamowienia" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Status")
                List = new ObservableCollection<ZamowienieForAllView>(List.OrderBy(item => item.Status));
            if (SortField == "Numer Referecyjny")
                List = new ObservableCollection<ZamowienieForAllView>(List.OrderBy(item => item.NumerReferencyjny));
            if (SortField == "Metoda Płatnosci")
                List = new ObservableCollection<ZamowienieForAllView>(List.OrderBy(item => item.MetodaPlatnosci));
            if (SortField == "Kanał zamowienia")
                List = new ObservableCollection<ZamowienieForAllView>(List.OrderBy(item => item.KanalZamowienia));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Status", "Metoda Płatnosci" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Status")
                List = new ObservableCollection<ZamowienieForAllView>(List.Where(item => item.Status != null && item.Status.StartsWith(FindTextBox)));
            if (FindField == "Metoda Płatnosci")
                List = new ObservableCollection<ZamowienieForAllView>(List.Where(item => item.MetodaPlatnosci != null && item.MetodaPlatnosci.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ZamowienieForAllView>(
                    from Zamowienia in sklepEntities.Zamowienia
                    select new ZamowienieForAllView
                    {
                        IdZamowienia=Zamowienia.IdZamowienia,
                        KlientaImie=Zamowienia.Klienci.Imie,
                        KlientaNazwisko=Zamowienia.Klienci.Nazwisko,
                        DataZamowienia=Zamowienia.DataZamowienia,
                        Status=Zamowienia.Status,
                        CenaCalkowita=Zamowienia.CenaCalkowita,
                        Zamkniete = Zamowienia.Zamkniete,
                        NumerReferencyjny = Zamowienia.NumerReferencyjny,
                        PracownikImie = Zamowienia.Pracownicy.Imie,
                        PracownikNazwisko = Zamowienia.Pracownicy.Nazwisko,
                        KanalZamowienia = Zamowienia.KanalZamowienia,
                        Komentarz = Zamowienia.Komentarze,
                        MetodaPlatnosci = Zamowienia.MetodaPlatnosci
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Zamowienia.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Zamowienia", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(12);
                        table.WidthPercentage = 100;

                        table.AddCell("Klienta Imie");
                        table.AddCell("Klienta Nazwisko");
                        table.AddCell("Data Zamowienia");
                        table.AddCell("Status");
                        table.AddCell("Cena Calkowita");
                        table.AddCell("Zamkniete");
                        table.AddCell("Numer Referencyjny");
                        table.AddCell("Pracownik Imie");
                        table.AddCell("Pracownik Nazwisko");
                        table.AddCell("Kanal Zamowienia");
                        table.AddCell("Komentarz");
                        table.AddCell("Metoda Platnosci");

                        foreach (var f in List)
                        {
                            table.AddCell(f.KlientaImie ?? "-");
                            table.AddCell(f.KlientaNazwisko ?? "-");
                            table.AddCell(f.DataZamowienia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.Status ?? "-");
                            table.AddCell(f.CenaCalkowita?.ToString() ?? "-");
                            table.AddCell(f.Zamkniete?.ToString() ?? "-");
                            table.AddCell(f.NumerReferencyjny ?? "-");
                            table.AddCell(f.PracownikImie ?? "-");
                            table.AddCell(f.PracownikNazwisko ?? "-");
                            table.AddCell(f.KanalZamowienia ?? "-");
                            table.AddCell(f.Komentarz ?? "-");
                            table.AddCell(f.MetodaPlatnosci ?? "-");
                        }

                        document.Add(table);
                    }
                    else
                    {
                        var noDataFont = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.ITALIC);
                        var noDataParagraph = new iTextSharp.text.Paragraph("Brak danych do wyświetlenia.", noDataFont)
                        {
                            Alignment = iTextSharp.text.Element.ALIGN_CENTER
                        };
                        document.Add(noDataParagraph);
                    }

                    document.Close();
                }
                MessageBox.Show("PDF został pomyślnie wygenerowany: " + filePath, "Sukces", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas generowania PDF: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
