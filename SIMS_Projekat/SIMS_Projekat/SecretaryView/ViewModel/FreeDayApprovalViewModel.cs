using SIMS_Projekat.Controller;
using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using SIMS_Projekat.SecretaryView.Notifications;
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
        private string APPROVED_NOTIFICATION = "Vaš zahtev za slobodne dane je prihvaćen";
        private string REJECTED_NOTIFICATION = "Vaš zahtev za slobodne dane je odbijen";

        private ContentControl contentControl;
        private AppointmentController appointmentController;
        private FreeDayRequestController freeDayRequestController;
        private INotificationSender notificationSender;

        public ObservableCollection<FreeDayRequest> FreeDayRequests { get; set; }

        private FreeDayRequest _selectedFreeDayRequest;

        public FreeDayRequest SelectedFreeDayRequest
        {
            get { return _selectedFreeDayRequest; }
            set 
            { 
                _selectedFreeDayRequest = value;
                if(_selectedFreeDayRequest != null)
                    UpdateAppointmentsForSelectedFreeDayRequest(_selectedFreeDayRequest.doctor, _selectedFreeDayRequest.from, _selectedFreeDayRequest.until);
            }
        }

        public ObservableCollection<Appointment> AppointmentsForSelectedTimeSpan { get; set; }

        public MyICommand ApproveFreeDayRequestCommand { get; set; }
        public MyICommand RejectFreeDayRequestCommand { get; set; }

        public FreeDayApprovalViewModel(ContentControl contentControl, INotificationSender notificationSender,
            AppointmentController appointmentController, FreeDayRequestController freeDayRequestController)
        {
            this.contentControl = contentControl;
            this.appointmentController = appointmentController;
            this.freeDayRequestController = freeDayRequestController;
            this.notificationSender = notificationSender;

            FreeDayRequests = new ObservableCollection<FreeDayRequest>(freeDayRequestController.GetRequests().Where(request => request.status == Status.Waiting));
            AppointmentsForSelectedTimeSpan = new ObservableCollection<Appointment>();

            ApproveFreeDayRequestCommand = new MyICommand(ApproveFreeDayRequestExecuteMethod);
            RejectFreeDayRequestCommand = new MyICommand(RejectFreeDayRequestExecuteMethod);
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

        private void ApproveFreeDayRequestExecuteMethod()
        {
            CancelAppointmentsForTimeSpan();
            SelectedFreeDayRequest.status = Status.Accepted;
            SendNotification(APPROVED_NOTIFICATION);
            FreeDayRequests.Remove(SelectedFreeDayRequest);
        }

        private void RejectFreeDayRequestExecuteMethod()
        {
            SelectedFreeDayRequest.status = Status.Denied;
            SendNotification(REJECTED_NOTIFICATION);
            FreeDayRequests.Remove(SelectedFreeDayRequest);
        }

        private void SendNotification(string content)
        {
            string notificationContent = content + " " + _selectedFreeDayRequest.from.Date + " - " + _selectedFreeDayRequest.until.Date;
            notificationSender.SendNotification(_selectedFreeDayRequest.doctor.ID, notificationContent);
        }

        private void CancelAppointmentsForTimeSpan()
        {
            foreach(Appointment appointment in AppointmentsForSelectedTimeSpan)
            {
                appointmentController.DeleteAppointment(appointment);
            }
        }


    }
}
