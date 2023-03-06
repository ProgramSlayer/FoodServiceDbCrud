namespace WpfCrud.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ViewModelBase _currentViewModel;

        public MainViewModel(ViewModelBase currentViewModel)
        {
            _currentViewModel = currentViewModel ?? throw new System.ArgumentNullException(nameof(currentViewModel));
        }
        public ViewModelBase CurrentViewModel => _currentViewModel;
    }
}
