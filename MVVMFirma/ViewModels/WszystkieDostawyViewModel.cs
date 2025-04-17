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
    public class WszystkieDostawyViewModel: WszystkieViewModel<DostawyForAllView>
    {
        #region Construktor
        public WszystkieDostawyViewModel()
            :base()
        {
            base.DisplayName = "Dostawy";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Kontrahenta", "Data Dostawy", "Status" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Kontrahenta")
                List = new ObservableCollection<DostawyForAllView>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Data Dostawy")
                List = new ObservableCollection<DostawyForAllView>(List.OrderBy(item => item.DataDostawy));
            if (SortField == "Status")
                List = new ObservableCollection<DostawyForAllView>(List.OrderBy(item => item.Status));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Kontrahenta", "Status" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Kontrahenta")
                List = new ObservableCollection<DostawyForAllView>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
            if (FindField == "Status")
                List = new ObservableCollection<DostawyForAllView>(List.Where(item => item.Status != null && item.Status.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<DostawyForAllView>(
                    from Dostawy in sklepEntities.Dostawy
                    select new DostawyForAllView
                    {
                        IdDostawy = Dostawy.IdDostawy,
                        Nazwa = Dostawy.Kontrahenci.Nazwa,
                        Email = Dostawy.Kontrahenci.Email,
                        Kontakt = Dostawy.Kontrahenci.Kontakt,
                        DataDostawy = Dostawy.DataDostawy,
                        Status = Dostawy.Status
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Dostawy.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Dostaw", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(5);
                        table.WidthPercentage = 100;

                        table.AddCell("Nazwa");
                        table.AddCell("Email");
                        table.AddCell("Kontakt");
                        table.AddCell("DataDostawy");
                        table.AddCell("Status");

                        foreach (var dostawy in List)
                        {
                            table.AddCell(dostawy.Nazwa ?? "-");
                            table.AddCell(dostawy.Email ?? "-");
                            table.AddCell(dostawy.Kontakt ?? "-");
                            table.AddCell(dostawy.DataDostawy?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(dostawy.Status ?? "-");
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
