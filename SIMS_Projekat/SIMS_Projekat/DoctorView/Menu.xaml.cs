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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private Frame Frame;
        Doctor doctor;
        private DoctorHomePage doctorHomePage;

        public Menu(Frame mainFrame, Doctor doctor1, DoctorHomePage doctorHomePage1)
        {
            InitializeComponent();
            Frame = mainFrame;
            doctor = doctor1;
            doctorHomePage = doctorHomePage1;
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor, doctorHomePage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            doctorHomePage.Close();
        }
    }
}
