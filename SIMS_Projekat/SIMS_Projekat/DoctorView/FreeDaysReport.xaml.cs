using SIMS_Projekat.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for FreeDaysReport.xaml
    /// </summary>
    public partial class FreeDaysReport : Page
    {
        private Doctor doctor;
        private Frame frame;
        public BindingList<ViewModelChart> Data { get; set; }

        public FreeDaysReport(Frame f, Doctor d)
        {
            InitializeComponent();
            doctor = d;
            frame = f;
            Data = new BindingList<ViewModelChart>();
            LoadData();

            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Requests(frame, doctor);
        }

        public class ViewModelChart
        {
            public int RequestsNum { get; set; }
            public string Month { get; set; }

            public ViewModelChart(string m, int br)
            {
                RequestsNum = br;
                Month = m;
            }
        }

        public void LoadData()
        {
            string mesec = "Maj";
            int num;
            for (int month = 5; month <= 12; month++)
            {
                num = 0;
                foreach (Model.FreeDayRequest r in App.freeDayRequestRepository.GetRequests())
                {
                    if (r.doctor.Equals(doctor) && r.from.Month == month)
                    {
                        num++;
                    }
                }
                if (month == 6)
                    mesec = "Jun";
                else if (month == 7)
                    mesec = "Jul";
                else if (month == 8)
                    mesec = "Avg";
                else if (month == 9)
                    mesec = "Sep";
                else if (month == 10)
                    mesec = "Okt";
                else if (month == 11)
                    mesec = "Nov";
                else if (month == 12)
                    mesec = "Dec";
                else
                    mesec = "Maj";

                Data.Add(new ViewModelChart(mesec, num));
            }
        }
    }
}
