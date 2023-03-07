using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.Dishes;
using WpfCrud.Services.DishIngredients;
using WpfCrud.Services.Products;

namespace WpfCrud.ViewModels
{
    public class DishIngredientListingCrudViewModel : ViewModelBase
    {
        private readonly DishIngredientService _dishIngredientService = new DishIngredientService();
        private readonly DishService _dishService = new DishService();
        private readonly ProductService _productService = new ProductService();
        private readonly ObservableCollection<ViewDishIngredient> _dishIngredients = new ObservableCollection<ViewDishIngredient>();
        private ViewDishIngredient selectedDishIngredient;

        public DishIngredientListingCrudViewModel()
        {
            DishIngredients = new ReadOnlyObservableCollection<ViewDishIngredient>(_dishIngredients);
            LoadDishIngredientsCommand = new AsyncDelegateCommand(LoadDishIngredientsAsync, HandleException);
            AddDishIngredientCommand = new AsyncDelegateCommand(AddDishIngredientAsync, HandleException);
            EditDishIngredientCommand = new AsyncDelegateCommand(EditDishIngredientAsync, HandleException);
            DeleteDishIngredientCommand = new AsyncDelegateCommand(DeleteDishIngredientAsync, HandleException);
        }

        private async Task DeleteDishIngredientAsync()
        {
            if (SelectedDishIngredient is null)
            {
                return;
            }

            await _dishIngredientService.DeleteDishIngredientAsync(SelectedDishIngredient.DishId, SelectedDishIngredient.ProductId);
        }

        private bool? AddDishIngredientWindowShowDialog(
            EditableDishIngredient di,
            IEnumerable<ViewDish> dishes,
            IEnumerable<ViewProduct> products)
        {
            var editDishIngredientViewModel = new AddDishIngredientViewModel(di, dishes, products);
            var editDishIngredientWindow = new AddDishIngredientWindow(editDishIngredientViewModel);
            var result = editDishIngredientWindow.ShowDialog();
            return result;
        }

        private bool? EditExistingDishIngredientWindowShowDialog(
            EditableDishIngredient di)
        {
            var editExistingDishIngredientViewModel = new EditExistingDishIngredientViewModel(di);
            var editExistingDishIngredientWindow = new EditExistingDishIngredientWindow(editExistingDishIngredientViewModel);
            var result = editExistingDishIngredientWindow.ShowDialog();
            return result;
        }

        private async Task EditDishIngredientAsync()
        {
            if (SelectedDishIngredient is null)
            {
                return;
            }

            var di = new EditableDishIngredient(
                SelectedDishIngredient.DishId,
                SelectedDishIngredient.ProductId,
                SelectedDishIngredient.RequiredWeightGrams);

            var dialogResult = EditExistingDishIngredientWindowShowDialog(di);
            if (dialogResult != true)
            {
                return;
            }

            await _dishIngredientService.UpdateDishIngredientAsync(di);
            await LoadDishIngredientsAsync();
        }

        private async Task AddDishIngredientAsync()
        {
            var di = new EditableDishIngredient();
            var dishes = await _dishService.GetDishInfosAsync();
            var products = await _productService.GetViewProductsAsync();
            var dialogResult = AddDishIngredientWindowShowDialog(di, dishes, products);

            if (dialogResult != true)
            {
                return;
            }

            _ = await _dishIngredientService.AddDishIngredientAsync(di);
            await LoadDishIngredientsAsync();
        }

        public DishIngredientListingCrudViewModel(DishIngredientService dishIngredientService, DishService dishService, ProductService productService) : this()
        {
            _dishIngredientService = dishIngredientService ?? throw new System.ArgumentNullException(nameof(dishIngredientService));
            _dishService = dishService ?? throw new System.ArgumentNullException(nameof(dishService));
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }


        private async Task LoadDishIngredientsAsync()
        {
            var dishIngredients = await _dishIngredientService.GetViewDishIngredientsAsync();
            _dishIngredients.Clear();
            foreach (var di in dishIngredients)
            {
                _dishIngredients.Add(di);
            }
        }

        private void HandleException(Exception e)
        {
            MessageBox.Show($"{e.GetType().Name} : {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public ReadOnlyObservableCollection<ViewDishIngredient> DishIngredients { get; }
        public ViewDishIngredient SelectedDishIngredient
        {
            get => selectedDishIngredient; 
            set
            {
                selectedDishIngredient = value;
                OnPropertyChanged(nameof(SelectedDishIngredient));
            }
        }
        public ICommand LoadDishIngredientsCommand { get; }
        public ICommand AddDishIngredientCommand { get; }
        public ICommand EditDishIngredientCommand { get; }
        public ICommand DeleteDishIngredientCommand { get; }
    }
}