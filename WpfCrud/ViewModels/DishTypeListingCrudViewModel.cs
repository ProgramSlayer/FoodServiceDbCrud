using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;
using WpfCrud.Services.DishTypes;

namespace WpfCrud.ViewModels
{
    // TODO: при обновлении названия типа блюда нужно обновить представление блюд.
    public class DishTypeListingCrudViewModel : ViewModelBase
    {
        private readonly DishTypeService _dishTypeService = new DishTypeService();
        private readonly ObservableCollection<DishType> _dishTypes = new ObservableCollection<DishType>();
        private DishType _selectedDishType;

        public ReadOnlyObservableCollection<DishType> DishTypes { get; }
        public ICommand LoadDishTypesCommand { get; }
        public ICommand AddDishTypeCommand { get; }
        public ICommand EditDishTypeCommand { get; }
        public ICommand DeleteDishTypeCommand { get; }
        public DishType SelectedDishType
        {
            get => _selectedDishType;
            set
            {
                _selectedDishType = value;
                OnPropertyChanged(nameof(SelectedDishType));
            }
        }

        public DishTypeListingCrudViewModel()
        {
            DishTypes = new ReadOnlyObservableCollection<DishType>(_dishTypes);
            LoadDishTypesCommand = new AsyncDelegateCommand(LoadDishTypesAsync, HandleException);
            AddDishTypeCommand = new AsyncDelegateCommand(AddDishTypeAsync, HandleException);
            EditDishTypeCommand = new AsyncDelegateCommand(EditDishTypeAsync, HandleException);
            DeleteDishTypeCommand = new AsyncDelegateCommand(DeleteDishTypeAsync, HandleException);
        }

        private async Task DeleteDishTypeAsync()
        {
            if (SelectedDishType is null)
            {
                return;
            }

            var linkedDishCount = await _dishTypeService.GetDishCountByDishType(SelectedDishType.Id);

            if (linkedDishCount != 0)
            {
                throw new Exception($"Нельзя удалить тип блюда (Id = {SelectedDishType.Id}), так как есть блюда этого типа!");
            }

            await _dishTypeService.DeleteDishTypeAsync(SelectedDishType.Id);
            await LoadDishTypesAsync();
        }

        private bool? EditDishTypeWindowShowDialog(DishType dishType)
        {
            var editDishTypeViewModel = new EditDishTypeViewModel(dishType);
            var editDishTypeWindow = new EditDishTypeWindow(editDishTypeViewModel);
            var result = editDishTypeWindow.ShowDialog();
            return result;
        }

        private async Task EditDishTypeAsync()
        {
            if (SelectedDishType is null)
            {
                return;
            }

            var dishType = new DishType
            {
                Id = SelectedDishType.Id,
                Name = SelectedDishType.Name,
            };

            var dialogResult = EditDishTypeWindowShowDialog(dishType);
            if (dialogResult != true)
            {
                return;
            }

            await _dishTypeService.UpdateDishTypeAsync(dishType);
            await LoadDishTypesAsync();
        }

        private async Task AddDishTypeAsync()
        {
            var dishType = new DishType();
            var dialogResult = EditDishTypeWindowShowDialog(dishType);
            if (dialogResult != true)
            {
                return;
            }
            _ = await _dishTypeService.AddDishTypeAsync(dishType);
            await LoadDishTypesAsync();
        }

        private void HandleException(Exception e)
        {
            MessageBox.Show($"{e.GetType().Name} : {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task LoadDishTypesAsync()
        {
            var dishTypes = await _dishTypeService.GetDishTypesAsync();
            _dishTypes.Clear();
            foreach (var dt in dishTypes)
            {
                _dishTypes.Add(dt);
            }
        }

    }
}