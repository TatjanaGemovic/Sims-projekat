using SIMS_Projekat.Controller;
using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.SecretaryView.ViewModel
{
    public class FreeDayApprovalViewModel
    {
        private ContentControl contentControl;
        private AppointmentController appointmentController;
        public ObservableCollection<FreeDayRequest> FreeDayRequests { get; set; }

        private FreeDayRequest _selectedFreeDayRequest;

        public FreeDayRequest SelectedFreeDayRequest
        {
            get { return _selectedFreeDayRequest; }
            set 
            { 
                _selectedFreeDayRequest = value;
                UpdateAppointmentsForSelectedFreeDayRequest(_selectedFreeDayRequest.doctor, _selectedFreeDayRequest.from, _selectedFreeDayRequest.until);
            }
        }

        public ObservableCollection<Appointment> AppointmentsForSelectedTimeSpan { get; set; }

        public MyICommand ApproveFreeDayRequestCommand { get; set; }
        public MyICommand RejectFreeDayRequestCommand { get; set; }

        public FreeDayApprovalViewModel(ContentControl contentControl, AppointmentController appointmentController)
        {
            this.contentControl = contentControl;
            this.appointmentController = appointmentController;

            FreeDayRequests = new ObservableCollection<FreeDayRequest>(App.freeDayRequestRepository.Requests.Where(request => request.status == Status.Waiting));
            AppointmentsForSelectedTimeSpan = new ObservableCollection<Appointment>();
        }

        private void UpdateAppointmentsForSelectedFreeDayRequest(Doctor doctor, DateTime startDateTime, DateTime endDateTime)
        {
            AppointmentsForSelectedTimeSpan.Clear();
            List<Appointment> appointmentsForTimeSpan = appointmentController.GetAppointmentsForTimeSpan(doctor, startDateTime, endDateTime);
            foreach(Appointment appointment in appointmentsForTimeSpan)
            {
                AppointmentsForSelectedTimeSpan.Add(appointment);
            }
        }


    }
}
