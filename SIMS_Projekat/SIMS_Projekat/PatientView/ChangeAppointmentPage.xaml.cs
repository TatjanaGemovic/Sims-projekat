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
        public BindingList<String> listOfAppointmentTime { get; set; }
        public ObservableCollection<DoctorInfo> doctorInfoList { get; set; }
        DoctorInfo drInfo;
        DateTime pickedDate;

        public ChangeAppointmentPage(Frame frame, int appointmentID, Patient p)
        {
            InitializeComponent();
            mainFrame = frame;
            patient = p;
            appointment = App.appointmentController.GetAppointmentByID(appointmentID);

            this.DataContext = this;
            SetBlackOutDates();
            InitializeDoctorComboBox();
            FillLabels();
            
        }
        public void FillLabels()
        {
            Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(appointment.doctor.LicenceNumber) as Doctor;
            chosen_doctor.Content = d.FirstName + " " + d.LastName;
            drInfo = new DoctorInfo(d.FirstName + " " + d.LastName, d.LicenceNumber);

            date.Text = appointment.beginningDate.Date.ToString("MM/dd/yyyy");
            comboTime.Text = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
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
            doctorInfoList = new ObservableCollection<DoctorInfo>();
            foreach (Doctor doctor in App.accountController.GetAllDoctorAccounts())
            {
                doctorInfoList.Add(new DoctorInfo(doctor.FirstName + " " + doctor.LastName, doctor.LicenceNumber));
            }
        }

        private void Choose_doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drInfo = choose_doctor.SelectedItem as DoctorInfo;
            chosen_doctor.Content = drInfo.doctorName;

            InitializeListOfAppointments();
            
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string date = this.date.ToString();
            pickedDate = DateTime.Parse(date);
            InitializeListOfAppointments();
           
        }

        private void ComboTime_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            comboTime.ItemsSource = listOfAppointmentTime;
        }
        private void InitializeListOfAppointments()
        {
            List<string> list;
            list = App.appointmentController.GetTakenAppointmentsForPatient(patient, pickedDate, drInfo.licenceNumber);
              
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
                if (time.Equals(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm")))
                {
                    if (drInfo.licenceNumber.Equals(appointment.doctor.LicenceNumber))
                    {
                        continue;   // u pitanju je isti doktor, znaci to vreme je dozvoljeno
                    }
                    else
                    {
                        Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(drInfo.licenceNumber) as Doctor;
                        if (App.appointmentController.CheckIfDoctorIsAvailable(d, appointment.beginningDate))
                            continue;       // dr slobodan
                        else
                            listOfAppointmentTime.Remove(time);
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
            DateTime start = DateTime.Parse(this.date.ToString());
            DateTime startDate = start.Date;

            TimeSpan timeStart = TimeSpan.Parse(this.comboTime.SelectionBoxItem.ToString());
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
                    room = App.appointmentController.GetAvailableRoom(startDate);
                else
                    room = this.appointment.room;
                
                Appointment newAppointment = new Appointment()
                {
                    appointmentID = this.appointment.appointmentID,
                    beginningDate = startDate,
                    endDate = startDate.AddMinutes(15),
                    room = room,
                    doctor = doctor,
                    patient = patient,
                    operation = false,
                    isDelayedByPatient = true,
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
