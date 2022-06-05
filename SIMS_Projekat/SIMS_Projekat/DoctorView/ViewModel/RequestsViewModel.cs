using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView.ViewModel
{
    public class RequestsViewModel : BindableBase
    {
        private Frame frame;
        private Doctor doctor;
        public ObservableCollection<Request2> Requests { get; set; }
        public MyICommand showCommand { get; set; }
        public MyICommand backCommand { get; set; }
        public MyICommand showReportCommand { get; set; }
        public RequestsViewModel(Frame main, Doctor d)
        {
            frame = main;
            doctor = d;
            loadRequests();

            showCommand = new MyICommand(OnShow);
            backCommand = new MyICommand(OnBack);
            showReportCommand = new MyICommand(OnShowReport);
        }

        private void OnBack()
        {
            frame.Content = new DoctorAppointments(frame, doctor);
        }

        private void OnShowReport()
        {
            frame.Content = new FreeDaysReport(frame, doctor);
        }

        private void OnShow()
        {
            frame.Content = new FreeDayRequest(frame, doctor);
        }

        public class Request2
        {
            public string fromUntil { get; set; }
            public string requestStatus { get; set; }

            public Request2(string fromUntil, string requestStatus)
            {
                this.fromUntil = fromUntil;
                this.requestStatus = requestStatus;
            }
        }

        public void loadRequests()
        {
            ObservableCollection<Request2> requests = new ObservableCollection<Request2>();
            requests = App.listsForBinding.CreateRequestsList(doctor);  
            Requests = requests;
        }
    }
}
