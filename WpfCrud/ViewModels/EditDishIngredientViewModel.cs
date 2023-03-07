using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class AddDishIngredientViewModel : ViewModelBase
    {
        private readonly EditableDishIngredient _di;

        public IEnumerable<ViewDish> Dishes { get; }
        public IEnumerable<ViewProduct> Products { get; }
        public int DishId
        {
            get => _di.DishId;
            set
            {
                _di.DishId = value;
                OnPropertyChanged(nameof(DishId));
            }
        }
        public int ProductId
        {
            get => _di.ProductId;
            set
            {
                _di.ProductId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        public double RequiredWeightGrams
        {
            get => _di.RequiredWeightGrams; 
            set
            {
                _di.RequiredWeightGrams = value;
                OnPropertyChanged(nameof(RequiredWeightGrams));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action<Exception> SubmitError;
        public event Action SubmitSuccess;
        public event Action CancelSubmit;

        protected void OnSubmitError(Exception e) => SubmitError?.Invoke(e);
        protected void OnSubmitSuccess() => SubmitSuccess?.Invoke();
        protected void OnCancelSubmit() => CancelSubmit?.Invoke();

        public AddDishIngredientViewModel(EditableDishIngredient di, IEnumerable<ViewDish> dishes, IEnumerable<ViewProduct> products)
        {
            if (di is null)
            {
                throw new ArgumentNullException(nameof(di));
            }

            if (dishes is null)
            {
                throw new ArgumentNullException(nameof(dishes));
            }

            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (!dishes.Any())
            {
                throw new InvalidOperationException("Список блюд для выбора пуст!");
            }

            if (!products.Any())
            {
                throw new InvalidOperationException("Список продуктов для выбора пуст!");
            }

            _di = di;
            Dishes = dishes;
            Products = products;
            
            if (_di.DishId == 0)
            {
                DishId = Dishes.First().Id;
            }

            if (_di.ProductId == 0)
            {
                ProductId = Products.First().Id;
            }

            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            OnCancelSubmit();
        }

        private void Submit(object obj)
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
            OnSubmitSuccess();
        }

        private void ValidateProperties()
        {
            if (DishId == 0)
            {
                throw new Exception("Блюдо должно быть выбрано!");
            }

            if (ProductId == 0)
            {
                throw new Exception("Продукт должен быть выбран!");
            }

            if (RequiredWeightGrams <= 0d)
            {
                throw new Exception("Требуемая масса продукта (г) должна быть положительным числом!");
            }
        }
    }
}