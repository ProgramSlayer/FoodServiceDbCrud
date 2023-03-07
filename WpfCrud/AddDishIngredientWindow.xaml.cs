using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class AddDishIngredientWindow : Window
    {
        private readonly AddDishIngredientViewModel _editDishIngredientViewModel;

        public AddDishIngredientWindow(AddDishIngredientViewModel editDishIngredientViewModel)
        {
            _editDishIngredientViewModel = editDishIngredientViewModel ?? throw new ArgumentNullException(nameof(editDishIngredientViewModel));
            DataContext = editDishIngredientViewModel;
            _editDishIngredientViewModel.SubmitError += OnSubmitError;
            _editDishIngredientViewModel.SubmitSuccess += OnSubmitSuccess;
            _editDishIngredientViewModel.CancelSubmit += OnCancelSubmit;
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
            _editDishIngredientViewModel.SubmitError -= OnSubmitError;
            _editDishIngredientViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editDishIngredientViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
