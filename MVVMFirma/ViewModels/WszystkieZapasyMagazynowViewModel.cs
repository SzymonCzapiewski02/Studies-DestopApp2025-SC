using GalaSoft.MvvmLight.Messaging;
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
    public class WszystkieZapasyMagazynowViewModel: WszystkieViewModel<ZapasyMagazynoweForAllView>
    {
        #region Helpers
        public WszystkieZapasyMagazynowViewModel()
            :base()
        {
            base.DisplayName = "Zapasy Magazynow";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "ilosc" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "ilosc")
                List = new ObservableCollection<ZapasyMagazynoweForAllView>(List.OrderBy(item => item.Ilosc));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "ilosc" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "ilosc")
                List = new ObservableCollection<ZapasyMagazynoweForAllView>(List.Where(item => item.Ilosc != null && item.Ilosc.ToString().StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ZapasyMagazynoweForAllView>(
                    from Zapasy in sklepEntities.ZapasyMagazynowe
                    select new ZapasyMagazynoweForAllView
                    {
                        IdZapadowMagazynowe = Zapasy.IdZapasowMagazynowe,
                        Lokalizacja = Zapasy.Magazyny.Lokalizacja,
                        Nazwa = Zapasy.Produkty.Nazwa,
                        Producent = Zapasy.Produkty.Producent,
                        Ilosc = Zapasy.Ilosc
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "ZapasyMagazynowe.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Zapasów Magazynowe", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(4);
                        table.WidthPercentage = 100;

                        table.AddCell("Lokalizacja");
                        table.AddCell("Nazwa");
                        table.AddCell("Producent");
                        table.AddCell("Ilosc");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Lokalizacja ?? "-");
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.Producent ?? "-");
                            table.AddCell(f.Ilosc?.ToString() ?? "-");
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
