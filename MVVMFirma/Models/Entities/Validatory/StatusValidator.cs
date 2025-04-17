using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class StatusValidator
    {
        public static string StatusZamowienia(string status)
        {
            if (status == null)
            {
                return "Status nie może być pusty";
            }

            status = status.Trim();

            if (status != "Wysłany" && status != "W przygotowaniu" && status != "Odebrana")
            {
                return "Nie poprawna wartość wpisana";
            }

            return null;
        }
    }
}
