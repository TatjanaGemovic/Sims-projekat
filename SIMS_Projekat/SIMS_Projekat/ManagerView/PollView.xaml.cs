using SIMS_Projekat.ManagerViewModel;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for PollView.xaml
    /// </summary>
    public partial class PollView : Page
    {
        public PollView(Evaluation poll)
        {
            InitializeComponent();
            this.DataContext = new PollViewModel(poll);
        }
    }
}
