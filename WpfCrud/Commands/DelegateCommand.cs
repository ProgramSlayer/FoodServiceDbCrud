using System;

namespace WpfCrud.Commands
{
    /// <summary>
    /// Команда, принимающая делегат.
    /// </summary>
    public class DelegateCommand : CommandBase
    {
        /// <summary>
        /// Действие, выполняемое командой.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Метод, определяющий доступность команды для выполнения.
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute">Метод, выполняемый командой.</param>
        /// <param name="canExecute">Метод, определяющий доступность команды для выполнения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="execute"/> имеет значение <see cref="null"/></exception>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute is null || _canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _execute(parameter);
            OnCanExecuteChanged();
        }
    }
}
