using System;
using System.Threading.Tasks;

namespace WpfCrud.Commands
{
    /// <summary>
    /// Абстрактная реализация команды, способной выполняться асинхронно.
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        /// <summary>
        /// Определяет, выполняется ли команда в данный момент.
        /// </summary>
        private bool _isLoading;

        /// <summary>
        /// Метод, определяющий обработку исключений во время выполнения команды.
        /// </summary>
        private readonly Action<Exception> _onException;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onException">Метод, определяющий обработку исключений во время выполнения команды.</param>
        protected AsyncCommandBase(Action<Exception> onException = null)
        {
            _onException = onException;
        }

        /// <summary>
        /// Определяет, выполняется ли команда в данный момент.
        /// </summary>
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
