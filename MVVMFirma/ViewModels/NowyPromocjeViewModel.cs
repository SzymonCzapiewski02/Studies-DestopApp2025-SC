using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities.Validatory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NowyPromocjeViewModel:JedenViewModel<Promocje>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyPromocjeViewModel()
            : base("Promocje")
        {
            item = new Promocje();
            Messenger.Default.Register<ProduktyForAllView>(this, getWybranyProdukt);
        }
        #endregion
        #region Command
        private BaseCommand _ShowProdukty;
        public ICommand ShowProdukty
        {
            get
            {
                if (_ShowProdukty == null)
                    _ShowProdukty = new BaseCommand(() => showProdukty());
                return _ShowProdukty;
            }
        }
        private void showProdukty()
        {
            Messenger.Default.Send("ProduktyAll");
        }
        #endregion
        #region Pola
        public string ProduktNazwa { get; set; }
        public int? IdProdukty
        {
            get
            {
                return item.IdProduktu;
            }
            set
            {
                item.IdProduktu = value;
                OnPropertyChanged(() => IdProdukty);
            }
        }
        public DateTime? DataRozpoczecia
        {
            get
            {
                return item.DataRozpoczecia;
            }
            set
            {
                item.DataRozpoczecia = value;
                OnPropertyChanged(() => DataRozpoczecia);
            }
        }
        public DateTime? DataZakonczenia
        {
            get
            {
                return item.DataZakonczenia;
            }
            set
            {
                item.DataZakonczenia = value;
                OnPropertyChanged(() => DataZakonczenia);
            }
        }
        public Decimal? NowaCena
        {
            get
            {
                return item.NowaCena;
            }
            set
            {
                item.NowaCena = value;
                OnPropertyChanged(() => NowaCena);
            }
        }
        #endregion
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string komunikat = null;
                if (name == "DataRozpoczecia")
                    komunikat = DataValidator.SprawdzDate(this.DataRozpoczecia);
                if (name == "DataZakonczenia")
                    komunikat = DataValidator.SprawdzDate(this.DataZakonczenia);
                if (name == "NowaCena")
                    komunikat = CenaValidator.CenaCzyPoprawna(this.NowaCena);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["DataRozpoczecia"] == null && this["DataZakonczenia"] == null && this["NowaCena"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> ProduktyKeyAndValue
        {
            get
            {
                return new ProduktyB(SklepEntities).GetProduktyKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyProdukt(ProduktyForAllView produkt)
        {
            IdProdukty = produkt.IdProduktu;
            ProduktNazwa = produkt.Nazwa;
        }
        public override void Save()
        {
            SklepEntities.Promocje.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
