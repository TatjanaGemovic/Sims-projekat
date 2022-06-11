using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class SingleReportPageViewModel : BindableBase
    {
        Frame mainFrame;
        NoteViewModel Note;
        TherapyViewModel therapy;
        bool fromTherapy;
        private ReportViewModel report;
        public Injector Inject { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand NoteCommand { get; set; }
        public ReportViewModel Report
        {
            get { return report; }
            set
            {
                if (report != value)
                {
                    report = value;
                    OnPropertyChanged("Report");

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

        public SingleReportPageViewModel(Frame frame, ReportViewModel vmReport, bool fromTherapy, TherapyViewModel therapy = null)
        {
            Inject = new Injector();
            mainFrame = frame;
            Report = vmReport;
            this.fromTherapy = fromTherapy;
            this.therapy = therapy;
            if (vmReport.NoteID != "0")
            {
                Note = Inject.NotesConverter.ConvertModelToViewModel(App.noteController.GetNoteByID(Convert.ToInt32(vmReport.NoteID)));
                NoteContent = Note.Content;
            }

            BackCommand = new MyICommand(OnBack);
            NoteCommand = new MyICommand(OnNote);
        }

        private void OnNote()
        {
            if(Report.NoteID == "0")
            {
                CreateNotePage createNotePage = new CreateNotePage(mainFrame, 1, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient, Report);
                mainFrame.NavigationService.Navigate(createNotePage);
                return;
            }
            
            ChangeNotePage changeNotePage = new ChangeNotePage(mainFrame, Note, 1);
            mainFrame.NavigationService.Navigate(changeNotePage);
        }
        private void OnBack()
        {
            if (fromTherapy)
            {
                ViewTherapyPage viewTherapyPage = new ViewTherapyPage(mainFrame, therapy);
                mainFrame.NavigationService.Navigate(viewTherapyPage);
                return;
            }
            ReportsPage reportsPage = new ReportsPage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient);
            mainFrame.NavigationService.Navigate(reportsPage);
        }
    }
}
