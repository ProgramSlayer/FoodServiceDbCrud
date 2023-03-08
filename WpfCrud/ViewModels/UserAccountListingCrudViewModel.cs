using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.UserAccounts;

namespace WpfCrud.ViewModels
{
    public class UserAccountListingCrudViewModel : ViewModelBase
    {
        private readonly CurrentUser _currentUser;
        private readonly UserAccountService _userAccountService = new UserAccountService();
        private readonly ObservableCollection<ViewUserAccount> _userAccounts = new ObservableCollection<ViewUserAccount>();
        private ViewUserAccount _selectedUserAccount;

        public UserAccountListingCrudViewModel(CurrentUser currentUser)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            UserAccounts = new ReadOnlyObservableCollection<ViewUserAccount>(_userAccounts);
            LoadUserAccountsCommand = new AsyncDelegateCommand(LoadUserAccountsAsync, HandleException);
            AddUserAccountCommand = new AsyncDelegateCommand(AddUserAccountAsync, HandleException);
            EditUserAccountCommand = new AsyncDelegateCommand(EditUserAccountAsync, HandleException);
            DeleteUserAccountCommand = new AsyncDelegateCommand(DeleteUserAccountAsync, HandleException);
        }

        private async Task DeleteUserAccountAsync()
        {
            if (SelectedUserAccount is null)
            {
                return;
            }

            if (SelectedUserAccount.Id == _currentUser.Id)
            {
                throw new Exception("Удаление текущего пользователя системы запрещено!");
            }

            await _userAccountService.DeleteUserAccountAsync(SelectedUserAccount.Id);
            await LoadUserAccountsAsync();
        }

        private bool? EditUserAccountWindowShowDialog(EditableUserAccount editableUserAccount)
        {
            var editUserAccountViewModel = new EditUserAccountViewModel(editableUserAccount);
            var editUserAccountWindow = new EditUserAccountWindow(editUserAccountViewModel);
            var result = editUserAccountWindow.ShowDialog();
            return result;
        }

        private async Task EditUserAccountAsync()
        {
            if (SelectedUserAccount is null)
            {
                return;
            }

            if (SelectedUserAccount.Id == _currentUser.Id)
            {
                throw new Exception("Редактирование текущего пользователя системы не реализовано!");
            }

            var userAccount = new EditableUserAccount(
                SelectedUserAccount.Id,
                SelectedUserAccount.Login,
                null,
                SelectedUserAccount.Role,
                SelectedUserAccount.Image);
            var dialogResult = EditUserAccountWindowShowDialog(userAccount);
            if (dialogResult != true)
            {
                return;
            }

            await _userAccountService.UpdateUserAccountAsync(userAccount);
            await LoadUserAccountsAsync();
        }

        private async Task AddUserAccountAsync()
        {
            var userAccount = new EditableUserAccount();
            var dialogResult = EditUserAccountWindowShowDialog(userAccount);
            if (dialogResult != true)
            {
                return;
            }

            _ = await _userAccountService.AddUserAccountAsync(userAccount);
            await LoadUserAccountsAsync();
        }

        private void HandleException(Exception obj)
        {
            MessageBox.Show($"{obj.GetType().Name} : {obj.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task LoadUserAccountsAsync()
        {
            var userAccounts = await _userAccountService.GetViewUserAccountsAsync();
            _userAccounts.Clear();
            foreach (var ua in userAccounts)
            {
                _userAccounts.Add(ua);
            }
        }

        public UserAccountListingCrudViewModel(CurrentUser currentUser, UserAccountService userAccountService) : this(currentUser)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public ReadOnlyObservableCollection<ViewUserAccount> UserAccounts { get; }
        public ViewUserAccount SelectedUserAccount
        {
            get => _selectedUserAccount; 
            set
            {
                _selectedUserAccount = value;
                OnPropertyChanged(nameof(SelectedUserAccount));
            }
        }
        public ICommand LoadUserAccountsCommand { get; }
        public ICommand AddUserAccountCommand { get; }
        public ICommand EditUserAccountCommand { get; }
        public ICommand DeleteUserAccountCommand { get; }
    }
}