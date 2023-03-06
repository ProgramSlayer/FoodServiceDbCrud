using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditableProductViewModel : ViewModelBase
    {
        private readonly Product _product = new Product();

        public EditableProductViewModel()
        {
        }

        public EditableProductViewModel(Product product)
        {
            _product = product ?? throw new System.ArgumentNullException(nameof(product));
        }

        public int Id => _product.Id;
        public string Name
        {
            get => _product.Name;
            set
            {
                _product.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double CaloricContentPer100Grams
        {
            get => _product.CaloricContentPer100Grams;
            set
            {
                _product.CaloricContentPer100Grams = value;
                OnPropertyChanged(nameof(CaloricContentPer100Grams));
            }
        }
        public double WeightGrams
        {
            get => _product.WeightGrams; 
            set
            {
                _product.WeightGrams = value;
                OnPropertyChanged(nameof(WeightGrams));
            }
        }

        public decimal PricePerKilogramRoubles
        {
            get => _product.PricePerKilogramRoubles; 
            set
            {
                _product.PricePerKilogramRoubles = value;
                OnPropertyChanged(nameof(PricePerKilogramRoubles));
            }
        }
    }
}