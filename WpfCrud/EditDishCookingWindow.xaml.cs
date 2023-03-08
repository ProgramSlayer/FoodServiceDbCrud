using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditDishCookingWindow : Window
    {
        private readonly EditDishCookingViewModel _editDishCookingViewModel;

        public EditDishCookingWindow(EditDishCookingViewModel editDishCookingViewModel)
        {
            _editDishCookingViewModel = editDishCookingViewModel ?? throw new ArgumentNullException(nameof(editDishCookingViewModel));
            DataContext = _editDishCookingViewModel;
            _editDishCookingViewModel.SubmitError += OnSubmitError;
            _editDishCookingViewModel.SubmitSuccess += OnSubmitSuccess;
            _editDishCookingViewModel.CancelSubmit += OnCancelSubmit;
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
            _editDishCookingViewModel.SubmitError -= OnSubmitError;
            _editDishCookingViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editDishCookingViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
