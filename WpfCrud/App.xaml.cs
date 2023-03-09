using System.Windows;
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
            var loginWindow = new LoginWindow(new LoginViewModel());
            loginWindow.Show();
        }
    }
}
