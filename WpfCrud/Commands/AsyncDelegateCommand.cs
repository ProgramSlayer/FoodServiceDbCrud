using System;
using System.Threading.Tasks;

namespace WpfCrud.Commands
{
    /// <summary>
    /// Асинхронная команда, принимающая делегат.
    /// </summary>
    public class AsyncDelegateCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">Метод, выполняемый командой.</param>
        /// <param name="onException">Метод, вызываемый при исключении во время выполнения команды.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AsyncDelegateCommand(Func<Task> callback, Action<Exception> onException = null) : base(onException)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}
