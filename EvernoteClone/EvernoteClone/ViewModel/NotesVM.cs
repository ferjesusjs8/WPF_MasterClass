using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        private Notebook selectedNotebook;


        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                if (value != null)
                {
                    selectedNotebook = value;

                    OnPropertyChanged("SelectedNotebook");
                    GetNotes();
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNoteBookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NotesVM()
        {
            NewNoteBookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();
            GetNotes();
        }

        public void CreateNotebook()
        {
            var noteBook = new Notebook()
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(noteBook);

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            if (notebookId > 0)
            {
                var note = new Note()
                {
                    CreatedAt = DateTime.Now,
                    NotebookId = notebookId,
                    Title = "New Note",
                    UpdatedAt = DateTime.Now
                };

                DatabaseHelper.Insert(note);

                GetNotes();
            }
        }

        private void GetNotebooks()
        {
            Notebooks.Clear();

            var notebooks = DatabaseHelper.Read<Notebook>();

            foreach (var notebook in notebooks)
                Notebooks.Add(notebook);
        }

        private void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                Notes.Clear();

                var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId.Equals(SelectedNotebook.Id)).ToList();

                foreach (var note in notes)
                    Notes.Add(note);
            }
        }
    }
}