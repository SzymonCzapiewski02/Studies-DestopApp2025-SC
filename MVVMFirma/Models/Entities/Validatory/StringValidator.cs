using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class StringValidator
    {
        public static string SprawdzCzyZaczynaSieDuzej(string wartosc)
        {
            try
            {
                if (!char.IsUpper(wartosc, 0))
                {
                    return "Rozpoczni dużą literą.";
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}
