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
        int fromWhere;
        ReportViewModel vmReport;
        TherapyViewModel vmTherapy;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand GoBackCommand { get; set; }
        public MyICommand CreateCommand { get; set; }

        private NoteViewModel newNoteViewModel = new NoteViewModel();
        public NoteViewModel NewNoteViewModel
        {
            get { return newNoteViewModel; }
            set
            {
                if (newNoteViewModel != value)
                {
                    newNoteViewModel = value;
                    OnPropertyChanged("NewNoteViewModel");
                    CreateCommand.RaiseCanExecuteChanged();

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
        public CreateNotePageViewModel(Frame f, int fromWhere, Patient p, ReportViewModel vmReport, TherapyViewModel vmTherapy)
        {
            mainFrame = f;
            Inject = new Injector();
            this.fromWhere = fromWhere;
            patient = p;
            this.vmReport = vmReport;
            this.vmTherapy = vmTherapy;
            GoBackCommand = new MyICommand(OnBack);
            CreateCommand = new MyICommand(OnCreate, CanCreate);
        }

        private void OnBack()
        {      
            mainFrame.NavigationService.GoBack();          
        }

        private void OnCreate()
        {
            NewNoteViewModel.Title = Title;
            NewNoteViewModel.Content = Content;
            Note newNote = Inject.NotesConverter.ConvertViewModelToModel(NewNoteViewModel, patient);
            newNote = App.noteController.AddNote(newNote);
            
            if (fromWhere == 1)
            {
                vmReport.NoteID = newNote.noteID.ToString();
                App.finishedAppointmentController.AddNoteToAppointment(vmReport.FinishedAppointmentID, Convert.ToInt32(vmReport.NoteID));
                ViewReportPage viewReportPage = new ViewReportPage(mainFrame, vmReport, false);
                mainFrame.NavigationService.Navigate(viewReportPage);
                return;
            }
            else if (fromWhere == 2)
            {
                vmTherapy.NoteID = newNote.noteID.ToString();
                App.receiptController.AddNoteToReceipt(vmTherapy.ReceiptID, Convert.ToInt32(vmTherapy.NoteID));
                ViewTherapyPage viewTherapyPage = new ViewTherapyPage(mainFrame, vmTherapy);
                mainFrame.NavigationService.Navigate(viewTherapyPage);
                return;
            }
            else
            {
                NotesPage notesPage = new NotesPage(mainFrame, patient);
                mainFrame.NavigationService.Navigate(notesPage);
            }         
        }

        private bool CanCreate()
        {
            if (Title == "" || Content == "")
                return false;
            return true;
        }
        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }
    }
}
