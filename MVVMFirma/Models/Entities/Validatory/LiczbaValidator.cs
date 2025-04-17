using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class LiczbaValidator
    {
        public static string SprawdzCzy5Cyfr(string liczba)
        {
            if (liczba == null)
            {
                return "Numer refereczyjny nie może być pusta.";
            }

            string liczbaString = liczba.ToString();

            if (liczbaString.Length > 6)
            {
                return "Numer refereczyjny musi być 6 liczbowy";
            }
            if (liczbaString.Length < 6)
            {
                return "Numer refereczyjny musi być 6 liczbowy";
            }

            return null;
        }
    }
}
