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
    public class WszystkieHistorieLogowaniaViewModel: WszystkieViewModel<HistorieLogowaniaForAllView>
    {
        #region Construktor
        public WszystkieHistorieLogowaniaViewModel()
            : base()
        {
            base.DisplayName = "Historia Logowania";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Klient", "Data Logowania", "AdresIP" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Klient")
                List = new ObservableCollection<HistorieLogowaniaForAllView>(List.OrderBy(item => item.Imie).OrderBy(item => item.Nazwisko));
            if (SortField == "AdresIP")
                List = new ObservableCollection<HistorieLogowaniaForAllView>(List.OrderBy(item => item.AdresIP));
            if (SortField == "Data Logowania")
                List = new ObservableCollection<HistorieLogowaniaForAllView>(List.OrderBy(item => item.DataLogowania));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Klient", "AdresIP" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Klient")
                List = new ObservableCollection<HistorieLogowaniaForAllView>(List.Where(item => (item.Imie != null && item.Imie.StartsWith(FindTextBox)) || (item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox))));
            if (FindField == "AdresIP")
                List = new ObservableCollection<HistorieLogowaniaForAllView>(List.Where(item => item.AdresIP != null && item.AdresIP.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers 
        public override void Load()
        {
            List = new ObservableCollection<HistorieLogowaniaForAllView>(
                    from HistoriaLogowania in sklepEntities.HistoriaLogowania
                    select new HistorieLogowaniaForAllView
                    {
                        IdLogowania = HistoriaLogowania.IdLogowania,
                        Imie = HistoriaLogowania.Klienci.Imie,
                        Nazwisko = HistoriaLogowania.Klienci.Nazwisko,
                        Email = HistoriaLogowania.Klienci.Email,
                        DataLogowania = HistoriaLogowania.DataLogowania,
                        AdresIP = HistoriaLogowania.AdresIP
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "HistoriaLogowania.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Historia Logowania", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(5);
                        table.WidthPercentage = 100;

                        table.AddCell("Imie");
                        table.AddCell("Nazwisko");
                        table.AddCell("Email");
                        table.AddCell("Data logowania");
                        table.AddCell("AdresIP");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Imie ?? "-");
                            table.AddCell(f.Nazwisko ?? "-");
                            table.AddCell(f.Email ?? "-");
                            table.AddCell(f.DataLogowania?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.AdresIP ?? "-");
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
