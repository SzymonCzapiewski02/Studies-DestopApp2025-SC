using GalaSoft.MvvmLight.Messaging;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region DB
        protected readonly SklepSamochodowyEntities sklepEntities;
        #endregion
        #region Command
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                    _LoadCommand = new BaseCommand(() => Load());
                return _LoadCommand;
            }
        }
        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new BaseCommand(() => add());
                return _AddCommand;
            }
        }
        private BaseCommand _GeneratePdfCommand;
        public ICommand GeneratePdfCommand
        {
            get
            {
                if (_GeneratePdfCommand == null)
                    _GeneratePdfCommand = new BaseCommand(() => pdf());
                return _GeneratePdfCommand;
            }
        }
        #endregion
        #region List
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null)
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Sort And Filtr
        public string SortField { get; set; }

        public List<string> SortComboboxItems
        {
            get
            {
                return getComboboxSortList();
            }
        }

        public abstract List<string> getComboboxSortList();
        private BaseCommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new BaseCommand(() => Sort());
                return _SortCommand;
            }
        }
        public abstract void Sort();

        public string FindField { get; set; }

        public List<string> FindComboboxItems
        {
            get
            {
                return getComboboxFindList();
            }
        }

        public abstract List<string> getComboboxFindList();
        public string FindTextBox { get; set; }
        private BaseCommand _FindCommand;
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                    _FindCommand = new BaseCommand(() => Find());
                return _FindCommand;
            }
        }
        public abstract void Find();
        #endregion
        #region Construktor
        public WszystkieViewModel()
        {
            sklepEntities = new SklepSamochodowyEntities();
        }
        #endregion
        #region Helpers
        public abstract void Load();
        public void add()
        {
            Messenger.Default.Send(DisplayName + "Add");
        }
        public abstract void pdf();
        #endregion
    }
}
