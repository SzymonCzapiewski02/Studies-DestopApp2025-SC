using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class LoginViewModel : WorkspaceViewModel
    {
        private SklepSamochodowyEntities _db;
        private AdminiB _adminiB;

        public LoginViewModel()
        {
            _db = new SklepSamochodowyEntities();
            _adminiB = new AdminiB(_db);
            LoginCommand = new BaseCommand(ExecuteLogin);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(() => Username);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(() => Password);
            }
        }

        public ICommand LoginCommand { get; }

        private void ExecuteLogin()
        {
            var user = _adminiB.LogowanieAdmina(Username, Password).FirstOrDefault();

            if (user != null)
            {
                MessageBox.Show("Logowanie pomyślne!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainWindowViewModel();
                mainWindow.Show();

                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Błędna nazwa użytkownika lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
