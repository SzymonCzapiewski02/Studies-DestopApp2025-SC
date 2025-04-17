using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities.EnttiesForView;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using MVVMFirma.Models.Entities.Validatory;

namespace MVVMFirma.ViewModels
{
    public class NoweDostawyViewModel:JedenViewModel<Dostawy>, IDataErrorInfo
    {
        #region Konstruktor
        public NoweDostawyViewModel()
            : base("Dostawy")
        {
            item = new Dostawy();
            Messenger.Default.Register<KontrahenciForAllView>(this, getWybranyKontrahenci);
        }
        #endregion
        #region Command
        private BaseCommand _ShowKontrahencji;
        public ICommand ShowKontrahencji
        {
            get
            {
                if (_ShowKontrahencji == null)
                    _ShowKontrahencji = new BaseCommand(() => ShowKontrahencj());
                return _ShowKontrahencji;
            }
        }
        private void ShowKontrahencj()
        {
            Messenger.Default.Send("KontrahencjiAll");
        }
        #endregion
        #region Pola
        public int? IdKontrahencji
        {
            get
            {
                return item.IdKontrahentow;
            }
            set
            {
                item.IdKontrahentow = value;
                OnPropertyChanged(() => IdKontrahencji);
            }
        }
        public DateTime? DataDostawy
        {
            get
            {
                return item.DataDostawy;
            }
            set
            {
                item.DataDostawy = value;
                OnPropertyChanged(() => DataDostawy);
            }
        }
        public string Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public string KontrahentaNazwa { get; set; }
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
                if (name == "DataDostawy")
                    komunikat = DataValidator.SprawdzDate(this.DataDostawy);
                if (name == "Status")
                    komunikat = StringValidator.SprawdzCzyZaczynaSieDuzej(this.Status);
                return komunikat;
            }
        }
        public override bool IsValid()
        {
            if (this["DataDostawy"] == null && this["Status"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Własciwosci dla combobox
        public IQueryable<KeyAndValue> KontrahencjiKeyAndValue
        {
            get
            {
                return new KontrahentowB(SklepEntities).GetKontrahenciKeyAndValueItemsNazwa();
            }
        }
        #endregion
        #region Helpers
        private void getWybranyKontrahenci(KontrahenciForAllView kontrahenci)
        {
            IdKontrahencji = kontrahenci.IdKontrahenta;
            KontrahentaNazwa = kontrahenci.Nazwa;
        }
        public override void Save()
        {
            SklepEntities.Dostawy.Add(item);
            SklepEntities.SaveChanges();
        }
        #endregion
    }
}
