using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.SecretaryView.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatientUserControl.xaml
    /// </summary>
    public partial class AddMeetingUserControl : UserControl
    {
        public AddMeetingUserControl(ContentControl contentControl)
        {
            InitializeComponent();
            this.DataContext = new AddMeetingViewModel(contentControl, App.appointmentController, App.roomController, App.MeetingController, App.accountController);    

        }
    }
}
