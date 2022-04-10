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
        
        public PatientHome()
        {
            InitializeComponent();
            MainFrame.Content = new Homepage();
        }

        private void zakazite_termin_Click(object sender, RoutedEventArgs e)
        {
            //Homepage homepage = new Homepage();
            //homepage.ShowsNavigationUI = true;
            MainFrame.Content = new Appointments(MainFrame);
            
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }
    }
}
