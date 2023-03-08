using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Models.Enums;

namespace WpfCrud.ViewModels
{
    public class EditUserAccountViewModel : ViewModelBase
    {
        private static readonly UserRoleEnum[] _userRoles =
        {
            UserRoleEnum.Cook,
            UserRoleEnum.Chef,
            UserRoleEnum.Admin,
        };

        private readonly EditableUserAccount _userAccount;
        public static UserRoleEnum[] UserRoles => _userRoles;

        public EditUserAccountViewModel(EditableUserAccount userAccount)
        {
            _userAccount = userAccount ?? throw new ArgumentNullException(nameof(userAccount));
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(Cancel);
            SelectImageCommand = new DelegateCommand(SelectImage);
            DeselectImageCommand = new DelegateCommand(_ => Image = null);
        }

        private void SelectImage(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png, *.jpeg, *.jpg)|*.png;*.jpeg;*.jpg",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                ValidateNames = true
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var imagePath = dialog.FileName;
            var imageData = File.ReadAllBytes(imagePath);
            Image = imageData;
        }

        private void Cancel(object obj)
        {
            OnCancelSubmit();
        }

        private void Submit(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Login))
                {
                    throw new Exception("Логин должен быть заполнен!");
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("Пароль должен быть заполнен!");
                }

                if (UserRole == UserRoleEnum.Unauthorized)
                {
                    throw new Exception("Роль пользователя в системе должна быть выбрана!");
                }
            }
            catch (Exception e)
            {
                OnSubmitError(e);
                return;
            }
            OnSubmitSuccess();
        }

        public int Id => _userAccount.Id;
        public string Login
        {
            get => _userAccount.Login;
            set
            {
                _userAccount.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _userAccount.Password;
            set
            {
                _userAccount.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public UserRoleEnum UserRole
        {
            get => _userAccount.Role;
            set
            {
                _userAccount.Role = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }

        public byte[] Image
        {
            get => _userAccount.Image; 
            set
            {
                _userAccount.Image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ICommand DeselectImageCommand { get; }
        public event Action<Exception> SubmitError;
        public event Action SubmitSuccess;
        public event Action CancelSubmit;
        protected void OnSubmitError(Exception e) => SubmitError?.Invoke(e);
        protected void OnSubmitSuccess() => SubmitSuccess?.Invoke();
        protected void OnCancelSubmit() => CancelSubmit?.Invoke();
    }
}