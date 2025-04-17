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
    public class WszystkieFakturyVielModel: WszystkieViewModel<FakturyForAllView>
    {
        #region Construktor
        public WszystkieFakturyVielModel()
            :base()
        {
            base.DisplayName = "Faktury";
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Data Wystawienia", "Kwota Całkowita", "NumerFaktury", "Termin Płatności", "Status Faktury", "Typ Faktury" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Data Wystawienia")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.DataWystawienia));
            if (SortField == "Kwota Całkowita")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.KwotaCalkowita));
            if (SortField == "NumerFaktury")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.NumerFaktury));
            if (SortField == "Termin Płatności")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.TerminPlatnosci));
            if (SortField == "Status Faktury")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.StatusFakturu));
            if (SortField == "Typ Faktury")
                List = new ObservableCollection<FakturyForAllView>(List.OrderBy(item => item.TypFaktury));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Kwota Całkowita", "Typ Faktury" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Kwota Całkowita")
                List = new ObservableCollection<FakturyForAllView>(List.Where(item => item.KwotaCalkowita != null && item.KwotaCalkowita.ToString().StartsWith(FindTextBox)));
            if (FindField == "Typ Faktury")
                List = new ObservableCollection<FakturyForAllView>(List.Where(item => item.TypFaktury != null && item.TypFaktury.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyForAllView>(
                    from Faktury in sklepEntities.Faktury
                    select new FakturyForAllView
                    {
                        IdFaktury = Faktury.IdFaktury,
                        StatusZamowienia = Faktury.Zamowienia.Status,
                        EmailKlienta = Faktury.Zamowienia.Klienci.Email,
                        DataWystawienia = Faktury.DataWystawienia,
                        KwotaCalkowita = Faktury.KwotaCalkowita,
                        TerminPlatnosci = Faktury.TerminPlatnosci,
                        NumerFaktury = Faktury.NumerFaktury,
                        PracownikImie = Faktury.Pracownicy.Imie,
                        PracownikNazwisko = Faktury.Pracownicy.Nazwisko,
                        TypFaktury = Faktury.TypFaktury,
                        StatusFakturu = Faktury.StatusFaktury,
                        Waluta = Faktury.Waluta
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Faktury.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Faktur", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(11);
                        table.WidthPercentage = 100;

                        table.AddCell("StatusZamowienia");
                        table.AddCell("Email Klienta");
                        table.AddCell("Data Wystawienia");
                        table.AddCell("Kwota Calkowita");
                        table.AddCell("Termin Platnosci");
                        table.AddCell("Numer Faktury");
                        table.AddCell("Pracownik Imie");
                        table.AddCell("Pracownik Nazwisko");
                        table.AddCell("Typ Faktury");
                        table.AddCell("Status Fakturu");
                        table.AddCell("Waluta");

                        foreach (var f in List)
                        {
                            table.AddCell(f.StatusZamowienia ?? "-");
                            table.AddCell(f.EmailKlienta ?? "-");
                            table.AddCell(f.DataWystawienia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.KwotaCalkowita?.ToString() ?? "-");
                            table.AddCell(f.TerminPlatnosci?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.NumerFaktury ?? "-");
                            table.AddCell(f.PracownikImie ?? "-");
                            table.AddCell(f.PracownikNazwisko ?? "-");
                            table.AddCell(f.TypFaktury ?? "-");
                            table.AddCell(f.StatusFakturu?.ToString() ?? "-");
                            table.AddCell(f.Waluta ?? "-");
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
