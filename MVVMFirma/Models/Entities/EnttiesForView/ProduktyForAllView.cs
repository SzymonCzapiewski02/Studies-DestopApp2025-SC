using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class ProduktyForAllView
    {
        public int IdProduktu { get; set; }
        public string Nazwa { get; set; }
        public string NazwaKategori {  get; set; }
        public string Producent { get; set; }
        public string NumerCzesci { get; set; }
        public int? IloscWMagazynie { get; set; }
        public decimal? Cena {  get; set; }
        public string Opis {  get; set; }
        public bool? Dostepny {  get; set; }
    }
}
