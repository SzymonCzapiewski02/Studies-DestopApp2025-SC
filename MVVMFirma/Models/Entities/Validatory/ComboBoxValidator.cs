using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Entities.Validatory
{
    public class ComboBoxValidator
    {
        public static string SprawdzCzyWybrany(object selectedItem)
        {
            if (selectedItem == null)
            {
                return "Należy wybrać z listy";
            }
            return null;
        }
    }
}
