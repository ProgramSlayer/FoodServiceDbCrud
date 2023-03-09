using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditableDishTypeViewModel : ViewModelBase
    {
        private readonly DishType _dishType = new DishType();

        public EditableDishTypeViewModel()
        {
        }

        public EditableDishTypeViewModel(DishType dishType)
        {
            _dishType = dishType ?? throw new System.ArgumentNullException(nameof(dishType));
        }

        public int Id => _dishType.Id;
        public string Name
        {
            get => _dishType.Name;
            set
            {
                _dishType.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}