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

            //ObservableCollection<FinishedAppointment> finishedAppointments = new ObservableCollection<FinishedAppointment>(App.finishedAppointmentController.GetAppointmentByPatientID(patient.ID));
            //Inject = new Injector();
            //Reports = new ObservableCollection<ReportViewModel>(Inject.ReportsConverter.ConvertCollectionToViewModel(finishedAppointments));
            BackCommand = new MyICommand(OnBack);
        }

        private void OnBack()
        {
            ReportsPage reportsPage = new ReportsPage(mainFrame, App.finishedAppointmentController.GetAppointmentByID(Report.FinishedAppointmentID).patient);
            mainFrame.NavigationService.Navigate(reportsPage);
        }
    }
}
