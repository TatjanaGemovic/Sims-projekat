using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Page
    {
        Frame Frame;
        Doctor doctor;
        public BindingList<Request2> RequestList { get; set; }
        public Requests(Frame main, Doctor d)
        {
            Frame = main;
            doctor = d;
            InitializeComponent();
            RequestList = new BindingList<Request2>();
            createList();
            RequestsList.ItemsSource = RequestList;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Posalji_zahtev_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new FreeDayRequest(Frame, doctor);
        }

        public class Request2
        {
            public string fromUntil { get; set; }
            public string requestStatus { get; set; }

            public Request2(string fromUntil, string requestStatus)
            {
                this.fromUntil = fromUntil;
                this.requestStatus = requestStatus;
            }
        }

        public void createList()
        {
            if (App.freeDayRequestRepository.GetRequests() != null)
            {
                foreach (Model.FreeDayRequest r in App.freeDayRequestRepository.GetRequests())
                {
                    if (r.doctor.Equals(doctor))
                    {
                        string from1 = r.from.ToString();
                        string until1 = r.until.ToString();
                        String[] parts = from1.Split(" ");
                        String[] parts2 = until1.Split(" ");
                        string time = parts[0] + "  -  " + parts2[0];
                        string status;
                        if (r.status.ToString().Equals("Waiting"))
                        {
                            status = "Na cekanju";
                        }else if (r.status.ToString().Equals("Accepted"))
                        {
                            status = "Prihvacen";
                        }
                        else
                        {
                            status = "Odbijen";
                        }

                        RequestList.Add(new Request2(time, status));
                    }
                }
            }
        }
    }
}
