using System;

namespace WpfCrud.Commands
{
    public class DelegateCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

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
