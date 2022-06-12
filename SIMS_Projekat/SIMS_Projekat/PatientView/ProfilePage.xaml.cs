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

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        Frame frame;
        Patient patient;
        public ProfilePage(Frame frame, Patient p)
        {
            this.frame = frame;
            patient = p;
            InitializeComponent();
            FillFields();
        }
        public void FillFields()
        {
            ime.Content = patient.FirstName + " " + patient.LastName;
            rodjendan.Content = patient.DateOfBirth.ToString("dd.MM.yyyy.");
            jmbg.Content = patient.Jmbg;
            posta.Content = patient.Email;
            telefon.Content = patient.PhoneNumber;
            osiguranje.Content = patient.HealthInsuranceID;
            visina.Content = patient.Height + " cm";
            tezina.Content = patient.Weight + " kg";
            username.Content = patient.Username;
            password.Content = patient.Password;

            if (patient.BloodType == BloodType.O_Positive)
                krvna.Content = "O pozitivna";
            else if (patient.BloodType == BloodType.O_Negative)
                krvna.Content = "O negativna";
            else if (patient.BloodType == BloodType.A_Positive)
                krvna.Content = "A pozitivna";
            else if (patient.BloodType == BloodType.A_Negative)
                krvna.Content = "A negativna";
            else if (patient.BloodType == BloodType.B_Positive)
                krvna.Content = "B pozitivna";
            else if (patient.BloodType == BloodType.B_Negative)
                krvna.Content = "B negativna";
            else if (patient.BloodType == BloodType.AB_Positive)
                krvna.Content = "AB pozitivna";
            else if (patient.BloodType == BloodType.AB_Negative)
                krvna.Content = "AB negativna";

        }
    }
}
