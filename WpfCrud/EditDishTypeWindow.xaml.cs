using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditDishTypeWindow : Window
    {
        private readonly EditDishTypeViewModel _editDishTypeViewModel;

        public EditDishTypeWindow(EditDishTypeViewModel editDishTypeViewModel)
        {
            _editDishTypeViewModel = editDishTypeViewModel ?? throw new ArgumentNullException(nameof(editDishTypeViewModel));
            DataContext = _editDishTypeViewModel;
            _editDishTypeViewModel.SubmitError += OnSubmitError;
            _editDishTypeViewModel.SubmitSuccess += OnSubmitSuccess;
            _editDishTypeViewModel.CancelSubmit += OnCancelSubmit;
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
            _editDishTypeViewModel.SubmitError -= OnSubmitError;
            _editDishTypeViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editDishTypeViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
