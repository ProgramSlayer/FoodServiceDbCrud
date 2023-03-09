using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCrud.Commands
{
    public class AsyncDelegateCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

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
