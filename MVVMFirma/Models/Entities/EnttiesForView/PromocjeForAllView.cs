using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class PromocjeForAllView
    {
        public int IdPromocji { get; set; }
        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public string NumerCzesci { get; set; }
        public DateTime? DataRozpoczecia { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public decimal? NowaCena { get; set; }
        public bool? Aktywna {  get; set; }
    }
}
