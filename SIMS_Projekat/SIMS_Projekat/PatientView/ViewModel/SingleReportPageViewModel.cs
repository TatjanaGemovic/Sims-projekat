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

        public SingleReportPageViewModel(Frame frame, ReportViewModel vmReport)
        {
            mainFrame = frame;
            Report = vmReport;
            
            if(vmReport.NoteID != "0")
            {
                NoteContent = App.noteController.GetNoteByID(Convert.ToInt32(vmReport.NoteID)).content;
            }

            BackCommand = new MyICommand(OnBack);
            NoteCommand = new MyICommand(OnNote);
        }

        private void OnNote()
        {
            if(Report.NoteID == "0")
            {
                CreateNotePage createNotePage = new CreateNotePage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient, true, Report);
                mainFrame.NavigationService.Navigate(createNotePage);
                return;
            }

            //CreateNotePage createNotePage = new CreateNotePage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient, true);
            //mainFrame.NavigationService.Navigate(createNotePage);
        }
        private void OnBack()
        {
            mainFrame.NavigationService.GoBack();
        }
    }
}
