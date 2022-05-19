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
            DateTime from2 = DateTime.Parse(from1);
            DateTime until2 = DateTime.Parse(until1);
            bool can = true;
            bool urgent;
            if ((bool)Hitno.IsChecked)
            {
                urgent = true;
            }
            else
            {
                urgent = false;
            }
            int var = from2.CompareTo(until2);
            if (var == 0 || var > 0)
            {
                can = false;
            }

            if (!urgent)
            {
                foreach (Doctor d in App.accountRepository.GetAllDoctorAccountBySpeciality(doctor.Speciality.ToString()))
                {
                    //if(d != doctor)
                    //{
                    foreach (Model.FreeDayRequest f in App.freeDayRequestRepository.GetRequests())
                    {
                        int temp = f.from.CompareTo(from2); //pocetni isti
                        int temp2 = f.until.CompareTo(until2); //krajnji isti
                        int temp3 = f.until.CompareTo(from2); //kraj-pocetak
                        int temp4 = f.from.CompareTo(until2); // ppocetak-kraj
                        if (temp == 0 || temp2 == 0 || temp3 == 0 || temp4 == 0)
                        {
                            can = false;
                        }
                        else if (temp > 0 && temp4 < 0 || temp2 < 0 && temp3 > 0 || temp > 0 && temp2 < 0 || temp < 0 && temp2 > 0)
                        {
                            can = false;
                        }
                    }
                    //}
                }
            }


            if (can)
            {
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
}
