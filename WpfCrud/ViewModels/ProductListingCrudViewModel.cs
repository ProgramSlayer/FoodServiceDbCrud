using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.Products;

namespace WpfCrud.ViewModels
{
    public class ProductListingCrudViewModel : ViewModelBase
    {
        private readonly ProductService _productService = new ProductService();
        private readonly ObservableCollection<ViewProduct> _products = new ObservableCollection<ViewProduct>();

        private ViewProduct _selectedProduct;

        public ReadOnlyObservableCollection<ViewProduct> Products { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ViewProduct SelectedProduct
        {
            get => _selectedProduct; 
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ProductListingCrudViewModel()
        {
            Products = new ReadOnlyObservableCollection<ViewProduct>(_products);
            LoadProductsCommand = new AsyncDelegateCommand(LoadProductsAsync, HandleException);
            AddProductCommand = new AsyncDelegateCommand(AddProductAsync, HandleException);
            EditProductCommand = new AsyncDelegateCommand(EditProductAsync, HandleException);
            DeleteProductCommand = new AsyncDelegateCommand(DeleteProductAsync, HandleException);
        }

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct is null)
            {
                return;
            }

            await _productService.DeleteProductAsync(SelectedProduct.Id);
            await LoadProductsAsync();
        }

        private bool? EditProductWindowShowDialog(EditableProduct product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var editProductViewModel = new EditProductViewModel(product);
            var editProductWindow = new EditProductWindow(editProductViewModel);
            var result = editProductWindow.ShowDialog();
            return result;
        }

        private async Task EditProductAsync()
        {
            if (SelectedProduct is null)
            {
                return;
            }

            var product = new EditableProduct(
                SelectedProduct.Id,
                SelectedProduct.Name,
                SelectedProduct.CaloricContentPer100Grams,
                SelectedProduct.WeightGrams,
                SelectedProduct.PricePerKilogramRoubles);

            var dialogResult = EditProductWindowShowDialog(product);
            if (dialogResult != true)
            {
                return;
            }
            await _productService.UpdateProductAsync(product);
            await LoadProductsAsync();
        }

        private async Task AddProductAsync()
        {
            var product = new EditableProduct();
            var dialogResult = EditProductWindowShowDialog(product);
            if (dialogResult != true)
            {
                return;
            }
            _ = await _productService.AddProductAsync(product);
            await LoadProductsAsync();
        }

        private void HandleException(Exception obj)
        {
            MessageBox.Show(
                $"{obj.GetType().Name} : {obj.Message}",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private async Task LoadProductsAsync()
        {
            var products = await _productService.GetViewProductsAsync();
            _products.Clear();
            foreach (var p in products)
            {
                _products.Add(p);
            }
        }

    }
}