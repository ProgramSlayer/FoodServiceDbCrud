using System;
using System.Windows.Input;
using WpfCrud.Commands;
using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class EditDishTypeViewModel : ViewModelBase
    {
        private readonly DishType _dishType;

        public EditDishTypeViewModel(DishType dishType)
        {
            _dishType = dishType ?? throw new System.ArgumentNullException(nameof(dishType));
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            OnCancelSubmit();
        }

        private void Submit(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    throw new Exception("Название типа блюда должно быть заполнено!");
                }
            }
            catch (Exception e)
            {
                OnSubmitError(e);
                return;
            }
            OnSubmitSuccess();
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

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action<Exception> SubmitError;
        public event Action SubmitSuccess;
        public event Action CancelSubmit;

        protected void OnSubmitError(Exception e) => SubmitError?.Invoke(e);
        protected void OnSubmitSuccess() => SubmitSuccess?.Invoke();
        protected void OnCancelSubmit() => CancelSubmit?.Invoke();
    }
}