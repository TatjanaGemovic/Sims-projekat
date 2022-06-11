using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class SingleTherapyPageViewModel : BindableBase
    {
        Frame mainFrame;
        NoteViewModel Note;
        private TherapyViewModel therapy;
        public Injector Inject { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand NoteCommand { get; set; }
         public MyICommand ReportCommand { get; set; }
        public TherapyViewModel Therapy
        {
            get { return therapy; }
            set
            {
                if (therapy != value)
                {
                    therapy = value;
                    OnPropertyChanged("Therapy");

                }
            }
        }

        private string noteContent;
        public string NoteContent
        {
            get { return noteContent; }
            set
            {
                if (noteContent != value)
                {
                    noteContent = value;
                    OnPropertyChanged("NoteContent");

                }
            }
        }

        public SingleTherapyPageViewModel(Frame frame, TherapyViewModel vmTherapy)
        {
            Inject = new Injector();
            mainFrame = frame;
            Therapy = vmTherapy;
            if (Therapy.NoteID != "0")
            {
                Note = Inject.NotesConverter.ConvertModelToViewModel(App.noteController.GetNoteByID(Convert.ToInt32(vmTherapy.NoteID)));
                NoteContent = Note.Content;
            }

            BackCommand = new MyICommand(OnBack);
            ReportCommand = new MyICommand(OnReport);
            NoteCommand = new MyICommand(OnNote);
        }

        private void OnNote()
        {
            if (Therapy.NoteID == "0")
            {
                CreateNotePage createNotePage = new CreateNotePage(mainFrame, 2, App.finishedAppointmentController.GetAppointmentByID(Therapy.FinishedAppointmentID).patient, null, Therapy);
                mainFrame.NavigationService.Navigate(createNotePage);
                return;
            }

            ChangeNotePage changeNotePage = new ChangeNotePage(mainFrame, Note, 2);
            mainFrame.NavigationService.Navigate(changeNotePage);
        }
        private void OnBack()
        {
            TherapyPage therapyPage = new TherapyPage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Therapy.FinishedAppointmentID).patient);
            mainFrame.NavigationService.Navigate(therapyPage);
        }

        private void OnReport()
        {
            ReportViewModel report = GetReportViewModel();
            
            ViewReportPage viewReportPage = new ViewReportPage(mainFrame, report, true, Therapy);
            mainFrame.NavigationService.Navigate(viewReportPage);
        }
        private ReportViewModel GetReportViewModel()
        {
    
            FinishedAppointment appointment = App.finishedAppointmentController.GetAppointmentByID(Therapy.FinishedAppointmentID);
            ReportViewModel report = Inject.ReportsConverter.ConvertModelToViewModel(appointment);
            return report;
        }
        
    }
}
