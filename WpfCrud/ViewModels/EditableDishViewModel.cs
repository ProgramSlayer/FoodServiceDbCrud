using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditableDishViewModel : ViewModelBase
    {
        private readonly Dish _dish = new Dish();
        private readonly ObservableCollection<DishType> _dishTypes;

        public EditableDishViewModel(IEnumerable<DishType> dishTypes)
        {
            if (dishTypes is null)
            {
                throw new ArgumentNullException(nameof(dishTypes));
            }

            if (!dishTypes.Any())
            {
                throw new InvalidOperationException("Dish Type list is empty");
            }

            _dishTypes = new ObservableCollection<DishType>(dishTypes);
            DishTypes = new ReadOnlyObservableCollection<DishType>(_dishTypes);
            DishType = _dishTypes.First();
        }

        public EditableDishViewModel(Dish dish, IEnumerable<DishType> dishTypes) : this(dishTypes)
        {
            _dish = dish ?? throw new ArgumentNullException(nameof(dish));
        }

        public ReadOnlyObservableCollection<DishType> DishTypes { get; }

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

        public DishType DishType
        {
            get => _dish.DishType; 
            set
            {
                _dish.DishType = value;
                OnPropertyChanged(nameof(DishType));
            }
        }
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
    }
}