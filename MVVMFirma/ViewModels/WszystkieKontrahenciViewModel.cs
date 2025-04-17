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
    public class WszystkieKontrahenciViewModel: WszystkieViewModel<KontrahenciForAllView>
    {
        #region Construktor
        public WszystkieKontrahenciViewModel()
            : base()
        {
            base.DisplayName = "Kontrahenci";
        }
        #endregion
        #region Propertis
        private KontrahenciForAllView _WybranyKontrahenci;
        public KontrahenciForAllView WybranyKontrahenci
        {
            get
            {
                return _WybranyKontrahenci;
            }

            set
            {
                _WybranyKontrahenci = value;
                Messenger.Default.Send(_WybranyKontrahenci);
                OnRequestClose();
            }
        }
        #endregion 
        #region Sort And Find
        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Nazwa", "Email", "Telefon", "Typ Kontrahenta", "Data Utworzenia" };
        }

        public override void Sort()
        {
            Load();
            if (SortField == "Nazwa")
                List = new ObservableCollection<KontrahenciForAllView>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Email")
                List = new ObservableCollection<KontrahenciForAllView>(List.OrderBy(item => item.Email));
            if (SortField == "Telefon")
                List = new ObservableCollection<KontrahenciForAllView>(List.OrderBy(item => item.Telefon));
            if (SortField == "Typ Kontrahenta")
                List = new ObservableCollection<KontrahenciForAllView>(List.OrderBy(item => item.TypKontrahenta));
            if (SortField == "Data Utworzenia")
                List = new ObservableCollection<KontrahenciForAllView>(List.OrderBy(item => item.DataUtworzenia));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Nazwa", "Email", "Typ Kontrahenta" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<KontrahenciForAllView>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));
            if (FindField == "Email")
                List = new ObservableCollection<KontrahenciForAllView>(List.Where(item => item.Email != null && item.Email.StartsWith(FindTextBox)));
            if (FindField == "Typ Kontrahenta")
                List = new ObservableCollection<KontrahenciForAllView>(List.Where(item => item.TypKontrahenta != null && item.TypKontrahenta.StartsWith(FindTextBox)));
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahenciForAllView>(
                    from Kontrahenci in sklepEntities.Kontrahenci
                    select new KontrahenciForAllView
                    {
                        IdKontrahenta = Kontrahenci.IdKontrahenta,
                        Nazwa = Kontrahenci.Nazwa,
                        Kontakt = Kontrahenci.Kontakt,
                        Email = Kontrahenci.Email,
                        Telefon = Kontrahenci.Telefon,
                        Miasto = Kontrahenci.Adres.Miasto,
                        Ulica = Kontrahenci.Adres.Ulica,
                        KodPocztowy = Kontrahenci.Adres.KodPocztowy,
                        NIP = Kontrahenci.NIP,
                        Regon = Kontrahenci.Regon,
                        TypKontrahenta = Kontrahenci.TypKontrahenta,
                        DataUtworzenia = Kontrahenci.DataUtworzenia,
                        StatusKontrahenta = Kontrahenci.StatusKontrahenta
                    }
                );
        }
        public override void pdf()
        {
            string filePath = "Kontrahentow.pdf";

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(filePath, System.IO.FileMode.Create));

                    document.Open();
                    var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    var title = new iTextSharp.text.Paragraph("Lista Kontrahentow", titleFont)
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    if (List != null && List.Any())
                    {
                        var table = new iTextSharp.text.pdf.PdfPTable(12);
                        table.WidthPercentage = 100;

                        table.AddCell("Nazwa");
                        table.AddCell("Kontakt");
                        table.AddCell("Email");
                        table.AddCell("Telefon");
                        table.AddCell("Miasto");
                        table.AddCell("Ulica");
                        table.AddCell("Kod Pocztowy");
                        table.AddCell("NIP");
                        table.AddCell("Regon");
                        table.AddCell("Typ Kontrahenta");
                        table.AddCell("DataUtworzenia");
                        table.AddCell("StatusKontrahenta");

                        foreach (var f in List)
                        {
                            table.AddCell(f.Nazwa ?? "-");
                            table.AddCell(f.Kontakt ?? "-");
                            table.AddCell(f.Email ?? "-");
                            table.AddCell(f.Telefon ?? "-");
                            table.AddCell(f.Miasto ?? "-");
                            table.AddCell(f.Ulica ?? "-");
                            table.AddCell(f.KodPocztowy ?? "-");
                            table.AddCell(f.NIP ?? "-");
                            table.AddCell(f.Regon ?? "-");
                            table.AddCell(f.TypKontrahenta ?? "-");
                            table.AddCell(f.DataUtworzenia?.ToString("yyyy-MM-dd") ?? "-");
                            table.AddCell(f.StatusKontrahenta ?? "-");
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
