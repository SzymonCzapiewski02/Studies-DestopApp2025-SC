using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        #region Context
        public SklepSamochodowyEntities db {  get; set; }
        #endregion
        #region Konstruktor
        public DatabaseClass(SklepSamochodowyEntities db)
        {
            this.db = db;
        }
        #endregion
    }
}
