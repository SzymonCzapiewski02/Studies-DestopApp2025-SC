using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class ZamowienieForAllView
    {
        public int IdZamowienia { get; set; }
        public string KlientaImie { get; set; }
        public string KlientaNazwisko { get; set; }
        public DateTime? DataZamowienia { get; set; }
        public string Status {  get; set; }
        public decimal? CenaCalkowita { get; set; }
        public bool? Zamkniete {  get; set; }
        public string NumerReferencyjny {  get; set; }
        public string PracownikImie { get; set; }
        public string PracownikNazwisko { get; set; }
        public string KanalZamowienia { get; set;}
        public string Komentarz {  get; set; }
        public string MetodaPlatnosci { get; set; }
    }
}
