using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public abstract class JedenViewModel<T>:WorkspaceViewModel
    {
        #region DB
        protected SklepSamochodowyEntities SklepEntities;
        #endregion
        #region Item
        protected T item;
        #endregion
        #region Command
        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(() => SaveAndClose());
                return _SaveCommand;
            }
        }
        private BaseCommand _CloseCommandd;
        public ICommand CloseCommandd
        {
            get
            {
                if (_CloseCommandd == null)
                    _CloseCommandd = new BaseCommand(() => Closee());
                return _CloseCommandd;
            }
        }
        #endregion
        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            SklepEntities = new SklepSamochodowyEntities();
        }
        #endregion
        #region Helpers
        public abstract void Save();
        public void SaveAndClose()
        {
            if (IsValid())
            {
                Save();
                base.OnRequestClose();
            }
        }
        public void Closee()
        {
                base.OnRequestClose();
        }
        #endregion
        #region Validation
        public virtual bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
