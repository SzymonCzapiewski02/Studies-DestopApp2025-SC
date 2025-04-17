using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Views;

namespace MVVMFirma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, open);
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Pracownicy",
                    new BaseCommand(() => this.CreateShowAll(new WszystrzyPracownicyViewModel()))),
                new CommandViewModel(
                    "Kategorie",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieKategorieViewModel()))),
                new CommandViewModel(
                    "Magazyny",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieMagazywnyViewModel()))),
                new CommandViewModel(
                    "Adresy",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieAdresyViewModel()))),
                new CommandViewModel(
                    "Zamowienia",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieZamowieniaViewModel()))),
                new CommandViewModel(
                    "Klienci",
                    new BaseCommand(() => this.CreateShowAll(new WszyszczyKlienciViewModel()))),
                new CommandViewModel(
                    "Historia Logowania",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieHistorieLogowaniaViewModel()))),
                new CommandViewModel(
                    "Faktury",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieFakturyVielModel()))),
                new CommandViewModel(
                    "Zapasy Magazynowe",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieZapasyMagazynowViewModel()))),
                new CommandViewModel(
                    "Szczegoly Zamowienia",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieSzczegolyZamowieniaViewModel()))),
                new CommandViewModel(
                    "Kontrahenci",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieKontrahenciViewModel()))),
                new CommandViewModel(
                    "Dostawy",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieDostawyViewModel()))),
                new CommandViewModel(
                    "Reklamacje",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieReklamacjeViewModel()))),
                new CommandViewModel(
                    "Produkty",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieProduktyViewModel()))),
                new CommandViewModel(
                    "Promocje",
                    new BaseCommand(() => this.CreateShowAll(new WszystkiePromocjeViewModel()))),
                new CommandViewModel(
                    "Admini",
                    new BaseCommand(() => this.CreateShowAll(new WszystkieAdminViewModel()))),
                new CommandViewModel(
                    "Utarg Zamowienia",
                    new BaseCommand(() => this.CreateShowAll(new UtargZamowieniaViewModel()))),
                new CommandViewModel(
                    "Raport Sprzedazy",
                    new BaseCommand(() => this.CreateShowAll(new RaportSprzedazyViewModel()))),
                new CommandViewModel(
                    "Raport Faktury",
                    new BaseCommand(() => this.CreateShowAll(new RaportFakturyViewModel()))),
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        private void CreateView(WorkspaceViewModel nowy)
        {
            this.Workspaces.Add(nowy);
            this.SetActiveWorkspace(nowy);
        }

        private void CreateShowAll(WorkspaceViewModel nowy)
        {
            var workspace = this.Workspaces.FirstOrDefault(vm => vm.GetType() == nowy.GetType());

            if (workspace == null)
            {
                workspace = (WorkspaceViewModel)Activator.CreateInstance(nowy.GetType());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void open(string name)
        {
            if (name == "PracownicyAdd")
            {
                CreateView(new NowyPracownikViewModel());
            }
            if (name == "ZamowieniaAdd")
            {
                CreateView(new NowaZamowienieViewModel());
            }
            if (name == "MagazynyAdd")
            {
                CreateView(new NowyMagazynViewModel());
            }
            if (name == "AdresyAdd")
            {
                CreateView(new NowyAdresViewModel());
            }
            if (name == "KategorieAdd")
            {
                CreateView(new NowaKategoriaViewModel());
            }
            if (name == "DostawyAdd")
            {
                CreateView(new NoweDostawyViewModel());
            }
            if (name == "Historia LogowaniaAdd")
            {
                CreateView(new NowaHistoriaLogowaniaViewModel());
            }
            if (name == "KontrahenciAdd")
            {
                CreateView(new NowyKontrahentViewModel());
            }
            if (name == "FakturyAdd")
            {
                CreateView(new NoweFakturyViewModel());
            }
            if (name == "KlienciAdd")
            {
                CreateView(new NowyKlientViewModel());
            }
            if (name == "ProduktyAdd")
            {
                CreateView(new NowyProduktViewModel());
            }
            if (name == "PromocjeAdd")
            {
                CreateView(new NowyPromocjeViewModel());
            }
            if (name == "ReklamacjeAdd")
            {
                CreateView(new NowaReklamacjaViewModel());
            }
            if (name == "Szczegoly ZamowieniaAdd")
            {
                CreateView(new NoweSzczegolyZamowieniaViewModel());
            }
            if (name == "Zapasy MagazynowAdd")
            {
                CreateView(new NoweZapasyMagazynoweViewModel());
            }
            if (name == "AdminiAdd")
            {
                CreateView(new NowyAdminViewModel());
            }
            if (name == "KlienciAll")
            {
                CreateShowAll(new WszyszczyKlienciViewModel());
            }
            if (name == "ZamowieniaAll")
            {
                CreateShowAll(new WszystkieZamowieniaViewModel());
            }
            if (name == "KontrahencjiAll")
            {
                CreateShowAll(new WszystkieKontrahenciViewModel());
            }
            if (name == "ProduktyAll")
            {
                CreateShowAll(new WszystkieProduktyViewModel());
            }
            if (name == "MagazynuAll")
            {
                CreateShowAll(new WszystkieMagazywnyViewModel());
            }
            if (name == "AdresyAll")
            {
                CreateShowAll(new WszystkieAdresyViewModel());
            }
            if (name == "KategorieAll")
            {
                CreateShowAll(new WszystkieKategorieViewModel());
            }
        }
        #endregion
    }
}
