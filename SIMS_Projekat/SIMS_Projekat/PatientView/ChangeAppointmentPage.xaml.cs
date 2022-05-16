using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ChangeAppointmentPage.xaml
    /// </summary>
    public partial class ChangeAppointmentPage : Page
    {
        Frame mainFrame;
        public Appointment appointment;
        Patient patient;
        BindingList<String> listOfTakenAppointmentTime;
        BindingList<String> listOfAppointmentTime;
        public ObservableCollection<DoctorInfo> doctorInfoList = new ObservableCollection<DoctorInfo>();
        DoctorInfo drInfo;
        DateTime pickedDate;

        public ChangeAppointmentPage(Frame frame, int appointmentID, Patient p)
        {
            InitializeComponent();
            mainFrame = frame;
            patient = p;
            appointment = App.appointmentController.GetAppointmentByID(appointmentID);

            SetBlackOutDates();
            InitializeDoctorComboBox();
            Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(appointment.doctor.LicenceNumber) as Doctor;
            chosen_doctor.Content = d.FirstName + " " + d.LastName;
            drInfo = new DoctorInfo(d.FirstName + " " + d.LastName, d.LicenceNumber);

            string appointmentDate = appointment.beginningDate.Date.ToString("MM/dd/yyyy");
            date.Text = appointmentDate;

            string appointmentTime = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
            comboTime.Text = appointmentTime;
         
        }
        private void SetBlackOutDates()
        {
            DateTime today = DateTime.Today;
            DateTime lastDate = DateTime.Today.AddMonths(6);
            DateTime dayOfAppointment = appointment.beginningDate;

            date.DisplayDateStart = today;
            date.DisplayDateEnd = lastDate;

            if (today.Date == dayOfAppointment.Date)
            {   
                date.BlackoutDates.Add(new CalendarDateRange(
                   today,
                   today
                ));
                date.BlackoutDates.Add(new CalendarDateRange(
                   today.AddDays(4),
                   lastDate
                ));

            }
            else{
                TimeSpan diff1 = dayOfAppointment.Subtract(today);
                date.BlackoutDates.Add(new CalendarDateRange(
                    today,
                    dayOfAppointment.AddDays(-2)
                ));
                date.BlackoutDates.Add(new CalendarDateRange(
                    dayOfAppointment.AddDays(3),
                    lastDate
                ));              
            }            
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
            foreach (Doctor doctor in App.accountController.GetAllDoctorAccounts())
            {
                doctorInfoList.Add(new DoctorInfo(doctor.FirstName + " " + doctor.LastName, doctor.LicenceNumber));

            }
            choose_doctor.ItemsSource = doctorInfoList;
        }

        private void choose_doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drInfo = choose_doctor.SelectedItem as DoctorInfo;
            chosen_doctor.Content = drInfo.doctorName;

            InitializeListOfAppointments();
            
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string date = this.date.ToString();
            pickedDate = DateTime.Parse(date);
            InitializeListOfAppointments();
           
        }

        private void comboTime_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            comboTime.ItemsSource = listOfAppointmentTime;
        }
        private void InitializeListOfAppointments()
        {
            List<string> list;
            list = App.appointmentController.GetAvailableAppointmentsForPatient(patient, pickedDate, drInfo.licenceNumber);
            
           
            // Create the new BindingList of Part type.
            listOfAppointmentTime = new BindingList<String>(App.appointmentController.createAppointmentTime());
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
                if (time.Equals(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm")))
                {
                    if (drInfo.licenceNumber.Equals(appointment.doctor.LicenceNumber))
                    {
                        continue;
                    }
                    else
                    {
                        Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(drInfo.licenceNumber) as Doctor;
                        bool isAvailable  = App.appointmentController.CheckIfDoctorIsAvailable(d, appointment.beginningDate);
                        if (isAvailable)
                        {
                            continue;       // dr slobodan
                        }
                        else
                        {
                            listOfAppointmentTime.Remove(time);
                        }
                    }
                }
                else 
                {
                    listOfAppointmentTime.Remove(time);
                }             
            }
            comboTime.ItemsSource = listOfAppointmentTime;

        }
      
        private void changeClick(object sender, RoutedEventArgs e)
        {
            string dateFromPage = this.date.ToString();
            DateTime start = DateTime.Parse(dateFromPage);
            DateTime startDate = start.Date;

            string timeFromPage = this.comboTime.SelectionBoxItem.ToString();
            TimeSpan timeStart = TimeSpan.Parse(timeFromPage);
            startDate = startDate.Add(timeStart);

            Doctor doctor;
            if (choose_doctor.SelectedItem != null)
            {
                doctor = App.accountController.GetDoctorAccountByLicenceNumber(drInfo.licenceNumber) as Doctor;
            }
            else
            {
                doctor = this.appointment.doctor;
            }
            
            if(startDate.Equals(this.appointment.beginningDate) && doctor.LicenceNumber.Equals(this.appointment.doctor.LicenceNumber))
            {
                ViewAppointmentPage viewAppointmentPage = new ViewAppointmentPage(mainFrame, this.appointment.appointmentID, patient);
                mainFrame.Content = viewAppointmentPage;
            }
            else
            {
                Room room;
                if (!startDate.Equals(this.appointment.beginningDate))
                {
                   room = App.appointmentController.GetAvailableRoom(startDate);
                }
                else
                {
                    room = this.appointment.room;
                }
                
                Appointment newAppointment = new Appointment()
                {
                    appointmentID = this.appointment.appointmentID,
                    beginningDate = startDate,
                    endDate = startDate.AddMinutes(15),
                    room = room,
                    doctor = doctor,
                    patient = patient,
                    operation = false,
                    isDelayed = true,
                    isScheduledByPatient = this.appointment.isScheduledByPatient
                };
                App.appointmentController.SetAppointment(newAppointment);

                ViewAppointmentPage viewAppointmentPage = new ViewAppointmentPage(mainFrame, appointment.appointmentID, patient);
                mainFrame.Content = viewAppointmentPage;
            }

        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            ViewAppointmentPage viewAppointmentPage = new ViewAppointmentPage(mainFrame, appointment.appointmentID, patient);
            mainFrame.Content = viewAppointmentPage;
        }

    }
}
