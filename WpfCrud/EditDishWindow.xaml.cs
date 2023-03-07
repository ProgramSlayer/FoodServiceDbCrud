using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditDishWindow : Window
    {
        private readonly EditDishViewModel _editDishViewModel;

        public EditDishWindow(EditDishViewModel editDishViewModel)
        {
            _editDishViewModel = editDishViewModel ?? throw new ArgumentNullException(nameof(editDishViewModel));
            DataContext = _editDishViewModel;
            _editDishViewModel.SubmitError += OnSubmitError;
            _editDishViewModel.SubmitSuccess += OnSubmitSuccess;
            _editDishViewModel.CancelSubmit += OnCancelSubmit;
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
            _editDishViewModel.SubmitError -= OnSubmitError;
            _editDishViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editDishViewModel.CancelSubmit -= OnCancelSubmit;
        }

    }
}
