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
    public class WszystkieMagazywnyViewModel:WszystkieViewModel<Magazyny>
    {
        #region Construktor
        public WszystkieMagazywnyViewModel() 
            :base()
        {
            base.DisplayName = "Magazyny";
        }
        #endregion
        #region Properties
        private Magazyny _WybranyMagazyn;
        public Magazyny WybranyMagazyn
        {
            get
            {
                return _WybranyMagazyn;
            }

            set
            {
                _WybranyMagazyn = value;
                Messenger.Default.Send(_WybranyMagazyn);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Lokalizacja", "Nazwa" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Lokalizacja")
                List = new ObservableCollection<Magazyny>(List.OrderBy(item => item.Lokalizacja));
            if (SortField == "Nazwa")
                List = new ObservableCollection<Magazyny>(List.OrderBy(item => item.Nazwa));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Lokalizacja", "Nazwa" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Lokalizacja")
                List = new ObservableCollection<Magazyny>(List.Where(item => item.Lokalizacja != null && item.Lokalizacja.StartsWith(FindTextBox)));
            if (FindField == "Nazwa")
                List = new ObservableCollection<Magazyny>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Magazyny>(
                    sklepEntities.Magazyny.ToList()
                );
        }
        public override void pdf()
        {
            string filePath = "Magazyn.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Magazynow", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(3);
                        table.WidthPercentage = 100;

                        table.AddCell("Id");
                        table.AddCell("Nazwa");
                        table.AddCell("Lokalizacja");

                        foreach (var f in List)
                        {
                            table.AddCell(f.IdMagazynu.ToString() ?? "-");
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.Lokalizacja ?? "-");
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
