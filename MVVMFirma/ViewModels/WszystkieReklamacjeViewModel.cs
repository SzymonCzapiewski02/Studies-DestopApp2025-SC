using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMFirma.ViewModels
{
    public class WszystkieReklamacjeViewModel: WszystkieViewModel<ReklamacjaForAllView>
    {
        #region Construktor
        public WszystkieReklamacjeViewModel()
            : base()
        {
            base.DisplayName = "Reklamacje";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Klient", "Data Zamowienia", "Status" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Klient")
                List = new ObservableCollection<ReklamacjaForAllView>(List.OrderBy(item => item.Imie).OrderBy(item => item.Nazwisko));
            if (SortField == "Data Zamowienia")
                List = new ObservableCollection<ReklamacjaForAllView>(List.OrderBy(item => item.DataZamowienia));
            if (SortField == "Status")
                List = new ObservableCollection<ReklamacjaForAllView>(List.OrderBy(item => item.Status));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Klient", "Status" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Klient")
                List = new ObservableCollection<ReklamacjaForAllView>(List.Where(item => (item.Imie != null && item.Imie.StartsWith(FindTextBox)) || (item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox))));
            if (FindField == "Status")
                List = new ObservableCollection<ReklamacjaForAllView>(List.Where(item => item.Status != null && item.Status.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ReklamacjaForAllView>(
                    from Reklamacje in sklepEntities.Reklamacje
                    select new ReklamacjaForAllView
                    {
                        IdReklamacji = Reklamacje.IdReklamacji,
                        Imie = Reklamacje.Klienci.Imie,
                        Nazwisko = Reklamacje.Klienci.Nazwisko,
                        Email = Reklamacje.Klienci.Email,
                        DataZamowienia = Reklamacje.Zamowienia.DataZamowienia,
                        Opis = Reklamacje.Opis,
                        Status = Reklamacje.Status
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Reklamacja.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Reklamacji", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(6);
                        table.WidthPercentage = 100;

                        table.AddCell("Imie");
                        table.AddCell("Nazwisko");
                        table.AddCell("Email");
                        table.AddCell("Data Zamowienia");
                        table.AddCell("Opis");
                        table.AddCell("Status");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Imie ?? "-");
                            table.AddCell(f.Nazwisko ?? "-");
                            table.AddCell(f.Email ?? "-");
                            table.AddCell(f.DataZamowienia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.Opis ?? "-");
                            table.AddCell(f.Status ?? "-");
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
