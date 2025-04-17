using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class UtargZamowieniaViewModel:WorkspaceViewModel
    {
        #region DB
        SklepSamochodowyEntities db;
        #endregion
        #region Konstruktor
        public UtargZamowieniaViewModel()
        {
            base.DisplayName = "Utarg Zamownienia";
            db = new SklepSamochodowyEntities();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
            Utarg = 0;
        }
        #endregion

        #region Pola
        private DateTime _DataOd;
        public DateTime DataOd
        {
            get { return _DataOd; }
            set
            {
                if (_DataOd != value)
                {
                    _DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }

        private DateTime _DataDo;
        public DateTime DataDo
        {
            get { return _DataDo; }
            set
            {
                if (_DataDo != value)
                {
                    _DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }
        private int _IdProduktu;
        public int IdProduktu
        {
            get
            {
                return _IdProduktu;
            }

            set
            {
                if (_IdProduktu != value)
                {
                    _IdProduktu = value;
                    OnPropertyChanged(() => IdProduktu);
                }
            }
        }
        private decimal? _Utarg;
        public decimal? Utarg
        {
            get
            {
                return _Utarg;
            }

            set
            {
                if (_Utarg != value)
                {
                    _Utarg = value;
                    OnPropertyChanged(() => Utarg);
                }
            }
        }

        public IQueryable<KeyAndValue> ProduktyItems
        {
            get
            {
                return new ProduktyB(db).GetProduktyKeyAndValueItemsNazwa();
            }
        }
        #endregion

        #region Komendy 
        private BaseCommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if (_ObliczCommand == null)
                    _ObliczCommand = new BaseCommand(() => obliczUtargClick());
                return _ObliczCommand;
            }
        }
        private void obliczUtargClick()
        {
            Utarg = new UtargB(db).UtargTowaruOkresu(IdProduktu, DataOd, DataDo);
        }
        #endregion
    }
}
