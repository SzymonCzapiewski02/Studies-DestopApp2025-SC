using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class SzczegolyZamowieniaForAllView
    {
        public int IdSzczegolu { get; set; }
        public DateTime? DataZamowienia { get; set; }
        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public int? Ilosc {  get; set; }
    }
}
