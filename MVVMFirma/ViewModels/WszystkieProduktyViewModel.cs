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
    public class WszystkieProduktyViewModel: WszystkieViewModel<ProduktyForAllView>
    {
        #region Construktor
        public WszystkieProduktyViewModel() 
            :base()
        {
            base.DisplayName = "Produkty";
        }
        #endregion
        #region Properties
        private ProduktyForAllView _WybranyProdukt;
        public ProduktyForAllView WybranyProdukt
        {
            get
            {
                return _WybranyProdukt;
            }

            set
            {
                _WybranyProdukt = value;
                Messenger.Default.Send(_WybranyProdukt);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Nazwa", "Kategorie", "Cena", "Dostepnosc" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Nazwa")
                List = new ObservableCollection<ProduktyForAllView>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Kategorie")
                List = new ObservableCollection<ProduktyForAllView>(List.OrderBy(item => item.NazwaKategori));
            if (SortField == "Cena")
                List = new ObservableCollection<ProduktyForAllView>(List.OrderBy(item => item.Cena));
            if (SortField == "Dostepnosc")
                List = new ObservableCollection<ProduktyForAllView>(List.OrderBy(item => item.Dostepny));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Nazwa", "Kategorie", "Cena" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<ProduktyForAllView>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
            if (FindField == "Kategorie")
                List = new ObservableCollection<ProduktyForAllView>(List.Where(item => item.NazwaKategori != null && item.NazwaKategori.StartsWith(FindTextBox)));
            if (FindField == "Cena")
                List = new ObservableCollection<ProduktyForAllView>(List.Where(item => item.Cena != null && item.Cena.ToString().StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ProduktyForAllView>(
                    from Produkty in sklepEntities.Produkty
                    select new ProduktyForAllView
                    {
                        IdProduktu = Produkty.IdProduktu,
                        Nazwa = Produkty.Nazwa,
                        NazwaKategori = Produkty.Kategorie.Nazwa,
                        Producent = Produkty.Producent,
                        NumerCzesci = Produkty.NumerCzesci,
                        Cena = Produkty.Cena,
                        Opis = Produkty.Opis,
                        Dostepny = Produkty.Dostepny
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Produktu.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Produktu", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(7);
                        table.WidthPercentage = 100;

                        table.AddCell("Nazwa");
                        table.AddCell("Nazwa Kategorii");
                        table.AddCell("Producent");
                        table.AddCell("Numer Czesci");
                        table.AddCell("Cena");
                        table.AddCell("Opis");
                        table.AddCell("Dostepny");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.NazwaKategori ?? "-");
                            table.AddCell(f.Producent ?? "-");
                            table.AddCell(f.NumerCzesci ?? "-");
                            table.AddCell(f.Cena?.ToString() ?? "-");
                            table.AddCell(f.Opis ?? "-");
                            table.AddCell(f.Dostepny?.ToString() ?? "-");
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
