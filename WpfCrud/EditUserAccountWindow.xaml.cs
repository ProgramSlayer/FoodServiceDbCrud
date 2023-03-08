using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditUserAccountWindow : Window
    {
        private readonly EditUserAccountViewModel _editUserAccountViewModel;

        public EditUserAccountWindow(EditUserAccountViewModel editUserAccountViewModel)
        {
            _editUserAccountViewModel = editUserAccountViewModel ?? throw new ArgumentNullException(nameof(editUserAccountViewModel));
            DataContext = _editUserAccountViewModel;
            _editUserAccountViewModel.SubmitError += OnSubmitError;
            _editUserAccountViewModel.SubmitSuccess += OnSubmitSuccess;
            _editUserAccountViewModel.CancelSubmit += OnCancelSubmit;
            InitializeComponent();
        }

        private void OnCancelSubmit()
        {
            DialogResult = false;
            Close();
        }

        private void OnSubmitSuccess()
        {
            DialogResult = true;
            Close();
        }

        private void OnSubmitError(Exception e)
        {
            MessageBox.Show(e.Message, e.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _editUserAccountViewModel.SubmitError -= OnSubmitError;
            _editUserAccountViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editUserAccountViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
