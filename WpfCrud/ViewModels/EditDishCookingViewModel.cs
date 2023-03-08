using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditDishCookingViewModel : ViewModelBase
    {
        private readonly EditableDishCooking _dishCooking;
        public IEnumerable<ViewDish> Dishes { get; }

        public EditDishCookingViewModel(EditableDishCooking dishCooking, IEnumerable<ViewDish> dishes)
        {
            if (dishCooking is null)
            {
                throw new ArgumentNullException(nameof(dishCooking));
            }

            if (dishes is null)
            {
                throw new ArgumentNullException(nameof(dishes));
            }

            if (!dishes.Any())
            {
                throw new InvalidOperationException("Список блюд для выбора пуст!");
            }

            _dishCooking = dishCooking;
            Dishes = dishes;

            if (DishId == 0)
            {
                DishId = Dishes.First().Id;
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
                if (DishId == 0)
                {
                    throw new Exception("Блюдо должно быть выбрано!");
                }

                if (Count <= 0)
                {
                    throw new Exception("Количество блюд должно быть положительным числом!");
                }
            }
            catch (Exception e)
            {
                OnSubmitError(e);
                return;
            }
            OnSubmitSuccess();
        }

        public int DishId
        {
            get => _dishCooking.DishId;
            set
            {
                _dishCooking.DishId = value;
                OnPropertyChanged(nameof(DishId));
            }
        }

        public int Count
        {
            get => _dishCooking.Count;
            set
            {
                _dishCooking.Count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public DateTime CookedAt
        {
            get => _dishCooking.CookedAt;
            set
            {
                _dishCooking.CookedAt = value;
                OnPropertyChanged(nameof(CookedAt));
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