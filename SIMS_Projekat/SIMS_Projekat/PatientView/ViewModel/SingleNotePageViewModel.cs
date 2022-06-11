using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView
{
    public class SingleNotePageViewModel : BindableBase
    {
        Frame mainFrame;
        private NoteViewModel note;
        public Injector Inject { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand ChangeCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public NoteViewModel Note
        {
            get { return note; }
            set
            {
                if (note != value)
                {
                    note = value;
                    OnPropertyChanged("Note");

                }
            }
        }
        public SingleNotePageViewModel(Frame frame, NoteViewModel vmNote)
        {
            mainFrame = frame;
            Note = vmNote;
            Inject = new Injector();
            BackCommand = new MyICommand(OnBack);
            ChangeCommand = new MyICommand(OnChange);
            DeleteCommand = new MyICommand(OnDelete);
        }

        private void OnChange()
        {
            ChangeNotePage changeNotePage = new ChangeNotePage(mainFrame, Note, 0);
            mainFrame.NavigationService.Navigate(changeNotePage);
        }

        private void OnDelete()
        {
            if (MessageBox.Show("Jeste li sigurni da zelite da obrišete belešku?",
            "Brisanje beleške", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Patient patient = App.noteController.GetNoteByID(Convert.ToInt32(Note.NoteID)).patient;
                Note note = App.noteController.GetNoteByID(Convert.ToInt32(Note.NoteID));
                App.finishedAppointmentController.EraseNoteForAppointmentIfExists(note.noteID, patient);
                App.receiptController.EraseNoteForReceiptIfExists(note.noteID, patient);
                App.noteController.DeleteNote(note);

                NotesPage notesPage = new NotesPage(mainFrame, patient);
                mainFrame.NavigationService.Navigate(notesPage);
            }
        }
        private void OnBack()
        {
            NotesPage notesPage = new NotesPage(mainFrame, App.noteController.GetNoteByID(Convert.ToInt32(Note.NoteID)).patient);
            mainFrame.NavigationService.Navigate(notesPage);
        }
    }
}
