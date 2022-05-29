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
    public class CreateNotePageViewModel : BindableBase
    {
        Frame mainFrame;
        Patient patient;
        bool fromReport;
        public Injector Inject { get; set; }
        public MyICommand GoBackCommand { get; set; }
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
        public CreateNotePageViewModel(Frame f, Patient p, bool fromRep)
        {
            mainFrame = f;
            patient = p;
            fromReport = fromRep;
            ObservableCollection<Note> notes = new ObservableCollection<Note>(App.noteController.GetNotesByPatientID(patient.ID));
            Inject = new Injector();
            Notes = new ObservableCollection<NoteViewModel>(Inject.NotesConverter.ConvertCollectionToViewModel(notes));
            GoBackCommand = new MyICommand(OnBack);
            //NewNoteCommand = new MyICommand(OnCreate);
        }

        private void OnBack()
        {      
            mainFrame.NavigationService.GoBack();          
        }

        private void OnCreate()
        {
            CreateNotePage createNotePage = new CreateNotePage(mainFrame, patient, false);
            mainFrame.NavigationService.Navigate(createNotePage);
        }
    }
}
