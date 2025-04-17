using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Views;
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
    public class WszystkieSzczegolyZamowieniaViewModel: WszystkieViewModel<SzczegolyZamowieniaForAllView>
    {
        #region Construktor
        public WszystkieSzczegolyZamowieniaViewModel() 
            :base()
        {
            base.DisplayName = "Szczegoly Zamowienia";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Ilość", "Produkty", "Data Zamowienie" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Ilość")
                List = new ObservableCollection<SzczegolyZamowieniaForAllView>(List.OrderBy(item => item.Ilosc));
            if (SortField == "Produkty")
                List = new ObservableCollection<SzczegolyZamowieniaForAllView>(List.OrderBy(item => item.Producent));
            if (SortField == "Data Zamowienie")
                List = new ObservableCollection<SzczegolyZamowieniaForAllView>(List.OrderBy(item => item.DataZamowienia));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Ilość" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Ilość")
                List = new ObservableCollection<SzczegolyZamowieniaForAllView>(List.Where(item => item.Ilosc != null && item.Ilosc.ToString().StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<SzczegolyZamowieniaForAllView>(
                    from Szczegoly in sklepEntities.SzczegolyZamowienia
                    select new SzczegolyZamowieniaForAllView
                    {
                        IdSzczegolu = Szczegoly.IdSzczegolu,
                        DataZamowienia = Szczegoly.Zamowienia.DataZamowienia,
                        Nazwa = Szczegoly.Produkty.Nazwa,
                        Producent = Szczegoly.Produkty.Producent,
                        Ilosc = Szczegoly.Ilosc,
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "SzcegolyZamownienia.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Szczegoly Zamownienia", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(4);
                        table.WidthPercentage = 100;

                        table.AddCell("DataZamowienia");
                        table.AddCell("Nazwa");
                        table.AddCell("Producent");
                        table.AddCell("Ilosc");

                        foreach (var f in List)
                        {
                            table.AddCell(f.DataZamowienia?.ToString("yyyy-MM-dd") ?? "-");
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
