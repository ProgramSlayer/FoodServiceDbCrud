using System;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.ViewModels;

namespace WpfCrud.ViewModels
{
    public class EditExistingDishIngredientViewModel : ViewModelBase
    {
        private readonly EditableDishIngredient _di;

        public EditExistingDishIngredientViewModel(EditableDishIngredient di)
        {
            if (di is null)
            {
                throw new ArgumentNullException(nameof(di));
            }

            if (di.DishId == 0)
            {
                throw new InvalidOperationException("di.DishId is 0");
            }

            if (di.ProductId == 0)
            {
                throw new InvalidOperationException("di.ProductId is 0");
            }

            _di = di;
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
                if (RequiredWeightGrams <= 0)
                {
                    throw new Exception("Требуемая масса продукта (г) должна быть положительным числом!");
                }
            }
            catch (Exception e)
            {
                OnSubmitError(e);
                return;
            }
            OnSubmitSuccess();
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
    }
}