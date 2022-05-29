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

        public SingleReportPageViewModel(Frame frame, ReportViewModel vmReport)
        {
            mainFrame = frame;
            Report = vmReport;

            BackCommand = new MyICommand(OnBack);
            NoteCommand = new MyICommand(OnNote);
        }

        private void OnNote()
        {
            if(Report.NoteID == "0")
            {
                CreateNotePage createNotePage = new CreateNotePage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient, true);
                mainFrame.NavigationService.Navigate(createNotePage);
            }

            //CreateNotePage createNotePage = new CreateNotePage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient, true);
            //mainFrame.NavigationService.Navigate(createNotePage);
        }
        private void OnBack()
        {
            //ReportsPage reportsPage = new ReportsPage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient);
            //mainFrame.NavigationService.Navigate(reportsPage);
            mainFrame.NavigationService.GoBack();
        }
    }
}
