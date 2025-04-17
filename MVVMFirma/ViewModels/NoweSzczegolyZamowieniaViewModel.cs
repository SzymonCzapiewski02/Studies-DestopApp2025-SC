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
    public class NoweSzczegolyZamowieniaViewModel:JedenViewModel<SzczegolyZamowienia>, IDataErrorInfo
    {
        #region Konstruktor
        public NoweSzczegolyZamowieniaViewModel()
            : base("Szczegoly Zamowienia")
        {
            item = new SzczegolyZamowienia();
            Messenger.Default.Register<ZamowienieForAllView>(this, getWybranyZamowienia);
            Messenger.Default.Register<ProduktyForAllView>(this, getWybranyProdukt);
        }
        #endregion
        #region Command
        private BaseCommand _ShowZamowienia;
        public ICommand ShowZamowienia
        {
            get
            {
                if (_ShowZamowienia == null)
                    _ShowZamowienia = new BaseCommand(() => showZamowienia());
                return _ShowZamowienia;
            }
        }
        private void showZamowienia()
        {
            Messenger.Default.Send("ZamowieniaAll");
        }
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
        public int? IdZamowienia
        {
            get
            {
                return item.IdZamowienia;
            }
            set
            {
                item.IdZamowienia = value;
                OnPropertyChanged(() => IdZamowienia);
            }
        }
        public int? IdProduktu
        {
            get
            {
                return item.IdProduktu;
            }
            set
            {
                item.IdProduktu = value;
                OnPropertyChanged(() => IdProduktu);
            }
        }
        public int? Ilosc
        {
            get
            {
                return item.Ilosc;
            }
            set
            {
                item.Ilosc = value;
                OnPropertyChanged(() => Ilosc);
            }
        }
        public string ZamowieniaNazwa { get; set; }
        public string ProduktNazwa { get; set; }
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
                if (name == "Ilosc")
                    komunikat = CenaValidator.CenaCzyPoprawna(this.Ilosc);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Ilosc"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> ZamowieniaKeyAndValue
        {
            get
            {
                return new ZamowieniaB(SklepEntities).GetZamowienieKeyAndValueItemsNazwa();
            }
        }
        public IQueryable<KeyAndValue> ProduktyKeyAndValue
        {
            get
            {
                return new ProduktyB(SklepEntities).GetProduktyKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyZamowienia(ZamowienieForAllView zamowienie)
        {
            IdZamowienia = zamowienie.IdZamowienia;
            ZamowieniaNazwa = zamowienie.NumerReferencyjny;
        }
        private void getWybranyProdukt(ProduktyForAllView produkt)
        {
            IdProduktu = produkt.IdProduktu;
            ProduktNazwa = produkt.Nazwa;
        }
        public override void Save()
        {
            SklepEntities.SzczegolyZamowienia.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
