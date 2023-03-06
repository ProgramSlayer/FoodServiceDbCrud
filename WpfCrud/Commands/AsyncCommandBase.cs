using System;
using System.Threading.Tasks;

namespace WpfCrud.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isLoading;
        private readonly Action<Exception> _onException;

        protected AsyncCommandBase(Action<Exception> onException = null)
        {
            _onException = onException;
        }

        public bool IsLoading
        {
            get => _isLoading; 
            set
            {
                _isLoading = value;
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !IsLoading;
        }

        public override async void Execute(object parameter)
        {
            IsLoading = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception e)
            {
                _onException?.Invoke(e);
            }
            IsLoading = false;
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}
