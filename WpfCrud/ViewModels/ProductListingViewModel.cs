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
    public class ProductListingViewModel : ViewModelBase
    {
        private readonly ProductService _productService = new ProductService();
        private readonly ObservableCollection<ViewProduct> _filteredProducts = new ObservableCollection<ViewProduct>();
        public ReadOnlyObservableCollection<ViewProduct> FilteredProducts { get; }

        private string _productNameFilter;

        public string ProductNameFilter
        {
            get => _productNameFilter;
            set
            {
                _productNameFilter = value;
                OnPropertyChanged(nameof(ProductNameFilter));
            }
        }

        public ICommand FilterProductsCommand { get; }

        private async Task FilterProductsAsync()
        {
            var filteredProducts = await _productService.GetViewProductsByNameAsync(ProductNameFilter);
            _filteredProducts.Clear();
            foreach (var fp in filteredProducts)
            {
                _filteredProducts.Add(fp);
            }
        }

        private void HandleException(Exception obj)
        {
            MessageBox.Show($"{obj.GetType().Name} : {obj.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public ProductListingViewModel()
        {
            FilteredProducts = new ReadOnlyObservableCollection<ViewProduct>(_filteredProducts);
            FilterProductsCommand = new AsyncDelegateCommand(FilterProductsAsync, HandleException);
        }

        public ProductListingViewModel(ProductService productService) : this()
        {
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }
    }
}
