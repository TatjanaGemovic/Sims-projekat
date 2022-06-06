using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for FreeDayRequest.xaml
    /// </summary>
    public partial class FreeDayRequest : Page
    {
        public FreeDayRequest(Frame main, Doctor d)
        {
            InitializeComponent();
            this.DataContext = new FreeDayRequestViewModel(main, d);
        }
    }
}
