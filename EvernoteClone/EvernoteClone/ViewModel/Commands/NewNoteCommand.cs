using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public NotesVM VM { get; set; }

        public NewNoteCommand(
            NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            var selectedNotebook = parameter as Notebook;
            return selectedNotebook != null;
        }

        public void Execute(object parameter)
        {
            var selectedNotebook = parameter as Notebook;

            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
