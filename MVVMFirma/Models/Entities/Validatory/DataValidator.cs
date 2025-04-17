using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class DataValidator
    {
        public static string SprawdzDate(DateTime? date)
        {
            if (date == null)
            {
                return "Data nie może być pusta";
            }

            if (date < new DateTime(2020, 1, 1))
            {
                return "Data nie może być wczesniesza niż 1 stycznia 2020.";
            }

            if (date > DateTime.Now)
            {
                return "Data nie może być w przysłości";
            }

            return null;
        }
    }
}
