using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditDishViewModel : ViewModelBase
    {
        private readonly EditableDish _dish;
        public IEnumerable<DishType> DishTypes { get; }

        public EditDishViewModel(EditableDish dish, IEnumerable<DishType> dishTypes)
        {
            if (dish is null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            if (dishTypes is null)
            {
                throw new ArgumentNullException(nameof(dishTypes));
            }

            if (!dishTypes.Any())
            {
                throw new InvalidOperationException($"Список типов блюд <{nameof(dishTypes)}> пуст!");
            }

            _dish = dish;
            DishTypes = dishTypes;
            if (DishTypeId == 0)
            {
                DishTypeId = DishTypes.First().Id;
            }
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(Cancel);
            SelectDishImageCommand = new DelegateCommand(SelectDishImage);
            DeselectDishImageCommand = new DelegateCommand(_ => Image = null);
        }

        private void SelectDishImage(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png, *.jpeg, *.jpg)|*.png;*.jpeg;*.jpg",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                ValidateNames = true
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var imagePath = dialog.FileName;
            var imageData = File.ReadAllBytes(imagePath);
            Image = imageData;
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

            OnSubmitSuccess();
        }

        public void ValidateProperties()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new Exception("Название блюда должно быть заполнено!");
            }

            if (DishTypeId == 0)
            {
                throw new Exception("Тип блюда должен быть выбран!");
            }

            if (CookingTimeMinutes <= 0d)
            {
                throw new Exception("Время готовки блюда (минут) должно быть положительным числом!");
            }

            if (WeightGrams <= 0d)
            {
                throw new Exception("Масса готового блюда (г) должна быть положительным числом!");
            }

            if (string.IsNullOrEmpty(Recipe))
            {
                throw new Exception("Рецепт блюда должен быть заполнен!");
            }
        }

        protected void OnSubmitError(Exception e) => SubmitError?.Invoke(e);
        protected void OnSubmitSuccess() => SubmitSuccess?.Invoke();
        protected void OnCancelSubmit() => CancelSubmit?.Invoke();

        public int Id => _dish.Id;

        public string Name
        {
            get => _dish.Name;
            set
            {
                _dish.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int DishTypeId
        {
            get => _dish.DishTypeId;
            set
            {
                _dish.DishTypeId = value;
                OnPropertyChanged(nameof(DishTypeId));
            }
        }

        //public DishType DishType
        //{
        //    get => _dish.DishType;
        //    set
        //    {
        //        _dish.DishType = value;
        //        OnPropertyChanged(nameof(DishType));
        //    }
        //}

        public double CookingTimeMinutes
        {
            get => _dish.CookingTimeMinutes; 
            set
            {
                _dish.CookingTimeMinutes = value;
                OnPropertyChanged(nameof(CookingTimeMinutes));
            }
        }

        public double WeightGrams
        {
            get => _dish.WeightGrams;
            set
            {
                _dish.WeightGrams = value;
                OnPropertyChanged(nameof(WeightGrams));
            }
        }

        public string Recipe
        {
            get => _dish.Recipe; 
            set
            {
                _dish.Recipe = value;
                OnPropertyChanged(nameof(Recipe));
            }
        }

        public byte[] Image
        {
            get => _dish.Image; 
            set
            {
                _dish.Image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectDishImageCommand { get; }
        public ICommand DeselectDishImageCommand { get; }
        public event Action<Exception> SubmitError;
        public event Action SubmitSuccess;
        public event Action CancelSubmit;
    }
}