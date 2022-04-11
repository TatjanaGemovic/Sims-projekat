using SIMS_Projekat.Controller;
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
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class ViewPatient : Window
    {
        public ViewPatient(Patient patient)
        {
            InitializeComponent();

            FirstName.Text = patient.FirstName;
            LastName.Text = patient.LastName;
            Date.Text = patient.DateOfBirth.ToString();
            Jmbg.Text = patient.Jmbg;
            Email.Text = patient.Email;
            PhoneNumber.Text = patient.PhoneNumber;
            HealthInsuranceID.Text = patient.PhoneNumber;
            Height.Text = patient.Height.ToString();
            Weight.Text = patient.Weight.ToString();
            Username.Text = patient.Username;
            BloodType.Text = patient.BloodType.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            Close();
        }
    }
}
