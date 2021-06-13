using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;

namespace EvernoteClone.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //TODO: get notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNoteBookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesVM()
        {
            NewNoteBookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public void CreateNotebook()
        {
            var noteBook = new Notebook()
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(noteBook);
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
            }
        }
    }
}