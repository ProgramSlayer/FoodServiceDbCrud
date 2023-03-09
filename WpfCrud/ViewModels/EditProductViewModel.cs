using System;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditProductViewModel : ViewModelBase
    {
        private readonly EditableProduct _editableProduct;

        public EditProductViewModel(EditableProduct editableProduct)
        {
            _editableProduct = editableProduct ?? throw new System.ArgumentNullException(nameof(editableProduct));
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public void Cancel(object obj)
        {
            OnCancelSubmit();
        }

        public void Submit(object obj)
        {
            try
            {
                ValidateProperties();
            }
            catch (Exception e)
            {
                OnSubmitError(e);
                return;
            }
            OnSubmitSucces();
        }

        public void ValidateProperties()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new Exception("Наименование должно быть заполнено!");
            }

            if (CaloricContentPer100Grams <= 0d)
            {
                throw new Exception("Калорийность (ккал / 100 г) должна быть положительным числом!");
            }

            if (WeightGrams <= 0d)
            {
                throw new Exception("Масса (г) должна быть положительным числом!");
            }

            if (PricePerKilogramRoubles <= 0m)
            {
                throw new Exception("Цена за 1 кг должна быть положительным числом!");
            }
        }

        public int Id => _editableProduct.Id;
        public string Name
        {
            get => _editableProduct.Name;
            set
            {
                _editableProduct.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public double CaloricContentPer100Grams
        {
            get => _editableProduct.CaloricContentPer100Grams;
            set
            {
                _editableProduct.CaloricContentPer100Grams = value;
                OnPropertyChanged(nameof(CaloricContentPer100Grams));
            }
        }
        public double WeightGrams
        {
            get => _editableProduct.WeightGrams; 
            set
            {
                _editableProduct.WeightGrams = value;
                OnPropertyChanged(nameof(WeightGrams));
            }
        }
        public decimal PricePerKilogramRoubles
        {
            get => _editableProduct.PricePerKilogramRoubles; 
            set
            {
                _editableProduct.PricePerKilogramRoubles = value;
                OnPropertyChanged(nameof(PricePerKilogramRoubles));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public event Action<Exception> SubmitError;
        public event Action SubmitSuccess;
        public event Action CancelSubmit;

        protected void OnSubmitError(Exception e) => SubmitError?.Invoke(e);
        protected void OnSubmitSucces() => SubmitSuccess?.Invoke();
        protected void OnCancelSubmit() => CancelSubmit?.Invoke();
    }
}