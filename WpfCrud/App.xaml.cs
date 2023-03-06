using System.Collections.Generic;
using System.Windows;
using WpfCrud.Models;
using WpfCrud.Services;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow(new MainViewModel(new AdminViewModel()));
            mainWindow.Show();
            //var loginWindow = new LoginWindow(new LoginViewModel());
            //loginWindow.Show();
        }
    }
}
