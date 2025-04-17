using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MVVMFirma.Views;
using MVVMFirma.ViewModels;

namespace MVVMFirma
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginView loginView = new LoginView();
            var loginViewModel = new LoginViewModel();
            loginView.DataContext = loginViewModel;
            loginView.Show();
        }
    }

}
