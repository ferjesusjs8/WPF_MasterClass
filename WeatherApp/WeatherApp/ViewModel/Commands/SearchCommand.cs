using System;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM _vm { get; set; }

        public event EventHandler CanExecuteChanged;

        public SearchCommand(WeatherVM vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.MakeQuery();
        }
    }
}
