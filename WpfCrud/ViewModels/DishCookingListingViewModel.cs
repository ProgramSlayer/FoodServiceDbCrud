using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.DishCookings;

namespace WpfCrud.ViewModels
{
    public class DishCookingListingViewModel : ViewModelBase
    {
        private readonly DishCookingService _dishCookingService = new DishCookingService();
        private readonly ObservableCollection<ViewDishCooking> _filteredDishCookings = new ObservableCollection<ViewDishCooking>();
        private string _dishNameFilter;
        public string DishNameFilter
        {
            get => _dishNameFilter;
            set
            {
                _dishNameFilter = value;
                OnPropertyChanged(nameof(DishNameFilter));
            }
        }
        public ReadOnlyObservableCollection<ViewDishCooking> FilteredDishCookings { get; }
        public ICommand FilterDishCookingsCommand { get; }

        public DishCookingListingViewModel()
        {
            FilteredDishCookings = new ReadOnlyObservableCollection<ViewDishCooking>(_filteredDishCookings);
            FilterDishCookingsCommand = new AsyncDelegateCommand(FilterDishCookingsAsync, HandleException);
        }

        private void HandleException(Exception e)
        {
            MessageBox.Show(e.Message, e.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task FilterDishCookingsAsync()
        {
            var filteredDishCookings = await _dishCookingService.GetViewDishCookingsByDishNameAsync(DishNameFilter);
            _filteredDishCookings.Clear();
            foreach (var fdc in filteredDishCookings)
            {
                _filteredDishCookings.Add(fdc);
            }
        }

        public DishCookingListingViewModel(DishCookingService dishCookingService)
        {
            _dishCookingService = dishCookingService ?? throw new System.ArgumentNullException(nameof(dishCookingService));
        }

    }
}
