namespace WpfCrud.ViewModels
{
    public class EditEntityViewModel : ViewModelBase
    {
        private readonly ViewModelBase _currentViewModel;

        public EditEntityViewModel(ViewModelBase currentViewModel)
        {
            _currentViewModel = currentViewModel ?? throw new System.ArgumentNullException(nameof(currentViewModel));
        }

        public ViewModelBase CurrentViewModel => _currentViewModel;
    }
}