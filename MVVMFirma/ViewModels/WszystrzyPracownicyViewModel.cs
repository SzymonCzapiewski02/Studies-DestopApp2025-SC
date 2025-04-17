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
using System.Windows.Documents;

namespace MVVMFirma.ViewModels
{
    public class WszystrzyPracownicyViewModel : WszystkieViewModel<Pracownicy>
    {
        #region Construktor
        public WszystrzyPracownicyViewModel()
        {
            base.DisplayName = "Pracownicy";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "imie", "nazwisko", "stanowisko", "imie i nazwisko" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "imie")
                List = new ObservableCollection<Pracownicy>(List.OrderBy(item => item.Imie));
            if (SortField == "nazwisko")
                List = new ObservableCollection<Pracownicy>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "stanowisko")
                List = new ObservableCollection<Pracownicy>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imie i nazwisko")
                List = new ObservableCollection<Pracownicy>(List.OrderBy(item => item.Imie).OrderBy(item => item.Nazwisko));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "imie", "nazwisko", "stanowisko" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "imie")
                List = new ObservableCollection<Pracownicy>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "nazwisko")
                List = new ObservableCollection<Pracownicy>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "stanowisko")
                List = new ObservableCollection<Pracownicy>(List.Where(item => item.Stanowisko != null && item.Stanowisko.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Pracownicy>(
                    sklepEntities.Pracownicy.ToList()
                );
        }
        public override void pdf()
        {
            string filePath = "Pracownicy.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Pracowników", titleFont)
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
                        table.AddCell("Telefon");
                        table.AddCell("Stanowisko");
                        table.AddCell("Data Zatrudnienia");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Imie ?? "-");
                            table.AddCell(f.Nazwisko ?? "-");
                            table.AddCell(f.Email ?? "-");
                            table.AddCell(f.Telefon ?? "-");
                            table.AddCell(f.Stanowisko ?? "-");
                            table.AddCell(f.DataZatrudnienia?.ToString("yyyy-MM-dd") ?? "-");
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
