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
    public class WszystkieAdresyViewModel:WszystkieViewModel<Adres>
    {
        #region Construktor
        public WszystkieAdresyViewModel() 
            :base()
        {
            base.DisplayName = "Adresy";
        }
        #endregion
        #region Properties
        private Adres _WybranyAdres;
        public Adres WybranyAdres
        {
            get
            {
                return _WybranyAdres;
            }

            set
            {
                _WybranyAdres = value;
                Messenger.Default.Send(_WybranyAdres);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Ulica", "Miasto", "Kod Pocztowy", "Kraj" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Ulica")
                List = new ObservableCollection<Adres>(List.OrderBy(item => item.Ulica));
            if (SortField == "Miasto")
                List = new ObservableCollection<Adres>(List.OrderBy(item => item.Miasto));
            if (SortField == "Kod Pocztowy")
                List = new ObservableCollection<Adres>(List.OrderBy(item => item.KodPocztowy));
            if (SortField == "Kraj")
                List = new ObservableCollection<Adres>(List.OrderBy(item => item.Kraj));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Ulica", "Miasto", "Kod Pocztowy", "Kraj" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Ulica")
                List = new ObservableCollection<Adres>(List.Where(item => item.Ulica != null && item.Ulica.StartsWith(FindTextBox)));
            if (FindField == "Miasto")
                List = new ObservableCollection<Adres>(List.Where(item => item.Miasto != null && item.Miasto.StartsWith(FindTextBox)));
            if (FindField == "Kod Pocztowy")
                List = new ObservableCollection<Adres>(List.Where(item => item.KodPocztowy != null && item.KodPocztowy.StartsWith(FindTextBox)));
            if (FindField == "Kraj")
                List = new ObservableCollection<Adres>(List.Where(item => item.Kraj != null && item.Kraj.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Adres>(
                    sklepEntities.Adres.ToList()
                );
        }

        public override void pdf()
        {
            string filePath = "Adresy.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();

                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Adresów", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(4);
                        table.WidthPercentage = 100;

                        table.AddCell("Ulica");
                        table.AddCell("Miasto");
                        table.AddCell("Kod Pocztowy");
                        table.AddCell("Kraj");

                        foreach (var adres in List)
                        {
                            table.AddCell(adres.Ulica ?? "-");
                            table.AddCell(adres.Miasto ?? "-");
                            table.AddCell(adres.KodPocztowy ?? "-");
                            table.AddCell(adres.Kraj ?? "-");
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
                MessageBox.Show("Wystąpił błąd podczas generowania PDF: "+ ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
