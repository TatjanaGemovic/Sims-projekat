﻿using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for EditDoctorAppointment.xaml
    /// </summary>
    public partial class EditDoctorAppointment : Page
    {
        Frame Frame;
        int id;
        Appointment appoint;
        String selectedDate1;
        BindingList<String> appointmentType;
        BindingList<String> patients;
        BindingList<String> rooms1;
        BindingList<String> rooms2;
        BindingList<String> listofTakenAppointmentTime;
        BindingList<String> listofAppointmentTime;
        private List<Patient> patients2;
        Doctor doctor;
        Room selectedRoom;
        bool op;
        Patient selectedPatient;
        public EditDoctorAppointment(Frame frame, Appointment app, DateTime selectedDate, Doctor d)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            selectedDate1 = selectedDate.ToString("MM/dd/yyyy HH:mm");
            String[] datePart = selectedDate1.Split(" ");
            selectedDate1 = datePart[0];
            DateTime pickedDate = selectedDate.Date;
            id = app.appointmentID;
            appoint = app;

            InitializeComboBox1();
            InitializeComboBox2();

            if (app.operation.ToString().Equals("False")){
                InitializeComboBox4();
                Tip_operacije.SelectedItem = appointmentType[0];
            }
            else
            {
                InitializeComboBox3();
                Tip_operacije.SelectedItem = appointmentType[1];
            }
            int it = 0;
            foreach (Patient p in App.accountController.GetAllPatientAccounts())
            {
                string str = p.FirstName + " " + p.LastName;
                if (str.Equals(app.patient.FirstName + " " + app.patient.LastName))
                {
                    Ime_pacijenta.SelectedItem = patients[it];
                }
                it++;
            }
            int it1 = 0;
            int it2 = 0;
            if (app.operation)
            {
                foreach (Room r in App.roomController.GetRoomsByType(RoomType.operatingRoom))
                {
                    if (r.RoomNumber.Equals(app.room.RoomNumber))
                    {
                        Ime_sobe.SelectedItem = rooms1[it1];
                    }
                    it1++;
                }
            }
            else
            {
                foreach (Room r in App.roomController.GetRoomsByType(RoomType.examRoom))
                {
                    if (r.RoomNumber.Equals(app.room.RoomNumber))
                    {
                        Ime_sobe.SelectedItem = rooms2[it2];
                    }
                    it2++;
                }
            }
        }

        private void InitializeComboBox1()
        {
            appointmentType = new BindingList<String>();
            appointmentType.Add("Pregled");
            appointmentType.Add("Operacija");
            Tip_operacije.ItemsSource = appointmentType;
        }

        private void InitializeComboBox2()
        {
            patients = new BindingList<String>();
            patients2 = App.accountController.GetAllPatientAccounts();

            foreach (Patient p in patients2)
            {
                patients.Add(p.FirstName + " " + p.LastName);
            }
            Ime_pacijenta.ItemsSource = patients;
        }
        private void InitializeComboBox3()
        {
            rooms1 = new BindingList<String>();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.pRoomType == RoomType.examRoom)
                {
                    rooms1.Add(r.RoomNumber.ToString());
                }
            }
            Ime_sobe.ItemsSource = rooms1;
        }
        private void InitializeComboBox4()
        {
            rooms2 = new BindingList<String>();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.pRoomType == RoomType.operatingRoom)
                {
                    rooms2.Add(r.RoomNumber.ToString());
                }
            }
            Ime_sobe.ItemsSource = rooms2;
        }

        private void InitializeListOfAppointments()
        {
            List<String> list = new List<string>();

            AppointmentServiceDTO dto = new AppointmentServiceDTO()
            {
                doctor = doctor,
                date = selectedDate1,
                patient = selectedPatient,
                room = selectedRoom
            };

            list = App.appointmentController.GetAvailableAppointmentsForDoctor(dto);
            
            listofAppointmentTime = new BindingList<String>();
            listofTakenAppointmentTime = new BindingList<String>(list);

            listofAppointmentTime.AllowNew = true;
            listofAppointmentTime.AllowRemove = true;

            listofAppointmentTime.RaiseListChangedEvents = true;

            listofAppointmentTime.AllowEdit = false;
            CreateList();

            foreach (string time in listofTakenAppointmentTime)
            {
                listofAppointmentTime.Remove(time);
            }
            Vreme_pocetka.ItemsSource = listofAppointmentTime;
        }

        private void Vreme_pocetka_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Vreme_pocetka.ItemsSource = listofAppointmentTime;
        }

        private void Ime_Pacijent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String patient = Ime_pacijenta.SelectedItem.ToString();
            foreach (Patient p in App.accountController.GetAllPatientAccounts())
            {
                string str = p.FirstName + " " + p.LastName;
                if (str.Equals(patient))
                {
                    selectedPatient = p;
                }
            }
            if (Ime_sobe.SelectedItem != null && Tip_operacije.SelectedItem != null)
            {
                Vreme_pocetka.IsHitTestVisible = true;
                Vreme_pocetka.IsEnabled = true;
                InitializeListOfAppointments();
            }
        }

        private void Tip_operacije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String tip = Tip_operacije.SelectedItem.ToString();
            if (tip.Equals("Pregled"))
            {
                op = false;
                InitializeComboBox3();
            }
            else
            {
                op = true;
                InitializeComboBox4();
            }
            Ime_sobe.IsEnabled = true;
        }

        private void Ime_Sobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String id = Ime_sobe.SelectedItem.ToString();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.RoomNumber == int.Parse(id))
                {
                    selectedRoom = r;
                }
            }
            if (Ime_pacijenta.SelectedItem != null && Tip_operacije.SelectedItem != null)
            {
                Vreme_pocetka.IsHitTestVisible = true;
                Vreme_pocetka.IsEnabled = true;
                InitializeListOfAppointments();
            }
        }

        private void Vreme_pocetka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ime_pacijenta.SelectedItem != null && Tip_operacije.SelectedItem != null && Ime_sobe.SelectedItem != null)
            {
                Izmeni.IsEnabled = true;
            }
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }
        private void Promeni_Click(object sender, RoutedEventArgs e)
        {
            string dateFromPage = selectedDate1.ToString();
            DateTime start = DateTime.Parse(dateFromPage);
            DateTime startDate = start.Date;

            string timeFromPage = this.Vreme_pocetka.SelectionBoxItem.ToString();
            TimeSpan timeStart = TimeSpan.Parse(timeFromPage);
            startDate = startDate.Add(timeStart);

            Appointment appointment = new Appointment()
            {
                appointmentID = id,
                beginningDate = startDate,
                endDate = startDate.AddMinutes(15),
                operation = op,
                room = selectedRoom,
                doctor = doctor,
                patient = selectedPatient,
                isScheduledByPatient = appoint.isScheduledByPatient,
                isDelayedByPatient = appoint.isDelayedByPatient,
            };

            App.appointmentController.SetAppointment(appointment);
            App.appointmentRepo.Serialize(); 
            Scheduling scheduling = new Scheduling(Frame, doctor);
            Frame.Content = scheduling;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scheduling scheduling = new Scheduling(Frame, doctor);
            Frame.Content = scheduling;
        }

        private void CreateList()
        {
            listofAppointmentTime = new BindingList<String>(App.listsForBinding.CreateAppointmentTimeList());
        }
    }
}
