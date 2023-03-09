using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.Dishes;

namespace WpfCrud.ViewModels
{
    public class DishListingViewModel : ViewModelBase
    {
        private readonly DishService _dishService = new DishService();
        private readonly ObservableCollection<DishWithIngredients> _filteredDishes = new ObservableCollection<DishWithIngredients>();

        public ReadOnlyObservableCollection<DishWithIngredients> FilteredDishes { get; }
        public ICommand FilterDishesCommand { get; }

        private string _dishNameFilter = string.Empty;
        private DishWithIngredients _selectedDish;
        public string DishNameFilter
        {
            get => _dishNameFilter;
            set
            {
                _dishNameFilter = value;
                OnPropertyChanged(nameof(DishNameFilter));
            }
        }

        public DishWithIngredients SelectedDish
        {
            get => _selectedDish;
            set
            {
                _selectedDish = value;
                OnPropertyChanged(nameof(SelectedDish));
                OnPropertyChanged(nameof(SelectedDishIngredients));
            }
        }

        public IEnumerable<ViewDishIngredient> SelectedDishIngredients => _selectedDish?.Ingredients;

        public DishListingViewModel()
        {
            FilteredDishes = new ReadOnlyObservableCollection<DishWithIngredients>(_filteredDishes);
            FilterDishesCommand = new AsyncDelegateCommand(FilterDishesAsync, HandleException);
        }

        private void HandleException(Exception obj)
        {
            MessageBox.Show($"{obj.GetType().Name} : {obj.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task FilterDishesAsync()
        {
            var filteredDishes = await _dishService.GetDishesWithIngredientsByDishNameAsync(DishNameFilter);
            _filteredDishes.Clear();
            foreach (var fd in filteredDishes)
            {
                _filteredDishes.Add(fd);
            }
        }

        public DishListingViewModel(DishService dishService)
        {
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
        }
    }
}
