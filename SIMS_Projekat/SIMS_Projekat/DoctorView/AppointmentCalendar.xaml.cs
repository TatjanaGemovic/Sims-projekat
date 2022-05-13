using SIMS_Projekat.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for AppointmentCalendar.xaml
    /// </summary>
    public partial class AppointmentCalendar : Page
    {
        Frame Frame;
        public String SelectedDate;
        Doctor doctor;
        public AppointmentCalendar(Frame MainFrame, Doctor d)
        {
            InitializeComponent();
            doctor = d;
            Frame = MainFrame;
        }

        private void CalendarItem_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = Kalendar1.SelectedDate.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate = Kalendar1.SelectedDate.ToString();
            Frame.Content = new Scheduling(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DoctorAppointments appointments = new DoctorAppointments(Frame, doctor);
            Frame.Content = appointments;
        }
    }
}
