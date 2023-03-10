using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.Dishes;
using WpfCrud.Services.DishTypes;

namespace WpfCrud.ViewModels
{
    public class DishListingCrudViewModel : ViewModelBase
    {
        private readonly DishService _dishService = new DishService();
        private readonly DishTypeService _dishTypeService = new DishTypeService();
        private readonly ObservableCollection<ViewDish> _dishes = new ObservableCollection<ViewDish>();
        private ViewDish _selectedDish;
        public ViewDish SelectedDish
        {
            get => _selectedDish;
            set
            {
                _selectedDish = value;
                OnPropertyChanged(nameof(SelectedDish));
            }
        }

        public ReadOnlyObservableCollection<ViewDish> Dishes { get; }
        public ICommand LoadDishesCommand { get; }
        public ICommand AddDishCommand { get; }
        public ICommand EditDishCommand { get; }
        public ICommand DeleteDishCommand { get; }

        public DishListingCrudViewModel()
        {
            Dishes = new ReadOnlyObservableCollection<ViewDish>(_dishes);
            LoadDishesCommand = new AsyncDelegateCommand(LoadDishesAsync, HandleException);
            AddDishCommand = new AsyncDelegateCommand(AddDishAsync, HandleException);
            EditDishCommand = new AsyncDelegateCommand(EditDishAsync, HandleException);
            DeleteDishCommand = new AsyncDelegateCommand(DeleteDishAsync, HandleException);
        }

        private async Task DeleteDishAsync()
        {
            if (SelectedDish is null)
            {
                return;
            }

            await _dishService.DeleteDishAsync(SelectedDish.Id);
            await LoadDishesAsync();
        }

        private bool? EditDishWindowShowDialog(EditableDish dish, IEnumerable<DishType> dishTypes)
        {
            var editDishViewModel = new EditDishViewModel(dish, dishTypes);
            var editDishWindow = new EditDishWindow(editDishViewModel);
            var result = editDishWindow.ShowDialog();
            return result;
        }

        private async Task EditDishAsync()
        {
            if (SelectedDish is null)
            {
                return;
            }

            var dishTypes = await _dishTypeService.GetDishTypesAsync();

            var dish = new EditableDish(
                SelectedDish.Id,
                SelectedDish.Name,
                SelectedDish.DishTypeId,
                SelectedDish.CookingTimeMinutes,
                SelectedDish.WeightGrams,
                SelectedDish.Recipe,
                SelectedDish.Image);

            var dialogResult = EditDishWindowShowDialog(dish, dishTypes);
            if (dialogResult != true)
            {
                return;
            }

            await _dishService.UpdateDishAsync(dish);
            await LoadDishesAsync();
        }
        private async Task AddDishAsync()
        {
            var dishTypes = await _dishTypeService.GetDishTypesAsync();
            var dish = new EditableDish();
            var dialogResult = EditDishWindowShowDialog(dish, dishTypes);
            if (dialogResult != true)
            {
                return;
            }
            _ = await _dishService.AddDishAsync(dish);
            await LoadDishesAsync();
        }

        private async Task LoadDishesAsync()
        {
            var dishes = await _dishService.GetDishInfosAsync();
            _dishes.Clear();
            foreach (var d in dishes)
            {
                _dishes.Add(d);
            }
        }

        private void HandleException(Exception e)
        {
            MessageBox.Show($"{e.GetType().Name} : {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}