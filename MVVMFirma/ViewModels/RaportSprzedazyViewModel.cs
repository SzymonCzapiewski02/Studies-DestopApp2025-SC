using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class RaportSprzedazyViewModel : WorkspaceViewModel
    {
        #region DB
        private readonly SklepSamochodowyEntities DB;
        #endregion

        #region Constructor
        public RaportSprzedazyViewModel()
        {
            base.DisplayName = "Raport Sprzedazy";
            DB = new SklepSamochodowyEntities();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
            MinimalnaCena = 0;
            IdKlient = null;
            IdProduktu = null;
            VAT = 0;
            Rabat = 0;
            StatusZamowienia = null;
            Messenger.Default.Register<KlienciForAllView>(this, getWybranyKlient);
            Messenger.Default.Register<Kategorie>(this, getWybranyKategorie);
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
        private BaseCommand _ShowKlienci;
        public ICommand ShowKlientci
        {
            get
            {
                if (_ShowKlienci == null)
                    _ShowKlienci = new BaseCommand(() => showKlientci());
                return _ShowKlienci;
            }
        }
        private void showKlientci()
        {
            Messenger.Default.Send("KlienciAll");
        }
        #endregion
        #region Fields and Properties
        private int? _IdKlient;
        public int? IdKlient
        {
            get => _IdKlient;
            set
            {
                if (_IdKlient != value)
                {
                    _IdKlient = value;
                    OnPropertyChanged(() => IdKlient);
                }
            }
        }

        private DateTime _DataOd;
        public DateTime DataOd
        {
            get => _DataOd;
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
            get => _DataDo;
            set
            {
                if (_DataDo != value)
                {
                    _DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }

        private int? _IdProduktu;
        public int? IdProduktu
        {
            get => _IdProduktu;
            set
            {
                if (_IdProduktu != value)
                {
                    _IdProduktu = value;
                    OnPropertyChanged(() => IdProduktu);
                }
            }
        }

        private decimal _MinimalnaCena;
        public decimal MinimalnaCena
        {
            get => _MinimalnaCena;
            set
            {
                if (_MinimalnaCena != value)
                {
                    _MinimalnaCena = value;
                    OnPropertyChanged(() => MinimalnaCena);
                }
            }
        }

        private int? _IdKategorii;
        public int? IdKategorii
        {
            get => _IdKategorii;
            set
            {
                if (_IdKategorii != value)
                {
                    _IdKategorii = value;
                    OnPropertyChanged(() => IdKategorii);
                }
            }
        }

        private decimal? _VAT;
        public decimal? VAT
        {
            get => _VAT;
            set
            {
                if (_VAT != value)
                {
                    _VAT = value;
                    OnPropertyChanged(() => VAT);
                }
            }
        }

        private decimal? _Rabat;
        public decimal? Rabat
        {
            get => _Rabat;
            set
            {
                if (_Rabat != value)
                {
                    _Rabat = value;
                    OnPropertyChanged(() => Rabat);
                }
            }
        }

        private string _StatusZamowienia;
        public string StatusZamowienia
        {
            get => _StatusZamowienia;
            set
            {
                if (_StatusZamowienia != value)
                {
                    _StatusZamowienia = value;
                    OnPropertyChanged(() => StatusZamowienia);
                }
            }
        }
        public string KlientImie { get; set; }
        public string KlientNazwisko { get; set; }
        public string KategoriaNazwa { get; set; }
        public string ProduktNazwa { get; set; }
        public string ProduktNumerCzesci { get; set; }
        private List<RaportSprzedazyB.RaportSprzedazyP> _Raport;
        public List<RaportSprzedazyB.RaportSprzedazyP> Raport
        {
            get => _Raport;
            set
            {
                _Raport = value;
                OnPropertyChanged(() => Raport);
            }
        }

        #endregion

        #region Commands
        private void getWybranyProdukt(ProduktyForAllView produkt)
        {
            IdProduktu = produkt.IdProduktu;
            ProduktNazwa = produkt.Nazwa;
            ProduktNumerCzesci = produkt.NumerCzesci;
        }
        private void getWybranyKlient(KlienciForAllView klient)
        {
            IdKlient = klient.IdKlienta;
            KlientImie = klient.Imie;
            KlientNazwisko = klient.Nazwisko;
        }
        private void getWybranyKategorie(Kategorie kategorie)
        {
            IdKategorii = kategorie.IdKategorii;
            KategoriaNazwa = kategorie.Nazwa;
        }
        private BaseCommand _GenerujCommand;
        public ICommand GenerujCommand
        {
            get
            {
                if (_GenerujCommand == null)
                    _GenerujCommand = new BaseCommand(() => GenerujRaport());
                return _GenerujCommand;
            }
        }

        private void GenerujRaport()
        {
            Raport = new RaportSprzedazyB(DB).GenerowanieRaportu(
                IdKlient,
                DataOd,
                DataDo,
                IdProduktu,
                MinimalnaCena,
                IdKategorii,
                VAT,
                Rabat,
                StatusZamowienia
            ).ToList();
        }
        #endregion
    }
}
