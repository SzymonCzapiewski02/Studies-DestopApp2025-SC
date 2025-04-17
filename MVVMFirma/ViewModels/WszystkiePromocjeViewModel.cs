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
    public class WszystkiePromocjeViewModel: WszystkieViewModel<PromocjeForAllView>
    {
        #region Construktor
        public WszystkiePromocjeViewModel() 
            : base()
        {
            base.DisplayName = "Promocje";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Nazwa", "Data Rozpoczecia", "Data Zakonczenia", "Nowa Cena", "Aktywna" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Nazwa")
                List = new ObservableCollection<PromocjeForAllView>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Data Rozpoczecia")
                List = new ObservableCollection<PromocjeForAllView>(List.OrderBy(item => item.DataRozpoczecia));
            if (SortField == "Data Zakonczenia")
                List = new ObservableCollection<PromocjeForAllView>(List.OrderBy(item => item.DataZakonczenia));
            if (SortField == "Nowa Cena")
                List = new ObservableCollection<PromocjeForAllView>(List.OrderBy(item => item.NowaCena));
            if (SortField == "Aktywna")
                List = new ObservableCollection<PromocjeForAllView>(List.OrderBy(item => item.Aktywna));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Nazwa", "Nowa Cena" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<PromocjeForAllView>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
            if (FindField == "Nowa Cena")
                List = new ObservableCollection<PromocjeForAllView>(List.Where(item => item.NowaCena != null && item.NowaCena.ToString().StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PromocjeForAllView>(
                    from Promocje in sklepEntities.Promocje
                    select new PromocjeForAllView
                    {
                        IdPromocji = Promocje.IdPromocji,
                        Nazwa = Promocje.Produkty.Nazwa,
                        Producent = Promocje.Produkty.Producent,
                        NumerCzesci = Promocje.Produkty.NumerCzesci,
                        DataRozpoczecia = Promocje.DataRozpoczecia,
                        DataZakonczenia = Promocje.DataZakonczenia,
                        NowaCena = Promocje.NowaCena,
                        Aktywna = Promocje.Aktywna,
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Promocje.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();

                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Promocji", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(7);
                        table.WidthPercentage = 100;

                        table.AddCell("Nazwa");
                        table.AddCell("Producent");
                        table.AddCell("Numer Czesci");
                        table.AddCell("Data Rozpoczecia");
                        table.AddCell("Data Zakonczenia");
                        table.AddCell("Nowa Cena");
                        table.AddCell("Aktywna");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.Producent ?? "-");
                            table.AddCell(f.NumerCzesci ?? "-");
                            table.AddCell(f.DataRozpoczecia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.DataZakonczenia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.NowaCena?.ToString() ?? "-");
                            table.AddCell(f.Aktywna?.ToString() ?? "-");
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
