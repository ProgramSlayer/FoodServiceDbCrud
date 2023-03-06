using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services;
using WpfCrud.Services.Products;

namespace WpfCrud.ViewModels
{
    public class ProductListingCrudViewModel : ViewModelBase
    {
        private readonly ProductService _productService = new ProductService();
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();

        private Product _selectedProduct;

        public ReadOnlyObservableCollection<Product> Products { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public Product SelectedProduct
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
            Products = new ReadOnlyObservableCollection<Product>(_products);
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

        private async Task EditProductAsync()
        {
            throw new NotImplementedException();
            //if (SelectedProduct is null)
            //{
            //    return;
            //}

            //var editProduct = new Product
            //{
            //    Id = SelectedProduct.Id,
            //    CaloricContentPer100Grams = SelectedProduct.CaloricContentPer100Grams,
            //    Name = SelectedProduct.Name,
            //    PricePerKilogramRoubles = SelectedProduct.PricePerKilogramRoubles,
            //    WeightGrams = SelectedProduct.WeightGrams,
            //};

            //var editableProductViewModel = new EditableProductViewModel(editProduct);
            //var dialogResult = _editEntityWindowDialogService.ShowDialog(editableProductViewModel);
            //if (dialogResult != true)
            //{
            //    return;
            //}
            //await _productService.UpdateProductAsync(editProduct);
            //var index = _products.IndexOf(SelectedProduct);
            //await LoadProductsAsync();
        }

        private async Task AddProductAsync()
        {
            throw new NotImplementedException();
            //var product = new Product();
            //var editableProductViewModel = new EditableProductViewModel(product);
            //bool? dialogResult = _editEntityWindowDialogService.ShowDialog(editableProductViewModel);
            //if (dialogResult != true)
            //{
            //    return;
            //}
            //_ = await _productService.AddProductAsync(product);
            //await LoadProductsAsync();
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
            var products = await _productService.GetProductsAsync();
            _products.Clear();
            foreach (var p in products)
            {
                _products.Add(p);
            }
        }

    }
}