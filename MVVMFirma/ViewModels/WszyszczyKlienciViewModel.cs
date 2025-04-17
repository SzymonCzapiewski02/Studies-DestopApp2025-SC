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
    public class WszyszczyKlienciViewModel: WszystkieViewModel<KlienciForAllView>
    {
        #region Construktor
        public WszyszczyKlienciViewModel()
            :base()
        {
            base.DisplayName = "Klienci";
        }
        #endregion
        #region Properties
        private KlienciForAllView _WybranyKlient;
        public KlienciForAllView WybranyKlient
        {
            get
            {
                return _WybranyKlient;
            }

            set
            {
                _WybranyKlient = value;
                Messenger.Default.Send(_WybranyKlient);
                OnRequestClose();
            }
        }
        #endregion
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "imie", "nazwisko", "imie i nazwisko" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "imie")
                List = new ObservableCollection<KlienciForAllView>(List.OrderBy(item => item.Imie));
            if (SortField == "nazwisko")
                List = new ObservableCollection<KlienciForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imie i nazwisko")
                List = new ObservableCollection<KlienciForAllView>(List.OrderBy(item => item.Imie).OrderBy(item => item.Nazwisko));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "imie", "nazwisko" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "imie")
                List = new ObservableCollection<KlienciForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "nazwisko")
                List = new ObservableCollection<KlienciForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KlienciForAllView>(
                    from Klienci in sklepEntities.Klienci
                    select new KlienciForAllView
                    {
                        IdKlienta = Klienci.IdKlienta,
                        Imie = Klienci.Imie,
                        Nazwisko = Klienci.Nazwisko,
                        Email = Klienci.Email,
                        Telefon = Klienci.Telefon,
                        Ulica = Klienci.Adres.Ulica,
                        Miasto = Klienci.Adres.Miasto,
                        KodPocztowy = Klienci.Adres.KodPocztowy,
                        DataRejestracji = Klienci.DataRejestracji,
                        Haslo = Klienci.Haslo
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Klient.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Klietów", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(8);
                        table.WidthPercentage = 100;

                        table.AddCell("Imie");
                        table.AddCell("Nazwisko");
                        table.AddCell("Email");
                        table.AddCell("Telefon");
                        table.AddCell("Ulica");
                        table.AddCell("Miasto");
                        table.AddCell("Kod Pocztowy");
                        table.AddCell("Data Rejestracji");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Imie ?? "-");
                            table.AddCell(f.Nazwisko ?? "-");
                            table.AddCell(f.Email ?? "-");
                            table.AddCell(f.Telefon ?? "-");
                            table.AddCell(f.Ulica ?? "-");
                            table.AddCell(f.Miasto ?? "-");
                            table.AddCell(f.KodPocztowy ?? "-");
                            table.AddCell(f.DataRejestracji?.ToString("yyyy-MM-dd") ?? "-");
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
