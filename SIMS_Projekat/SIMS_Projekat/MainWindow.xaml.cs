using SIMS_Projekat.PatientView;
﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountRepository accountRepository;
        private AccountController accountController;
        private AllergenController allergenController;
        private RoomController roomController;
        public int prozor;

        public MainWindow()
        {
            InitializeComponent();
            accountRepository = App.accountRepository;
            accountController = App.accountController;
            allergenController = App.AllergenController;
            roomController = App.roomController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            prozor = 1;
            LoginWindow loginWindow = new LoginWindow(prozor, accountController, accountRepository, allergenController, roomController);
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            prozor = 2;
            LoginWindow loginWindow = new LoginWindow(prozor, accountController, accountRepository, allergenController, roomController);
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            prozor = 3;
            LoginWindow loginWindow = new LoginWindow(prozor, accountController, accountRepository, allergenController, roomController);
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            prozor = 4;
            LoginWindow loginWindow = new LoginWindow(prozor, accountController, accountRepository, allergenController, roomController);
            loginWindow.Show();
            this.Close();
        }

    }
}
