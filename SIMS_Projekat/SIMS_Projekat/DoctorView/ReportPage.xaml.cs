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
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private Frame frame;
        private Doctor doctor;
        private DoctorHomePage homePage;

        public ReportPage(Frame f, Doctor d, DoctorHomePage doctorHomePage)
        {
            frame = f;
            doctor = d;
            homePage = doctorHomePage;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new DoctorAppointments(frame, doctor, homePage);
        }

        private void Od_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string fromDate = Od.SelectedDate.ToString();
            string untilDate = Do.SelectedDate.ToString();
        }
    }
}
