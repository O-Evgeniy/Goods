using System;
using System.Windows.Input;
using System.Windows;
namespace GoodsViewModel
{
    public class Command : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        private readonly Action action;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action execute)
        {
            this.action = execute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                Execute();
            else
                this.execute(parameter);
        }
        public void Execute()
        {
            this.action();
        }
    }
}
