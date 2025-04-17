using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class FakturyForAllView
    {
        public int IdFaktury {  get; set; }
        public string StatusZamowienia { get; set; }
        public string EmailKlienta { get; set; }
        public DateTime? DataWystawienia { get; set; }
        public decimal? KwotaCalkowita { get; set; }
        public DateTime? TerminPlatnosci { get; set; }
        public string NumerFaktury { get; set; }
        public string PracownikImie { get; set; }
        public string PracownikNazwisko { get; set; }
        public string TypFaktury { get; set; }
        public bool? StatusFakturu { get; set; }
        public string Waluta {  get; set; }
    }
}
