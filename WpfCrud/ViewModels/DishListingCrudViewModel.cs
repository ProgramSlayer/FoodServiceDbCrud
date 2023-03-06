using System;
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
            throw new NotImplementedException();
            //if (SelectedDish is null)
            //{
            //    return;
            //}

            //await _dishService.DeleteDishAsync(SelectedDish.Id);
            //await LoadDishesAsync();
        }

        private async Task EditDishAsync()
        {
            throw new NotImplementedException();
            //if (SelectedDish is null)
            //{
            //    return;
            //}

            //var editDish = new Dish
            //{
            //    Id = SelectedDish.Id,
            //    CaloricContentPer100Grams = SelectedDish.CaloricContentPer100Grams,
            //    CookingTimeMinutes = SelectedDish.CookingTimeMinutes,
            //    DishType = SelectedDish.DishType,
            //    Image = SelectedDish.Image,
            //    Name = SelectedDish.Name,
            //    Recipe = SelectedDish.Recipe,
            //    WeightGrams = SelectedDish.WeightGrams,
            //};

            //var dishTypes = await _dishTypeService.GetDishTypesAsync();
            //var editableDishViewModel = new EditableDishViewModel(editDish, dishTypes);
            //var result = _editEntityWindowDialogService.ShowDialog(editableDishViewModel);
            //if (result != true)
            //{
            //    return;
            //}
            //await _dishService.UpdateDishAsync(editDish);
            //await LoadDishesAsync();
        }
        private async Task AddDishAsync()
        {
            throw new NotImplementedException();
            //var dishTypes = await _dishTypeService.GetDishTypesAsync();
            //var editDish = new Dish();
            //var editableDishViewModel = new EditableDishViewModel(editDish, dishTypes);
            //var result = _editEntityWindowDialogService.ShowDialog(editableDishViewModel);
            //if (result != true)
            //{
            //    return;
            //}
            //_ = await _dishService.AddDishAsync(editDish);
            //await LoadDishesAsync();
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