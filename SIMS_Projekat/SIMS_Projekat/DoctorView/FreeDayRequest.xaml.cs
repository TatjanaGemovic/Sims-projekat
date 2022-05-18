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
    /// Interaction logic for FreeDayRequest.xaml
    /// </summary>
    public partial class FreeDayRequest : Page
    {
        Frame Frame;
        Doctor doctor;
        public FreeDayRequest(Frame main, Doctor d)
        {
            Frame = main;
            doctor = d;
            InitializeComponent();
            SetBlackOutDates();
        }

        private void SetBlackOutDates()
        {
            DateTime firstDay = DateTime.Today.AddDays(2);
            DateTime lastDay = DateTime.Today.AddMonths(6);

            Od.DisplayDateStart = firstDay;
            Od.DisplayDateEnd = lastDay;

            Do.DisplayDateStart = firstDay.AddDays(1);
            Do.DisplayDateEnd = lastDay;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Requests(Frame, doctor);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            String from1 = Od.SelectedDate.ToString();
            String until1 = Do.SelectedDate.ToString();
            bool urgent;
            if ((bool)Hitno.IsChecked)
            {
                urgent = true;
            }
            else
            {
                urgent = false;
            }

            Model.FreeDayRequest request = new Model.FreeDayRequest()
            {
                from = DateTime.Parse(from1),
                until = DateTime.Parse(until1),
                sentDate = DateTime.Now,
                doctor = doctor,
                secretaryComment = Razlog.Text,
                status = Status.Waiting,
                isUrgent = urgent
            };

            App.freeDayRequestRepository.AddRequest(request);

            App.freeDayRequestRepository.Serialize();
            Frame.Content = new Requests(Frame, doctor);
        }
    }
}
