using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class PlatnoscValidator
    {
        public static string PlatnoscZamowienia(string wartosc)
        {
            if (wartosc == null)
            {
                return "Musi być wpisana wartosc.";
            }

            wartosc = wartosc.Trim();

            if (wartosc != "Blik" && wartosc != "Karta kredytowa" && wartosc != "PayPal")
            {
                return "Została wpisana nie poprawna wartosc.";
            }

            return null;
        }
    }
}
