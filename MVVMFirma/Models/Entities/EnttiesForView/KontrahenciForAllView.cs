using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class KontrahenciForAllView
    {
        public int IdKontrahenta { get; set; }
        public string Nazwa {  get; set; }
        public string Kontakt {  get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public string NIP {  get; set; }
        public string Regon {  get; set; }
        public string TypKontrahenta { get; set; }
        public DateTime? DataUtworzenia { get; set; }
        public string StatusKontrahenta { get; set; }
    }
}
