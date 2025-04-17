using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class ReklamacjaForAllView
    {
        public int IdReklamacji { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public DateTime? DataZamowienia { get; set; }
        public string Opis {  get; set; }
        public string Status { get; set; }
    }
}
