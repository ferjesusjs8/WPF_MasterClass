using System;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM _vm { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherVM vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            _vm.MakeQuery();
        }
    }
}
