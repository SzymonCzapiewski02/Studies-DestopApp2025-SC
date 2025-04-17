using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class TelefonValidator
    {
        public static string CzyDobryNumer(string numer)
        {
            if (numer == null)
            {
                return "Vat nie może być pusta.";
            }
            string liczbaString = numer.ToString();

            if (liczbaString.Length != 9)
            {
                return "Numer musi mieć 9 liczb.";
            }

            return null;
        }
    }
}
