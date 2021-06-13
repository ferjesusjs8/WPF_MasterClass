using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public LoginVM VM { get; set; }

        public LoginCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}