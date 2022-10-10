using System;
using System.Windows;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using SIMS_Projekat.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIMS_Projekat.Properties;

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerCreateReport.xaml
    /// </summary>
    public partial class ManagerCreateReport : Page
    {
        private List<Evaluation> polls;
        private double totalDoctor;
        private double totalHospital;
        private int suma;
        public ManagerCreateReport()
        {
            InitializeComponent();
            this.DataContext = this;
            odTxt.SelectedDate = DateTime.Now;
            doTxt.SelectedDate = DateTime.Now;
            totalDoctor = 0;
            totalHospital = 0;
            suma = 0;
        }

        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {
            polls = App.evaluationController.GetPollsForInterval((DateTime)odTxt.SelectedDate, (DateTime)doTxt.SelectedDate);
            suma = polls.Count;
            if (Settings.Default.CurrentLanguage == "sr-LATN")
                GenerateDocument();
            else
                GenerateDocumentEn();


            if (Settings.Default.CurrentLanguage == "sr-LATN")
                AutoClosingMessageBox.Show("Uspešno ste kreirali izveštaj.", "Kreiranje izveštaja anketa", 1500);
            else
                AutoClosingMessageBox.Show("You have successfully created report.", "Create polls report", 1500);
            ManagerHome.mainFrame.Content = new PollsView();

        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }



        private void GenerateDocument()
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfDocument doc = new PdfDocument();
                doc.DocumentInformation.Title = "Izveštaj anketa";
                //Add a page to the document
                PdfPage page = doc.Pages.Add();


                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Load the image from the disk.
                PdfImage image = PdfImage.FromFile(@".\..\..\..\ManagerPDF\logo250.png");
                //Draw the image
                graphics.DrawImage(image, new RectangleF(320, 0, 100, 100));



                //Draw the text
                string naslov = "Izvestaj anketa u periodu " + "od: " + odTxt.SelectedDate.ToString().Split(" ")[0]
                    + " do: " + doTxt.SelectedDate.ToString().Split(" ")[0];
                graphics.DrawString(naslov, font, PdfBrushes.Black, new RectangleF(30, 20, 300, 300));


                graphics.DrawString("Zdravo Bolnica", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 20, 200, 200));
                graphics.DrawString("Zlatne Grede 4", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 35, 200, 200));
                graphics.DrawString("Novi Sad", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 50, 200, 200));
                graphics.DrawString("Tel:+021/462019", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 65, 200, 200));


                graphics.DrawString("Datum", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(20, 120, 300, 300));
                graphics.DrawString("Ocena doktora", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(100, 120, 300, 300));
                graphics.DrawString("Ocena bolnice", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(200, 120, 300, 300));
                graphics.DrawString("Komentar", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(300, 120, 300, 300));
                graphics.DrawString("______________________________________________________________________________", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 126, 500, 500));

                var y = 0;

                foreach (Evaluation e in polls)
                {
                    graphics.DrawString(e.evaluationCreated.ToString().Split(" ")[0], new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 150+y, 300, 300));
                    graphics.DrawString(e.averageDoctorRating.ToString(), new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(100, 150+y, 300, 300));
                    graphics.DrawString(e.averageHospitalRating.ToString(), new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(200, 150+y, 300, 300));
                    graphics.DrawString(e.comment, new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(300, 150+y, 300, 300));
                    y += 20;

                    totalDoctor += e.averageDoctorRating;
                    totalHospital += e.averageHospitalRating;

                }

                graphics.DrawString("______________________________________________________________________________", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 150+y, 500, 500));


                var ukupnoAnketa = "Ukupan broj anketa:     " + suma.ToString();
                var ukupnoDoktor = "Prosecna ocena osoblja: " + (Math.Round((totalDoctor / suma), 2, MidpointRounding.ToEven)).ToString();
                var ukupnoBolnica= "Prosecna ocena bolnice: " + (Math.Round((totalHospital / suma), 2, MidpointRounding.ToEven)).ToString();

                graphics.DrawString(ukupnoAnketa, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 175+y, 300, 300));
                graphics.DrawString(ukupnoDoktor, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 195+y, 300, 300));
                graphics.DrawString(ukupnoBolnica, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 215 + y, 300, 300));


                Random rand = new Random();
                int number = rand.Next(0, 100000000);
                var naziv_random = @".\..\..\..\ManagerPDF\Upravnik" + number.ToString()+".pdf";
                //Save the document
                doc.Save(naziv_random);
                doc.Close(true);
                //System.Diagnostics.Process.Start("Output.pdf");
            }



        }

        private void GenerateDocumentEn()
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfDocument doc = new PdfDocument();
                doc.DocumentInformation.Title = "Polls report";
                //Add a page to the document
                PdfPage page = doc.Pages.Add();


                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Load the image from the disk.
                PdfImage image = PdfImage.FromFile(@".\..\..\..\ManagerPDF\logo250.png");
                //Draw the image
                graphics.DrawImage(image, new RectangleF(320, 0, 100, 100));



                //Draw the text
                string naslov = "Polls report in interval " + "from: " + odTxt.SelectedDate.ToString().Split(" ")[0]
                    + " to: " + doTxt.SelectedDate.ToString().Split(" ")[0];
                graphics.DrawString(naslov, font, PdfBrushes.Black, new RectangleF(30, 20, 300, 300));


                graphics.DrawString("Zdravo Hospital", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 20, 200, 200));
                graphics.DrawString("Zlatne Grede 4", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 35, 200, 200));
                graphics.DrawString("Novi Sad", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 50, 200, 200));
                graphics.DrawString("Tel:+021/462019", new PdfStandardFont(PdfFontFamily.TimesRoman, 11), PdfBrushes.Black, new RectangleF(430, 65, 200, 200));


                graphics.DrawString("Date", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(20, 120, 300, 300));
                graphics.DrawString("Doctor rank", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(100, 120, 300, 300));
                graphics.DrawString("Hospital rank", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(200, 120, 300, 300));
                graphics.DrawString("Comment", new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(300, 120, 300, 300));
                graphics.DrawString("______________________________________________________________________________", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 126, 500, 500));

                var y = 0;

                foreach (Evaluation e in polls)
                {
                    graphics.DrawString(e.evaluationCreated.ToString().Split(" ")[0], new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 150 + y, 300, 300));
                    graphics.DrawString(e.averageDoctorRating.ToString(), new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(100, 150 + y, 300, 300));
                    graphics.DrawString(e.averageHospitalRating.ToString(), new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(200, 150 + y, 300, 300));
                    graphics.DrawString(e.comment, new PdfStandardFont(PdfFontFamily.TimesRoman, 14), PdfBrushes.Black, new RectangleF(300, 150 + y, 300, 300));
                    y += 20;

                    totalDoctor += e.averageDoctorRating;
                    totalHospital += e.averageHospitalRating;

                }

                graphics.DrawString("______________________________________________________________________________", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), PdfBrushes.Black, new RectangleF(20, 150 + y, 500, 500));


                var ukupnoAnketa = "Total number of polls:     " + suma.ToString();
                var ukupnoDoktor = "Average rank of doctors: " + (Math.Round((totalDoctor / suma), 2, MidpointRounding.ToEven)).ToString();
                var ukupnoBolnica = "Average rank of hospital: " + (Math.Round((totalHospital / suma), 2, MidpointRounding.ToEven)).ToString();

                graphics.DrawString(ukupnoAnketa, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 175 + y, 300, 300));
                graphics.DrawString(ukupnoDoktor, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 195 + y, 300, 300));
                graphics.DrawString(ukupnoBolnica, new PdfStandardFont(PdfFontFamily.TimesRoman, 16), PdfBrushes.Black, new RectangleF(300, 215 + y, 300, 300));


                Random rand = new Random();
                int number = rand.Next(0, 100000000);
                var naziv_random = @".\..\..\..\ManagerPDF\Upravnik" + number.ToString() + ".pdf";
                //Save the document
                doc.Save(naziv_random);
                doc.Close(true);
                //System.Diagnostics.Process.Start("Output.pdf");
            }



        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new PollsView();
        }
    }
}
