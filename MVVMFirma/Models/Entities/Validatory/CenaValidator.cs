using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class CenaValidator
    {
        public static string CenaCzyPoprawna(decimal? cena)
        {
            if (cena == null)
            {
                return "Cena nie może być pusta.";
            }

            if (cena < 0 || cena > 10000)
            {
                return "Cena musi być wieksza niż 0 ale też mniesza niż 10000 zł.";
            }

            return null;
        }
    }
}

