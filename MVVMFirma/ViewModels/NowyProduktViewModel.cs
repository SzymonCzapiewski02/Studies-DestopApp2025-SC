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
    public class NowyProduktViewModel:JedenViewModel<Produkty>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyProduktViewModel()
            : base("Produkt")
        {
            item = new Produkty();
            Messenger.Default.Register<Kategorie>(this, getWybranyKategorie);
        }
        #endregion
        #region Command
        private BaseCommand _ShowKategorie;
        public ICommand ShowKategorie
        {
            get
            {
                if (_ShowKategorie == null)
                    _ShowKategorie = new BaseCommand(() => showKategorie());
                return _ShowKategorie;
            }
        }
        private void showKategorie()
        {
            Messenger.Default.Send("KategorieAll");
        }
        #endregion
        #region Pola
        public string KategoriaNazwa {  get; set; }
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }
            set
            {
                item.Nazwa = value;
                OnPropertyChanged(() => Nazwa);
            }
        }
        public int? IdKategorie
        {
            get
            {
                return item.IdKategori;
            }
            set
            {
                item.IdKategori = value;
                OnPropertyChanged(() => IdKategorie);
            }
        }
        public string Producent
        {
            get
            {
                return item.Producent;
            }
            set
            {
                item.Producent = value;
                OnPropertyChanged(() => Producent);
            }
        }
        public string NumerCzesci
        {
            get
            {
                return item.NumerCzesci;
            }
            set
            {
                item.NumerCzesci = value;
                OnPropertyChanged(() => NumerCzesci);
            }
        }
        public Decimal? Cena 
        {
            get
            {
                return item.Cena;
            }
            set
            {
                item.Cena = value;
                OnPropertyChanged(() => Cena);
            }
        }
        public string Opis
        {
            get
            {
                return item.Opis;
            }
            set
            {
                item.Opis = value;
                OnPropertyChanged(() => Opis);
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
                if (name == "Cena")
                    komunikat = CenaValidator.CenaCzyPoprawna(this.Cena);
                if (name == "NumerCzesci")
                    komunikat = LiczbaValidator.SprawdzCzy5Cyfr(this.NumerCzesci);
                if (name == "Nazwa")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Nazwa);
                if (name == "Opis")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Opis);
                if (name == "Producent")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Producent);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["Cena"] == null && this["Nazwa"] == null && this["Opis"] == null
                && this["NumerCzesci"] == null && this["Producent"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> KategorieKeyAndValue
        {
            get
            {
                return new KategowieB(SklepEntities).GetKategorieKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyKategorie(Kategorie kategorie)
        {
            IdKategorie = kategorie.IdKategorii;
            KategoriaNazwa = kategorie.Nazwa;
        }
        public override void Save()
        {
            SklepEntities.Produkty.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
