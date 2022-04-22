using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
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
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {
        private Patient patient;
        private string nameSurname;
        public PatientHome(Patient p)
        {
            InitializeComponent();
            patient = p;
            nameSurname = p.FirstName + " " + p.LastName;
            name_surname.Content = nameSurname;
            MainFrame.Content = new Homepage(patient);
        }

        private void make_appointment_Click(object sender, RoutedEventArgs e)
        {
            //Homepage homepage = new Homepage();
            //homepage.ShowsNavigationUI = true;
            MainFrame.Content = new Appointments(MainFrame, patient);
            
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void homepage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Homepage(patient);
        }
    }
}
