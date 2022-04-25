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
    /// Interaction logic for ChooseDoctorPage.xaml
    /// </summary>
    public partial class ChooseDoctorPage : Page
    {
        Patient patient;
        public ChooseDoctorPage(Patient p)
        {
            patient = p;
            InitializeComponent();

        }
    }
}
