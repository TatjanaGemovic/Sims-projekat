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
    public class ReportsPageViewModel : BindableBase
    {   
        Frame mainFrame;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand DetailsCommand { get; set; }

        private ReportViewModel reportViewModel;
        public ReportViewModel ReportViewModel
        {
            get { return reportViewModel; }
            set
            {
                if (reportViewModel != value)
                {
                    reportViewModel = value;
                    OnPropertyChanged("ReportViewModel");

                }
            }
        }

        private ObservableCollection<ReportViewModel> reports;
        public ObservableCollection<ReportViewModel> Reports
        {
            get { return reports; }
            set
            {
                if (reports != value)
                {
                    reports = value;
                    OnPropertyChanged("Reports");

                }
            }
        }
        public ReportsPageViewModel(Frame f, Patient p)
        {
            mainFrame = f;
            patient = p;

            ObservableCollection<FinishedAppointment> finishedAppointments = new ObservableCollection<FinishedAppointment>(App.finishedAppointmentController.GetAppointmentByPatientID(patient.ID));
            Inject = new Injector();
            Reports = new ObservableCollection<ReportViewModel>(Inject.ReportsConverter.ConvertCollectionToViewModel(finishedAppointments));
            DetailsCommand = new MyICommand(OnDetail);
        }
    
        private void OnDetail()
        {
            ViewReportPage viewReportPage = new ViewReportPage(mainFrame, ReportViewModel, false);
            mainFrame.NavigationService.Navigate(viewReportPage);
        }
    }
}
