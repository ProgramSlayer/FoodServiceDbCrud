using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditExistingDishIngredientWindow : Window
    {
        private readonly EditExistingDishIngredientViewModel _editExistingDishIngredientViewModel;

        public EditExistingDishIngredientWindow(EditExistingDishIngredientViewModel editExistingDishIngredientViewModel)
        {
            _editExistingDishIngredientViewModel = editExistingDishIngredientViewModel ?? throw new System.ArgumentNullException(nameof(editExistingDishIngredientViewModel));
            DataContext = _editExistingDishIngredientViewModel;
            _editExistingDishIngredientViewModel.SubmitError += OnSubmitError;
            _editExistingDishIngredientViewModel.SubmitSuccess += OnSubmitSuccess;
            _editExistingDishIngredientViewModel.CancelSubmit += OnCancelSubmit;
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
            _editExistingDishIngredientViewModel.SubmitError -= OnSubmitError;
            _editExistingDishIngredientViewModel.SubmitSuccess -= OnSubmitSuccess;
            _editExistingDishIngredientViewModel.CancelSubmit -= OnCancelSubmit;
        }
    }
}
