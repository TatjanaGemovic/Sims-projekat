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
    /// Interaction logic for EvaluationPage.xaml
    /// </summary>
    public partial class EvaluationPage : Page
    {
        public EvaluationPage(Frame frame, Patient patient, Evaluation evaluation)
        {
            InitializeComponent();
            this.DataContext = new EvaluationViewModel(frame, evaluation);
        }

    }
}
