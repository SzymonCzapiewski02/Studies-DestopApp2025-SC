using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class HistorieLogowaniaForAllView
    {
        public int IdLogowania { get; set; }
        public string Imie {  get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public DateTime? DataLogowania { get; set; }
        public string AdresIP { get; set; }
    }
}
