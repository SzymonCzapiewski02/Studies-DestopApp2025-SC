using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class KlienciForAllView
    {
        public int IdKlienta { get; set; }
        public string Imie {  get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy {  get; set; }
        public DateTime? DataRejestracji { get; set; }
        public string Haslo {  get; set; }

    }
}
