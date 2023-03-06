using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services;
using WpfCrud.Services.Dishes;
using WpfCrud.Services.DishTypes;

namespace WpfCrud.ViewModels
{
    public class DishListingCrudViewModel : ViewModelBase
    {
        private readonly DishService _dishService = new DishService();
        private readonly DishTypeService _dishTypeService = new DishTypeService();
        private readonly EditEntityWindowDialogService _editEntityWindowDialogService = new EditEntityWindowDialogService();
        private readonly ObservableCollection<Dish> _dishes = new ObservableCollection<Dish>();
        private Dish _selectedDish;
        public Dish SelectedDish
        {
            get => _selectedDish;
            set
            {
                _selectedDish = value;
                OnPropertyChanged(nameof(SelectedDish));
            }
        }

        public ReadOnlyObservableCollection<Dish> Dishes { get; }
        public ICommand LoadDishesCommand { get; }
        public ICommand AddDishCommand { get; }
        public ICommand EditDishCommand { get; }
        public ICommand DeleteDishCommand { get; }

        public DishListingCrudViewModel()
        {
            Dishes = new ReadOnlyObservableCollection<Dish>(_dishes);
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

        private async Task EditDishAsync()
        {
            if (SelectedDish is null)
            {
                return;
            }

            var editDish = new Dish
            {
                Id = SelectedDish.Id,
                CaloricContentPer100Grams = SelectedDish.CaloricContentPer100Grams,
                CookingTimeMinutes = SelectedDish.CookingTimeMinutes,
                DishType = SelectedDish.DishType,
                Image = SelectedDish.Image,
                Name = SelectedDish.Name,
                Recipe = SelectedDish.Recipe,
                WeightGrams = SelectedDish.WeightGrams,
            };

            var dishTypes = await _dishTypeService.GetDishTypesAsync();
            var editableDishViewModel = new EditableDishViewModel(editDish, dishTypes);
            var result = _editEntityWindowDialogService.ShowDialog(editableDishViewModel);
            if (result != true)
            {
                return;
            }
            await _dishService.UpdateDishAsync(editDish);
            await LoadDishesAsync();
        }
        private async Task AddDishAsync()
        {
            var dishTypes = await _dishTypeService.GetDishTypesAsync();
            var editDish = new Dish();
            var editableDishViewModel = new EditableDishViewModel(editDish, dishTypes);
            var result = _editEntityWindowDialogService.ShowDialog(editableDishViewModel);
            if (result != true)
            {
                return;
            }
            _ = await _dishService.AddDishAsync(editDish);
            await LoadDishesAsync();
        }

        private async Task LoadDishesAsync()
        {
            var dishes = await _dishService.GetDishesAsync();
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

        public DishListingCrudViewModel(DishService dishService, DishTypeService dishTypeService, EditEntityWindowDialogService editEntityWindowDialogService) : this()
        {
            _dishService = dishService ?? throw new System.ArgumentNullException(nameof(dishService));
            _dishTypeService = dishTypeService ?? throw new System.ArgumentNullException(nameof(dishTypeService));
            _editEntityWindowDialogService = editEntityWindowDialogService ?? throw new System.ArgumentNullException(nameof(editEntityWindowDialogService));
        }

    }
}