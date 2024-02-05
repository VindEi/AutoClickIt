using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoClickIt.Core
{
    class RelyCommand
    {
        private Action<object> _excute;
        private Func<object, bool> _canExecute;

        public event EventHandler canExcuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelyCommand(Action<object> excute, Func<object, bool> canExcute = null)
        {
            _excute = excute;
            _canExecute = canExcute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _excute(parameter);
        }
    }
}
