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

            //var dishTypes = new List<DishType>
            //{
            //    new DishType {Id = 1, Name = "A" },
            //    new DishType { Name = "B", Id = 2 },
            //    new DishType { Name = "C", Id = 3 },
            //};

            //var editEntityWindowDialogService = new EditEntityWindowDialogService();
            //editEntityWindowDialogService.ShowDialog(new EditableDishViewModel(dishTypes));

            //var editEntityWindow = new EditEntityWindow(new EditEntityViewModel(new EditableUserAccountViewModel()));
            //editEntityWindow.ShowDialog();

            var mainWindow = new MainWindow(new MainViewModel(new AdminViewModel()));
            mainWindow.Show();
            //var loginWindow = new LoginWindow(new LoginViewModel());
            //loginWindow.Show();
        }
    }
}
