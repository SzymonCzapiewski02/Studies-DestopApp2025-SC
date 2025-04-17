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
    public class WszystkieKategorieViewModel : WszystkieViewModel<Kategorie>
    {
        #region Construktor
        public WszystkieKategorieViewModel()
            :base()
        {
            base.DisplayName = "Kategorie";
        }
        #endregion
        #region Properties
        private Kategorie _WybranyKategorie;
        public Kategorie WybranyKategorie
        {
            get
            {
                return _WybranyKategorie;
            }

            set
            {
                _WybranyKategorie = value;
                Messenger.Default.Send(_WybranyKategorie);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Nazwa", "Opis" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Nazwa")
                List = new ObservableCollection<Kategorie>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Opis")
                List = new ObservableCollection<Kategorie>(List.OrderBy(item => item.Opis));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Kategorie>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers 
        public override void Load()
        {
            List = new ObservableCollection<Kategorie>
                (
                    sklepEntities.Kategorie.ToList()
                );
        }
        public override void pdf()
        {
            string filePath = "Kategorie.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Kategorii", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(2);
                        table.WidthPercentage = 100;

                        table.AddCell("Nazwa");
                        table.AddCell("Opis");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.Opis ?? "-");
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
