﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.DoctorView;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
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

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private AccountController accountController;
        private AccountRepository accountRepository;
        private AllergenController allergenController;
        private RoomController roomController;
        private int prozor;
        public static Patient patient;

        public LoginWindow(int prozor1, AccountController accountController, AccountRepository accountRepository, AllergenController allergenController, RoomController roomController)
        {
            InitializeComponent();
            //window = newWindow;
            prozor = prozor1;
            this.accountController = accountController;
            this.accountRepository = accountRepository;
            this.allergenController = allergenController;
            this.roomController = roomController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;

            if(prozor == 3)
            {
                List<Patient> patients = accountController.GetAllPatientAccounts();
                foreach(Patient pat in patients)
                {
                    if(pat.Username.Equals(username) && pat.Password.Equals(password))
                    {
                        patient = pat;
                        PatientHome patientHomePage = new PatientHome(patient);
                        this.Close();
                        patientHomePage.Show();
                        return;
                    }
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Password = "";
            }
            else if(prozor == 1)
            {
                List<Doctor> doctors = accountController.GetAllDoctorAccounts();
                foreach (Doctor doctor in doctors)
                {
                    if (doctor.Username.Equals(username) && doctor.Password.Equals(password))
                    {
                        DoctorHomePage doctorHomePage = new DoctorHomePage(doctor);
                        this.Close();
                        doctorHomePage.Show();
                        return;
                    }
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Password = "";
            }
            else if (prozor == 2)
            {
                if (username.Equals("tatamata") &&password.Equals("123"))
                {
                    ManagerHome managerHomePage = new ManagerHome();
                    this.Close();
                    managerHomePage.Show();
                    return;
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Password = "";
            }
            else if (prozor == 4)
            {
                if (username.Equals("sekretar") && password.Equals("123"))
                {
                    SecretaryHome secretaryHomePage = new SecretaryHome(accountRepository, accountController, allergenController, roomController);
                    this.Close();
                    secretaryHomePage.Show();
                    return;
                }
                WrongDataLabel.Content = "Pogresni podaci";
                Username.Text = "";
                Password.Password = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
