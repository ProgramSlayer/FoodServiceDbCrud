using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditProductWindow : Window
    {
        private readonly EditProductViewModel _editProductViewModel;

        public EditProductWindow(EditProductViewModel editProductViewModel)
        {
            _editProductViewModel = editProductViewModel ?? throw new ArgumentNullException(nameof(editProductViewModel));
            DataContext = _editProductViewModel;
            _editProductViewModel.SubmitError += OnSubmitError;
            _editProductViewModel.SubmitSuccess += OnSubmitSuccess;
            _editProductViewModel.CancelSubmit += OnCancelSubmit;
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

        private void OnSubmitError(Exception obj)
        {
            MessageBox.Show(obj.Message, obj.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _editProductViewModel.SubmitError -= OnSubmitError;
            _editProductViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editProductViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
