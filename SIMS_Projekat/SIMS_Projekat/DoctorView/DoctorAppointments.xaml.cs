using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DoctorAppointments.xaml
    /// </summary>
    public partial class DoctorAppointments : Page
    {
        Frame MainFrame;
        Doctor doctor;
        private DoctorHomePage doctorHomePage;

        public DoctorAppointments(Frame main, Doctor d, DoctorHomePage doctorHomePage1)
        {
            InitializeComponent();
            doctor = d;
            MainFrame = main;
            doctorHomePage = doctorHomePage1;
        }
        public DoctorAppointments(Frame main, Doctor d)
        {
            InitializeComponent();
            doctor = d;
            MainFrame = main;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Scheduling(MainFrame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PatientList(MainFrame, doctor);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Content= new Menu(MainFrame, doctor, doctorHomePage);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Examinations(MainFrame, doctor);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Medicines(MainFrame, doctor);
        }
    }
}
