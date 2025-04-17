using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities.Validatory;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NowaHistoriaLogowaniaViewModel:JedenViewModel<HistoriaLogowania>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaHistoriaLogowaniaViewModel()
            : base("Historia Logowania")
        {
            item = new HistoriaLogowania();
            Messenger.Default.Register<KlienciForAllView>(this, getWybranyKlient);
        }
        #endregion
        #region Command
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
        public int? IdKlienta
        {
            get
            {
                return item.IdKlienta;
            }
            set
            {
                item.IdKlienta = value;
                OnPropertyChanged(() => IdKlienta);
            }
        }
        public DateTime? DataLogowania
        {
            get
            {
                return item.DataLogowania;
            }
            set
            {
                item.DataLogowania = value;
                OnPropertyChanged(() => DataLogowania);
            }
        }
        public string AdresIP
        {
            get
            {
                return item.AdresIP;
            }
            set
            {
                item.AdresIP = value;
                OnPropertyChanged(() => AdresIP);
            }
        }
        public string KlientNazwa { get; set; }
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
                if (name == "DataLogowania")
                    komunikat = DataValidator.SprawdzDate(this.DataLogowania);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["DataLogowania"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> KlientKeyAndValue
        {
            get
            {
                return new KlienciB(SklepEntities).GetKLientKeyAndValueItemsImie();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyKlient(KlienciForAllView klient)
        {
            IdKlienta = klient.IdKlienta;
            KlientNazwa = klient.Imie + " " + klient.Nazwisko;
        }
        public override void Save()
        {
            SklepEntities.HistoriaLogowania.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
