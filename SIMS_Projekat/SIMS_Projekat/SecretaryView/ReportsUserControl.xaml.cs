using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
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
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddUrgentPatientUserControl .xaml
    /// </summary>
    public partial class ReportsUserControl : UserControl
    {
        private RoomController roomController = App.roomController;
        private AppointmentController appointmentController = App.appointmentController;
        public ObservableCollection<RoomOccupancy> RoomOccupancies { get; set; }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return _startDate; }
            set 
            { 
                _startDate = value;
                TimeSpan timeStart = new TimeSpan(1, 0, 0);
                _startDate = _startDate.Date + timeStart;
            }
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                TimeSpan timeEnd = new TimeSpan(23, 59, 59);
                _endDate = _endDate.Date + timeEnd;
                PopulateList();
            }
        }


        public ReportsUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            RoomOccupancies = new ObservableCollection<RoomOccupancy>();
        }

        private void PopulateList()
        {
            RoomOccupancies.Clear();
            List<Room> rooms = roomController.GetRooms();

            foreach(Room room in rooms)
            {
                List<Appointment> appointmentsForRoom = appointmentController.GetAppointmentsForTimeSpan(room, StartDate, EndDate);
                int minutes = appointmentsForRoom.Count * 15;
                int hours = 0;
                if (minutes != 0)
                {
                    hours = minutes / 60;
                    minutes -= hours * 60 ;
                }
                int maxHours = (EndDate.DayOfYear - StartDate.DayOfYear) * 9;

                RoomOccupancies.Add(new RoomOccupancy()
                {
                    Room = room,
                    Hours = hours,
                    Minutes = minutes,
                    MaxHours = maxHours
                });
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GeneratePDFReport();
        }


        private void GeneratePDFReport()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Izveštaj o zauzetosti prostorija";
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontTitle = new XFont("Futura Round Demi", 32, XFontStyle.Regular);
            XFont fontDates = new XFont("Futura Round Demi", 22, XFontStyle.Regular);
            XFont fontLogo = new XFont("Futura Round Demi", 16, XFontStyle.Regular);
            XFont fontTime= new XFont("Futura Round Demi", 12, XFontStyle.Regular);
            XFont fontTableHeader = new XFont("Futura Round", 14, XFontStyle.Bold);
            XFont fontTableBody = new XFont("Futura Round", 13, XFontStyle.Regular);



            // Header
            XImage img = XImage.FromFile(@".\..\..\..\Resources\icons_doctor\cardiogram (3).png");
            gfx.DrawImage(img, 10, 10, 30, 30);
            gfx.DrawString("Zdravo korporacija", fontLogo, XBrushes.DimGray, new XPoint(50, 30));

            DateTime dateTimeNow = DateTime.Now;
            string dateTimeNowString = dateTimeNow.Day + "/" + dateTimeNow.Month + "/" + dateTimeNow.Year + " " + dateTimeNow.Hour + ":" + dateTimeNow.Minute;

            gfx.DrawString(dateTimeNowString, fontTime, XBrushes.DimGray, new XPoint(480, 30));


            // Title
            gfx.DrawString("Izveštaj o zauzetosti prostorija", fontTitle, XBrushes.Black, new XRect(0, 80, page.Width, page.Height), XStringFormats.TopCenter);

            string dateRange = StartDate.Day + "/" + StartDate.Month + "/" + StartDate.Year + " - " + EndDate.Day + "/" + EndDate.Month + "/" + EndDate.Year;
            gfx.DrawString(dateRange, fontDates, XBrushes.Black, new XRect(0, 125, page.Width, page.Height), XStringFormats.TopCenter);

            // Table

            // Table header
            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(90, 200), new XPoint(510, 200));
            gfx.DrawString("Broj sobe", fontTableHeader, XBrushes.Black, new XPoint(100, 215));
            gfx.DrawString("Tip sobe", fontTableHeader, XBrushes.Black, new XPoint(220, 215));
            gfx.DrawString("Zauzetost", fontTableHeader, XBrushes.Black, new XPoint(350, 215));
            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(90, 222), new XPoint(510, 222));

            // Table body

            int currYposition_values = 240;
            int currYposition_line = 238;

            foreach (RoomOccupancy roomOccupancy in RoomOccupancies)
            {
                gfx.DrawString(roomOccupancy.Room.RoomNumber.ToString(), fontTableBody, XBrushes.Black, new XPoint(100, currYposition_values));
                gfx.DrawString(ConvertEnumToString(roomOccupancy.Room.pRoomType), fontTableBody, XBrushes.Black, new XPoint(220, currYposition_values));
                gfx.DrawString(roomOccupancy.ToString(), fontTableBody, XBrushes.Black, new XPoint(350, currYposition_values));
                //gfx.DrawLine(new XPen(XColor.FromArgb(205, 222, 255)), new XPoint(90, currYposition_line), new XPoint(510, currYposition_line));

                currYposition_values += 28;
                currYposition_line += 28;
            }
            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(90, currYposition_line - 18), new XPoint(510, currYposition_line - 18));
            //gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(90, 200), new XPoint(90, currYposition_line - 20));
            //gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(510, 200), new XPoint(510, currYposition_line - 20));



            try
            {
                document.Save(@".\..\..\..\Resources\pdf_files\Izveštaj_o_zauzetosti_prostorija.pdf");
                PDFSavedDialog pdfSavedDialog = new PDFSavedDialog();
                pdfSavedDialog.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Morate prvo zatvoriti otvoreni PDF fajl");
            }
        }

        private string ConvertEnumToString(RoomType roomType)
        {
            return roomType switch
            {
                RoomType.examRoom => "Soba za preglede",
                RoomType.operatingRoom => "Operaciona sala",
                RoomType.meetingRoom => "Soba za sastanke",
                RoomType.recoveryRoom => "Soba za oporavak",
                _ => "",
            };
        }

        public class RoomOccupancy
        {
            public Room Room { get; set; }
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public int MaxHours { get; set; }

            public override string ToString()
            {
                if (Hours == 0)
                    return Minutes + "min od " + MaxHours + "h";
                return Hours + "h i " + Minutes + "min od " + MaxHours + "h";
            }

        }
    }
}
