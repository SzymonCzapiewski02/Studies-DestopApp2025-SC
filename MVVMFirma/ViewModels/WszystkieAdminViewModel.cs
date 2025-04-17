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
    public class WszystkieAdminViewModel: WszystkieViewModel<Admin>
    {
        #region Construktor
        public WszystkieAdminViewModel()
            : base()
        {
            base.DisplayName = "Admini";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Login" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Login")
                List = new ObservableCollection<Admin>(List.OrderBy(item => item.Login));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Login" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Login")
                List = new ObservableCollection<Admin>(List.Where(item => (item.Login != null && item.Login.StartsWith(FindTextBox))));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Admin>(
                    sklepEntities.Admin.ToList()
                );
        }
        public override void pdf()
        {
            string filePath = "Admin.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Adminów", titleFont)
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
                        table.AddCell("Login");
                        table.AddCell("Hasło");

                        foreach (var f in List)
                        {
                            table.AddCell(f.idAdmin.ToString() ?? "-");
                            table.AddCell(f.Login ?? "-");
                            table.AddCell(f.Haslo ?? "-");
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
