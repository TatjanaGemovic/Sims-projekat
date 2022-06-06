using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class NotesPageViewModel : BindableBase
    {
        Frame mainFrame;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand DetailsCommand { get; set; }
        public MyICommand NewNoteCommand { get; set; }

        private NoteViewModel noteViewModel;
        public NoteViewModel NoteViewModel
        {
            get { return noteViewModel; }
            set
            {
                if (noteViewModel != value)
                {
                    noteViewModel = value;
                    OnPropertyChanged("NoteViewModel");

                }
            }
        }

        private ObservableCollection<NoteViewModel> notes;
        public ObservableCollection<NoteViewModel> Notes
        {
            get { return notes; }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    OnPropertyChanged("Notes");

                }
            }
        }
        public NotesPageViewModel(Frame f, Patient p)
        {
            mainFrame = f;
            patient = p;

            ObservableCollection<Note> notes = new ObservableCollection<Note>(App.noteController.GetNotesByPatientID(patient.ID));
            Inject = new Injector();
            Notes = new ObservableCollection<NoteViewModel>(Inject.NotesConverter.ConvertCollectionToViewModel(notes));
            DetailsCommand = new MyICommand(OnDetail);
            NewNoteCommand = new MyICommand(OnCreate);
        }

        private void OnDetail()
        {
            ViewNotePage viewNotePage = new ViewNotePage(mainFrame, NoteViewModel);
            mainFrame.NavigationService.Navigate(viewNotePage);
        }

        private void OnCreate()
        {
            CreateNotePage createNotePage = new CreateNotePage(mainFrame, false, patient);
            mainFrame.NavigationService.Navigate(createNotePage);
        }
    }
}
