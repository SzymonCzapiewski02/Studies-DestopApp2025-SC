using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class VatValidator
    {
        public static string CenaCzyPoprawna(decimal? cena)
        {
            if (cena == null)
            {
                return "Vat nie może być pusta.";
            }

            if (cena < 0 || cena > 100)
            {
                return "Vat musi być pomiedzy 0 a 100";
            }

            return null;
        }
    }
}
