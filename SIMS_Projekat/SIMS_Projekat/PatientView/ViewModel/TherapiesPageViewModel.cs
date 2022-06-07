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
    public class TherapiesPageViewModel : BindableBase
    {
        Frame mainFrame;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand DetailsCommand { get; set; }

        private TherapyViewModel therapyViewModel;
        public TherapyViewModel TherapyViewModel
        {
            get { return therapyViewModel; }
            set
            {
                if (therapyViewModel != value)
                {
                    therapyViewModel = value;
                    OnPropertyChanged("TherapyViewModel");

                }
            }
        }

        private ObservableCollection<TherapyViewModel> therapies;
        public ObservableCollection<TherapyViewModel> Therapies
        {
            get { return therapies; }
            set
            {
                if (therapies != value)
                {
                    therapies = value;
                    OnPropertyChanged("Therapies");

                }
            }
        }
        public TherapiesPageViewModel(Frame f, Patient p)
        {
            mainFrame = f;
            patient = p;

            ObservableCollection<Receipt> receipts = new ObservableCollection<Receipt>(App.receiptController.GetActiveReceiptByPatientID(patient.ID));
            Inject = new Injector();
            Therapies = new ObservableCollection<TherapyViewModel>(Inject.TherapyConverter.ConvertCollectionToViewModel(receipts));
            DetailsCommand = new MyICommand(OnDetail);
        }

        private void OnDetail()
        {
            //ViewReportPage viewReportPage = new ViewReportPage(mainFrame, ReportViewModel);
            //mainFrame.NavigationService.Navigate(viewReportPage);
        }
    }
}
