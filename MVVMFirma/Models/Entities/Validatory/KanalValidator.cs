using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class KanalValidator
    {
        public static string SprawdzPierwszaLiteraIKod(string wartosc)
        {
            if (wartosc == null)
            {
                return "Wartosc nie może być pusta";
            }

            if (wartosc[0] != 'K')
            {
                return "Pierwsza litera musi być 'K'.";
            }

            string reszta = wartosc.Substring(1);

            if (reszta.Length != 10)
            {
                return "Po pierwszej literze musi występować dokładnie 11 cyfr.";
            }

            return null;
        }
    }
}
