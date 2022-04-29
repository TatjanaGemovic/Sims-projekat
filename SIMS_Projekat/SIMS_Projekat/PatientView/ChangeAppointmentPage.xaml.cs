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

            InitializeDoctorComboBox();
            Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(appointment.doctor.LicenceNumber) as Doctor;
            chosen_doctor.Content = d.FirstName + " " + d.LastName;
            drInfo = new DoctorInfo(d.FirstName + " " + d.LastName, d.LicenceNumber);

            string appointmentDate = appointment.beginningDate.Date.ToString("MM/dd/yyyy");
            date.Text = appointmentDate;

            string appointmentTime = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
            comboTime.Text = appointmentTime;
         
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
            listOfAppointmentTime = new BindingList<String>();
            listOfTakenAppointmentTime = new BindingList<String>(list);

            // Allow new parts to be added, but not removed once committed.        
            listOfAppointmentTime.AllowNew = true;
            listOfAppointmentTime.AllowRemove = true;

            // Raise ListChanged events when new parts are added.
            listOfAppointmentTime.RaiseListChangedEvents = true;

            // Do not allow parts to be edited.
            listOfAppointmentTime.AllowEdit = false;
            CreateList();

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

        private void CreateList()
        {
            listOfAppointmentTime.Add("08:00");
            listOfAppointmentTime.Add("08:15");
            listOfAppointmentTime.Add("08:30");
            listOfAppointmentTime.Add("08:45");
            listOfAppointmentTime.Add("09:00");
            listOfAppointmentTime.Add("09:15");
            listOfAppointmentTime.Add("09:30");
            listOfAppointmentTime.Add("09:45");
            listOfAppointmentTime.Add("10:00");
            listOfAppointmentTime.Add("10:15");
            listOfAppointmentTime.Add("10:30");
            listOfAppointmentTime.Add("10:45");
            listOfAppointmentTime.Add("11:00");
            listOfAppointmentTime.Add("11:15");
            listOfAppointmentTime.Add("11:30");
            listOfAppointmentTime.Add("11:45");
            listOfAppointmentTime.Add("12:00");
            listOfAppointmentTime.Add("12:15");
            listOfAppointmentTime.Add("12:30");
            listOfAppointmentTime.Add("12:45");
            listOfAppointmentTime.Add("13:00");
            listOfAppointmentTime.Add("13:15");
            listOfAppointmentTime.Add("13:30");
            listOfAppointmentTime.Add("13:45");
            listOfAppointmentTime.Add("14:00");
            listOfAppointmentTime.Add("14:15");
            listOfAppointmentTime.Add("14:30");
            listOfAppointmentTime.Add("14:45");
            listOfAppointmentTime.Add("15:00");
            listOfAppointmentTime.Add("15:15");
            listOfAppointmentTime.Add("15:30");
            listOfAppointmentTime.Add("15:45");
            listOfAppointmentTime.Add("16:00");
            listOfAppointmentTime.Add("16:15");
            listOfAppointmentTime.Add("16:30");
            listOfAppointmentTime.Add("16:45");
        }
    }
}
