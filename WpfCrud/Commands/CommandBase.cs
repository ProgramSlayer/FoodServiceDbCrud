using System;
using System.Windows.Input;

namespace WpfCrud.Commands
{
    /// <summary>
    /// Абстрактная реализация интерфейса <see cref="ICommand"/>.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        /// <summary>
        /// Вызывает событие <see cref="CanExecuteChanged"/>.
        /// </summary>
        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);   
    }
}
