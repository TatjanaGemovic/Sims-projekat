using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for ScheduleAppointmentPage.xaml
    /// </summary>
    public partial class ScheduleAppointmentPage : Page
    {
        //private ComboBox izbor_vremena;
        Frame frame;
        Patient patient;
        DateTime pickedDate;
        BindingList<String> listOfTakenAppointmentTime;
        BindingList<String> listOfAppointmentTime;
        public ObservableCollection<DoctorInfo> doctorInfoList { get; set; }
        DoctorInfo drInfo;
        public ScheduleAppointmentPage(Frame mainFrame, Patient p)
        {
            frame = mainFrame;
            patient = p;

            InitializeComponent();
            this.DataContext = this;
            RandomAppointmentFrame.Content = new RandomAppontmentFirstPage(patient, frame);
            SetBlackOutDates();

            if (patient.doctorLicenceNumber != "")
            {
                Doctor doctor = App.accountController.GetDoctorAccountByLicenceNumber(patient.doctorLicenceNumber)as Doctor;       
                drInfo = new DoctorInfo(doctor.FirstName + " " + doctor.LastName, doctor.LicenceNumber);
            }

            InitializeDoctorComboBox();
        }

        private void SetBlackOutDates()
        {
            DateTime startDate = DateTime.Today;
            date.BlackoutDates.Add(new CalendarDateRange(
               startDate,
               startDate
            ));
            date.DisplayDateEnd = startDate.AddMonths(6);
        }

        public class DoctorInfo
        {
            public string doctorName { get; set; }
            public string licenceNumber { get; set; }

            public DoctorInfo(string doctor, string licence)
            {

                doctorName = doctor;
                licenceNumber = licence;
            }
        }
        public void InitializeDoctorComboBox()
        {
            doctorInfoList = new ObservableCollection<DoctorInfo>();
            int doctorPlaceInCollection = 0;

            foreach (Doctor doctor in App.accountController.GetGeneralPractitionerDoctors())
            {
                doctorInfoList.Add(new DoctorInfo(doctor.FirstName + " " + doctor.LastName, doctor.LicenceNumber));
                if (drInfo != null && drInfo.doctorName.Equals(doctor.FirstName + " " + doctor.LastName))
                {
                    doctorPlaceInCollection = doctorInfoList.Count() - 1;
                    choose_doctor.SelectedItem = doctorInfoList[doctorPlaceInCollection];
                }       
            }          
        }
        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(patient.doctorLicenceNumber != "" || choose_doctor.SelectedItem != null)
            {
                comboTime.IsHitTestVisible = true;
                comboTime.IsEnabled = true;
                string date = this.date.ToString();
                pickedDate = DateTime.Parse(date);
                InitializeListOfAppointments();
            }
            else
            {
                warning.Visibility = Visibility.Visible;
                string date = this.date.ToString();
                pickedDate = DateTime.Parse(date);
            }
        }

        private void InitializeListOfAppointments()
        {
            List<string> list;
            if (choose_doctor.SelectedItem != null)
            {
               list = App.appointmentController.GetTakenAppointmentsForPatient(patient, pickedDate, drInfo.licenceNumber);
            }
            else
            {
               list = App.appointmentController.GetTakenAppointmentsForPatient(patient, pickedDate, patient.doctorLicenceNumber);
            }
            

            // Create the new BindingList of Part type.
            listOfAppointmentTime = new BindingList<String>(App.appointmentController.CreateAppointmentTime());
            listOfTakenAppointmentTime = new BindingList<String>(list);

            // Allow new parts to be added, but not removed once committed.        
            listOfAppointmentTime.AllowNew = true;
            listOfAppointmentTime.AllowRemove = true;

            // Raise ListChanged events when new parts are added.
            listOfAppointmentTime.RaiseListChangedEvents = true;

            // Do not allow parts to be edited.
            listOfAppointmentTime.AllowEdit = false;

            foreach (string time in listOfTakenAppointmentTime)
            {
                listOfAppointmentTime.Remove(time);
            }
            comboTime.ItemsSource = listOfAppointmentTime;
        }
       
        private void ComboTime_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            comboTime.ItemsSource = listOfAppointmentTime;
        }
       

        private void Choose_doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drInfo = choose_doctor.SelectedItem as DoctorInfo;
            
            if (date.SelectedDate != null)
            {
                comboTime.IsHitTestVisible = true;
                comboTime.IsEnabled = true;
                InitializeListOfAppointments();
            }
        }
        private void ScheduleClick(object sender, RoutedEventArgs e)
        {
            DateTime start = DateTime.Parse(this.date.ToString());
            DateTime startDate = start.Date;

            TimeSpan timeStart = TimeSpan.Parse(this.comboTime.SelectionBoxItem.ToString());
            startDate = startDate.Add(timeStart);

            int reminderID = 0;
            if ((bool)checkbox.IsChecked)
                reminderID = CreateReminder(startDate);

            Doctor doctor = App.accountController.GetDoctorAccountByLicenceNumber(drInfo.licenceNumber) as Doctor;
            Room room = App.appointmentController.GetAvailableRoom(startDate);

            Appointment appointment = new Appointment()
            {
                beginningDate = startDate,
                endDate = startDate.AddMinutes(15),
                room = room,
                doctor = doctor,
                patient = patient,
                operation = false,
                isDelayedByPatient = false,
                isScheduledByPatient = true,
                reminderForPatientID = reminderID
            };

            App.appointmentController.AddAppointment(appointment);
            Appointments Appointments = new Appointments(frame, patient);
            frame.Content = Appointments;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Appointments Appointments = new Appointments(frame, patient);
            frame.Content = Appointments;
        }
        
        private void ComboTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            scheduleClickButton.IsEnabled = true;
        }
        private int CreateReminder(DateTime timeOfAppointment)
        {
            Reminder newReminder = new Reminder()
            {
                isRepeatable = "Nikada",
                patient = patient,
                startTime = timeOfAppointment.AddDays(-1),
                type = "Pregled: ",
                content = "sutra u " + timeOfAppointment.TimeOfDay.ToString(@"hh\:mm")
            };
            newReminder = App.reminderController.AddReminder(newReminder);
            return newReminder.ID;
        }
    }
    
}
