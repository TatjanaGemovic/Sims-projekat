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

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Window
    {
        Doctor doctor;
        public DoctorHomePage(Doctor d)
        {
            InitializeComponent();
            doctor = d;
            MainFrame.Content = new DoctorAppointments(MainFrame, doctor);
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }
    }
}
