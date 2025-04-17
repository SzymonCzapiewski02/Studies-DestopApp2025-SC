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
    public class NoweZapasyMagazynoweViewModel:JedenViewModel<ZapasyMagazynowe>, IDataErrorInfo
    {
        #region Konstruktor
        public NoweZapasyMagazynoweViewModel()
            : base("Zapasy Magazynowe")
        {
            item = new ZapasyMagazynowe();
            Messenger.Default.Register<Magazyny>(this, getWybranyMagazynu);
            Messenger.Default.Register<ProduktyForAllView>(this, getWybranyProdukt);
        }
        #endregion
        #region Command
        private BaseCommand _ShowMagazynu;
        public ICommand ShowMagazynu
        {
            get
            {
                if (_ShowMagazynu == null)
                    _ShowMagazynu = new BaseCommand(() => showMagazynu());
                return _ShowMagazynu;
            }
        }
        private void showMagazynu()
        {
            Messenger.Default.Send("MagazynuAll");
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
        public string ProduktNazwa { get; set; }
        public string MagazynNazwa { get; set; }
        public int? IdMagazynu
        {
            get
            {
                return item.IdMagazynu;
            }
            set
            {
                item.IdMagazynu = value;
                OnPropertyChanged(() => IdMagazynu);
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
        public IQueryable<KeyAndValue> MagazynuKeyAndValue
        {
            get
            {
                return new MagazynyB(SklepEntities).GetMagazynyKeyAndValueItemsNazwa();
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
        private void getWybranyMagazynu(Magazyny magazyny)
        {
            IdMagazynu = magazyny.IdMagazynu;
            MagazynNazwa = magazyny.Nazwa;
        }
        private void getWybranyProdukt(ProduktyForAllView produkt)
        {
            IdProduktu = produkt.IdProduktu;
            ProduktNazwa = produkt.Nazwa;
        }
        public override void Save()
        {
            SklepEntities.ZapasyMagazynowe.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
