using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.EnttiesForView
{
    public class DostawyForAllView
    {
        public int IdDostawy { get; set; }
        public string Nazwa {  get; set; }
        public string Kontakt { get; set; }
        public string Email { get; set; }
        public DateTime? DataDostawy { get; set; }
        public string Status { get; set; }
    }
}
