using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.DishCookings;
using WpfCrud.Services.Dishes;

namespace WpfCrud.ViewModels
{
    public class DishCookingListingCrudViewModel : ViewModelBase
    {
        private readonly DishCookingService _dishCookingService = new DishCookingService();
        private readonly DishService _dishService = new DishService();
        private readonly ObservableCollection<ViewDishCooking> _dishCookings = new ObservableCollection<ViewDishCooking>();
        private ViewDishCooking _selectedDishCooking;

        public ReadOnlyObservableCollection<ViewDishCooking> DishCookings { get; }
        public ViewDishCooking SelectedDishCooking
        {
            get => _selectedDishCooking; 
            set
            {
                _selectedDishCooking = value;
                OnPropertyChanged(nameof(SelectedDishCooking));
            }
        }

        public ICommand LoadDishCookingsCommand { get; }
        public ICommand AddDishCookingCommand { get; }
        public ICommand EditDishCookingCommand { get; }
        public ICommand DeleteDishCookingCommand { get; }

        public DishCookingListingCrudViewModel()
        {
            DishCookings = new ReadOnlyObservableCollection<ViewDishCooking>(_dishCookings);
            LoadDishCookingsCommand = new AsyncDelegateCommand(LoadDishCookingsAsync, HandleException);
            AddDishCookingCommand = new AsyncDelegateCommand(AddDishCookingAsync, HandleException);
            EditDishCookingCommand = new AsyncDelegateCommand(EditDishCookingAsync, HandleException);
            DeleteDishCookingCommand = new AsyncDelegateCommand(DeleteDishCookingAsync, HandleException);
        }

        private async Task DeleteDishCookingAsync()
        {
            if (SelectedDishCooking is null)
            {
                return;
            }

            await _dishCookingService.DeleteDishCookingAsync(SelectedDishCooking.Id);
            await LoadDishCookingsAsync();
        }

        private bool? EditDishCookingWindowShowDialog(EditableDishCooking dishCooking, IEnumerable<ViewDish> dishes)
        {
            var editDishCookingViewModel = new EditDishCookingViewModel(dishCooking, dishes);
            var editDishCookingWindow = new EditDishCookingWindow(editDishCookingViewModel);
            var result = editDishCookingWindow.ShowDialog();
            return result;
        }

        private async Task EditDishCookingAsync()
        {
            if (SelectedDishCooking is null)
            {
                return;
            }

            var dc = new EditableDishCooking(
                SelectedDishCooking.Id,
                SelectedDishCooking.DishId,
                SelectedDishCooking.Count,
                SelectedDishCooking.CookedAt);
            
            var dishes = await _dishService.GetDishInfosAsync();
            var dialogResult = EditDishCookingWindowShowDialog(dc, dishes);
            if (dialogResult != true)
            {
                return;
            }

            await _dishCookingService.UpdateDishCookingAsync(dc);
            await LoadDishCookingsAsync();
        }

        private async Task AddDishCookingAsync()
        {
            var dc = new EditableDishCooking();
            var dishes = await _dishService.GetDishInfosAsync();
            var dialogResult = EditDishCookingWindowShowDialog(dc, dishes);
            if (dialogResult != true)
            {
                return;
            }
            _ = await _dishCookingService.AddDishCookingAsync(dc);
            await LoadDishCookingsAsync();
        }

        private void HandleException(Exception e)
        {
            MessageBox.Show($"{e.GetType().Name} : {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task LoadDishCookingsAsync()
        {
            var dishCookings = await _dishCookingService.GetViewDishCookingsAsync();
            _dishCookings.Clear();
            foreach (var dc in dishCookings)
            {
                _dishCookings.Add(dc);
            }
        }

        public DishCookingListingCrudViewModel(DishCookingService dishCookingService, DishService dishService) : this()
        {
            _dishCookingService = dishCookingService ?? throw new ArgumentNullException(nameof(dishCookingService));
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
        }
    }
}