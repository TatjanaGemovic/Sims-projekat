using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private Frame frame;
        private Doctor doctor;
        //private DoctorHomePage homePage;
        List<String> meseci;
        List<PdfData> data;
        public BindingList<ReportChartData> DataChart { get; set; }

        public ReportPage(Frame f, Doctor d)
        {
            frame = f;
            doctor = d;
            data = new List<PdfData>();
            DataChart = new BindingList<ReportChartData>();
            InitializeComponent();
            InitializeComboBox();
            LoadChartData();

            this.DataContext = this;
        }

        private void InitializeComboBox()
        {
            meseci = new List<string>();
            meseci.Add("Januar");
            meseci.Add("Februar");
            meseci.Add("Mart");
            meseci.Add("April");
            meseci.Add("Maj");
            meseci.Add("Jun");
            meseci.Add("Jul");
            meseci.Add("Avgust");
            meseci.Add("Septembar");
            meseci.Add("Oktobar");
            meseci.Add("Novembar");
            meseci.Add("Decembar");
            Mesec.ItemsSource = meseci;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new DoctorAppointments(frame, doctor);
        }

        private void Generisi_Izvestaj_Click(object sender, RoutedEventArgs e)
        {
            InitializeList();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Izvestaj o zakazanim terminima";
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 36, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14);

            string temp = "Lista pregleda za " + Mesec.SelectedItem.ToString();
            gfx.DrawString(temp, font, XBrushes.Black, new XPoint(100, 70));
            gfx.DrawLine(new XPen(XColor.FromArgb(103, 111, 163)), new XPoint(100, 100), new XPoint(500, 100));
            gfx.DrawString("Termin", font2, XBrushes.Black, new XPoint(100, 280));
            gfx.DrawString("Ime pacijenta", font2, XBrushes.Black, new XPoint(200, 280));
            gfx.DrawString("Tip", font2, XBrushes.Black, new XPoint(400, 280));

            gfx.DrawLine(new XPen(XColor.FromArgb(103, 111, 163)), new XPoint(50, 290), new XPoint(550, 290));

            int currYposition_values = 303;
            int currYposition_line = 310;

            foreach(PdfData p in data)
            {
                gfx.DrawString(p.Date, font2, XBrushes.Black, new XPoint(100, currYposition_values));
                gfx.DrawString(p.PatientName, font2, XBrushes.Black, new XPoint(200, currYposition_values));
                gfx.DrawString(p.Type, font2, XBrushes.Black, new XPoint(400, currYposition_values));
                gfx.DrawLine(new XPen(XColor.FromArgb(205, 222, 255)), new XPoint(50, currYposition_line), new XPoint(550, currYposition_line));

                currYposition_values += 20;
                currYposition_line += 20;
            }

            document.Save(@".\..\..\..\Resources\pdf_files\Test.pdf");
        }

        private void Mesec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mesec.SelectedItem != null)
            {
                Generisi_Izvestaj.IsEnabled = true;
                InitializeList();
            }
        }

        private void LoadChartData()
        {
            int num;
            for (int month2 = 1; month2 <= 12; month2++)
            {
                num = 0;
                foreach (Appointment a in App.appointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
                {
                    string datum = a.beginningDate.ToString();
                    String[] deoDatuma = datum.Split("/");
                    int mesec = int.Parse(deoDatuma[0]);
                    if (month2 == mesec)
                    {
                        num++;
                    }
                }
                foreach(FinishedAppointment f in App.finishedAppointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
                {
                    string datum = f.beginningDate.ToString();
                    String[] deoDatuma = datum.Split("/");
                    int mesec = int.Parse(deoDatuma[0]);
                    if (month2 == mesec)
                    {
                        num++;
                    }
                }
                DataChart.Add(new ReportChartData(month2, num));
            }
        }

        private void InitializeList()
        {
            string selectedMonth = Mesec.SelectedItem.ToString();
            data = App.listsForBinding.CreateListForPdf(selectedMonth, doctor);
        }

        public class ReportChartData
        {
            public int AppNum { get; set; }
            public int Days { get; set; }

            public ReportChartData(int m, int br)
            {
                AppNum = br;
                Days = m;
            }
        }

        public class PdfData
        {
            public string Date { get; set; }
            public string PatientName { get; set; }
            public string Type { get; set; }

            public PdfData(string d, string pn, string t)
            {
                Date = d;
                PatientName = pn;
                Type = t;
            }
        }
    }
}
