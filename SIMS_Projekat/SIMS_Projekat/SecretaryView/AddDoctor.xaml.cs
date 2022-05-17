using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.SecretaryView.Converters;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        public AccountController AccountController { get; set; }
        public List<DoctorSpeciality> DoctorSpecialitiesList { get; set; }
        public AddDoctor(AccountController accountController)
        {
            InitializeComponent();
            this.DataContext = this;
            AccountController = accountController;
            DoctorSpecialitiesList = new List<DoctorSpeciality>();
            foreach(DoctorSpeciality doctorSpeciality in Enum.GetValues(typeof(DoctorSpeciality)))
            {
                DoctorSpecialitiesList.Add(doctorSpeciality);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorSpeciality selectedSpeciality = DoctorSpeciality.GENERAL_PRACTITIONER;
            Enum.TryParse(SpecialityComboBox.Text, out selectedSpeciality);
            var newDoctor = new Doctor()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = DateTime.Parse(Date.Text),
                Jmbg = Jmbg.Text,
                Email = Email.Text,
                PhoneNumber = PhoneNumber.Text,
                LicenceNumber = LicenceNumber.Text,
                Username = Username.Text,
                Password = Password.Password,
                Speciality = selectedSpeciality
            };
            AccountController.CreateDoctorAccount(newDoctor);
            AccountsView.AddDoctor(newDoctor);
            Close();
        }
    }
}
