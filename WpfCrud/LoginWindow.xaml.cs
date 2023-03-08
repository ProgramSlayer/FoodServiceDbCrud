using System;
using System.Windows;
using System.Windows.Controls;
using WpfCrud.Models;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _loginViewModel;
        private MainWindow _mainWindow;

        public LoginWindow(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel ?? throw new ArgumentNullException(nameof(loginViewModel));
            _loginViewModel.LoginSuccess += OnLoginSuccess;
            _loginViewModel.LoginFailure += OnLoginFailure;
            DataContext = loginViewModel;
            InitializeComponent();
        }

        private void OnLoginFailure(string obj)
        {
            MessageBox.Show(obj, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnLoginSuccess()
        {
            MessageBox.Show("Добро пожаловать", "Авторизация пройдена", MessageBoxButton.OK, MessageBoxImage.Information);
            switch (_loginViewModel.CurrentUserRole)
            {
                case Models.Enums.UserRoleEnum.Admin:
                    var currentUser = new CurrentUser
                    {
                        Id = _loginViewModel.CurrentUserId,
                        Login = _loginViewModel.Login,
                        Role = _loginViewModel.CurrentUserRole,
                    };
                    ShowMainWindow(new AdminViewModel(currentUser));
                    break;
                case Models.Enums.UserRoleEnum.Chef:
                    ShowMainWindow(new ChefViewModel());
                    break;
                case Models.Enums.UserRoleEnum.Cook:
                    ShowMainWindow(new CookViewModel());
                    break;
                default:
                    Close();
                    return;
            }
            Hide();
        }

        private void ShowMainWindow(ViewModelBase currentViewModel)
        {
            var mainViewModel = new MainViewModel(currentViewModel);
            _mainWindow = new MainWindow(mainViewModel)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            _mainWindow.Closed += OnMainWindowClosed;
            _mainWindow.Show();
        }

        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            _mainWindow.Closed -= OnMainWindowClosed;
            Show();
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = (sender as PasswordBox).Password;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _loginViewModel.LoginSuccess -= OnLoginSuccess;
            _loginViewModel.LoginFailure -= OnLoginFailure;
        }
    }
}
