using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.DbModels;
using WpfCrud.Models;
using WpfCrud.Models.Enums;

namespace WpfCrud.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public const string CorrectCaptcha = "1337";
        private string _password;
        private string _captcha;
        private readonly CurrentUser _currentUser = new CurrentUser();

        public string Login
        {
            get => _currentUser.Login;
            set
            {
                _currentUser.Login = value;
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public int CurrentUserId
        {
            get => _currentUser.Id; 
            set
            {
                _currentUser.Id = value;
                OnPropertyChanged(nameof(CurrentUserId));
            }
        }

        public UserRoleEnum CurrentUserRole
        {
            get => _currentUser.Role; 
            set
            {
                _currentUser.Role = value;
                OnPropertyChanged(nameof(CurrentUserRole));
            }
        }

        public ICommand LoginCommand { get; }

        public event Action LoginSuccess;
        public event Action<string> LoginFailure;
        private void OnLoginSuccess() => LoginSuccess?.Invoke();
        private void OnLoginFailure(string reason) => LoginFailure?.Invoke(reason);    

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Submit, null);
        }

        public bool CanSubmit =>
            !string.IsNullOrWhiteSpace(Login)
            && !string.IsNullOrWhiteSpace(Password)
            && Captcha == CorrectCaptcha;

        public string Captcha
        {
            get => _captcha; 
            set
            {
                _captcha = value;
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private void Submit(object obj)
        {
            UserAccount foundUser = null;
            try
            {
                var loginParam = new SqlParameter("@login", SqlDbType.NVarChar, 100) { Value = Login };
                var passwordParam = new SqlParameter(@"password", SqlDbType.VarChar) { Value = Password };
                using (var context = new FoodServiceDbContext())
                {
                    foundUser = context
                        .UserAccounts
                        .SqlQuery(
                            "SELECT * FROM UserAccount WHERE Login = @Login AND Password = HASHBYTES('sha2_512', @Password)",
                            loginParam,
                            passwordParam)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                OnLoginFailure($"{e.GetType().Name} : {e.Message}");
                return;
            }

            if (foundUser is null)
            {
                OnLoginFailure("Логин и/или пароль введены неверно.");
                return;
            }

            CurrentUserId = foundUser.Id;
            CurrentUserRole = (UserRoleEnum)foundUser.UserRoleId;
            OnLoginSuccess();
        }
    }
}