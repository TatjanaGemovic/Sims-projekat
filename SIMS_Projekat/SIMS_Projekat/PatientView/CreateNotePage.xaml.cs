using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
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
    /// Interaction logic for CreateNotePage.xaml
    /// </summary>
    public partial class CreateNotePage : Page
    {
        public CreateNotePage(Frame frame, Patient patient, bool fromReport, ReportViewModel vmReport = null)
        {
            InitializeComponent();
            this.DataContext = new CreateNotePageViewModel(frame, patient, fromReport, vmReport);
        }
    }
}
