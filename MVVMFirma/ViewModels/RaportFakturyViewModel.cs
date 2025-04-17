using GalaSoft.MvvmLight.Messaging;
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
    public class RaportFakturyViewModel : WorkspaceViewModel
    {
        #region DB
        private SklepSamochodowyEntities DB;
        #endregion

        #region Konstruktor
        public RaportFakturyViewModel()
        {
            base.DisplayName = "Raport Faktur";
            DB = new SklepSamochodowyEntities();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
            CzyOplacone = null;
            Vat = 23;
            Messenger.Default.Register<KlienciForAllView>(this, getWybranyKlient);
            Messenger.Default.Register<Kategorie>(this, getWybranyKategorie);
            Messenger.Default.Register<Magazyny>(this, getWybranyMagazynu);
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
        #region Pola

        private bool? _CzyOplacone;
        public bool? CzyOplacone
        {
            get { return _CzyOplacone; }
            set
            {
                if (_CzyOplacone != value)
                {
                    _CzyOplacone = value;
                    OnPropertyChanged(() => CzyOplacone);
                }
            }
        }

        private DateTime? _DataOd;
        public DateTime? DataOd
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

        private DateTime? _DataDo;
        public DateTime? DataDo
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

        private int? _IdKlienta;
        public int? IdKlienta
        {
            get { return _IdKlienta; }
            set
            {
                if (value != _IdKlienta)
                {
                    _IdKlienta = value;
                    OnPropertyChanged(() => IdKlienta);
                }
            }
        }

        private decimal? _MinimalnaKwota;
        public decimal? MinimalnaKwota
        {
            get { return _MinimalnaKwota; }
            set
            {
                if (_MinimalnaKwota != value)
                {
                    _MinimalnaKwota = value;
                    OnPropertyChanged(() => MinimalnaKwota);
                }
            }
        }

        private decimal? _MaksymalnaKwota;
        public decimal? MaksymalnaKwota
        {
            get { return _MaksymalnaKwota; }
            set
            {
                if (_MaksymalnaKwota != value)
                {
                    _MaksymalnaKwota = value;
                    OnPropertyChanged(() => MaksymalnaKwota);
                }
            }
        }

        private int? _IdKategorii;
        public int? IdKategorii
        {
            get { return _IdKategorii; }
            set
            {
                if (_IdKategorii != value)
                {
                    _IdKategorii = value;
                    OnPropertyChanged(() => IdKategorii);
                }
            }
        }

        private int? _IdPromocji;
        public int? IdPromocji
        {
            get { return _IdPromocji; }
            set
            {
                if (_IdPromocji != value)
                {
                    _IdPromocji = value;
                    OnPropertyChanged(() => IdPromocji);
                }
            }
        }

        private int? _IdMagazynu;
        public int? IdMagazynu
        {
            get { return _IdMagazynu; }
            set
            {
                if (_IdMagazynu != value)
                {
                    _IdMagazynu = value;
                    OnPropertyChanged(() => IdMagazynu);
                }
            }
        }

        private decimal? _Vat;
        public decimal? Vat
        {
            get { return _Vat; }
            set
            {
                if (_Vat != value)
                {
                    _Vat = value;
                    OnPropertyChanged(() => Vat);
                }
            }
        }
        public string KlientImie { get; set; }
        public string KlientNazwisko { get; set; }
        public string KategoriaNazwa {  get; set; }
        public string MagazynNazwa { get; set; }
        public string MagazynLokalizacja { get; set; }

        private List<RaportFakturB.RaportFakturP> _Raport;
        public List<RaportFakturB.RaportFakturP> Raport
        {
            get { return _Raport; }
            set
            {
                if (_Raport != value)
                {
                    _Raport = value;
                    OnPropertyChanged(() => Raport);
                }
            }
        }

        private List<RaportFakturB.RaportKlientow> _RaportKlienta;
        public List<RaportFakturB.RaportKlientow> RaportKlienta
        {
            get { return _RaportKlienta; }
            set
            {
                if (_RaportKlienta != value)
                {
                    _RaportKlienta = value;
                    OnPropertyChanged(() => RaportKlienta);
                }
            }
        }

        #endregion

        #region Komendy
        private void getWybranyMagazynu(Magazyny magazyny)
        {
            IdMagazynu = magazyny.IdMagazynu;
            MagazynNazwa = magazyny.Nazwa;
            MagazynLokalizacja = magazyny.Lokalizacja;
        }
        private void getWybranyKlient(KlienciForAllView klient)
        {
            IdKlienta = klient.IdKlienta;
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

        private BaseCommand _GenerujCommandK;
        public ICommand GenerujCommandK
        {
            get
            {
                if (_GenerujCommandK == null)
                    _GenerujCommandK = new BaseCommand(() => GenerujRaportKlienta());
                return _GenerujCommandK;
            }
        }

        private void GenerujRaport()
        {
            Raport = new RaportFakturB(DB).GenerowanieRaportuFaktur(
                CzyOplacone,
                DataOd,
                DataDo,
                MinimalnaKwota,
                MaksymalnaKwota,
                IdKategorii,
                IdMagazynu,
                Vat 
                ).ToList();
        }

        private void GenerujRaportKlienta()
        {
            RaportKlienta = new RaportFakturB(DB).GenerujRaportKlientow(
                IdKlienta,
                DataOd,
                DataDo
                ).ToList();
        }
        #endregion
    }
}
