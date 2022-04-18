using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
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

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Window window;
        private AccountController accountController;

        public LoginWindow(Window newWindow, AccountController accountController)
        {
            InitializeComponent();
            window = newWindow;
            this.accountController = accountController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            if(window is PatientHome)
            {
                List<Patient> patients = accountController.GetAllPatientAccounts();
                foreach(Patient patient in patients)
                {
                    if(patient.Username.Equals(username) && patient.Password.Equals(password))
                    {
                        this.Close();
                        window.Show();
                        return;
                    }
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Text = "";
            }
            else if(window is DoctorHome)
            {
                List<Doctor> doctors = accountController.GetAllDoctorAccounts();
                foreach (Doctor doctor in doctors)
                {
                    if (doctor.Username.Equals(username) && doctor.Password.Equals(password))
                    {
                        this.Close();
                        window.Show();
                        return;
                    }
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Text = "";
            }


            
        }
    }
}
