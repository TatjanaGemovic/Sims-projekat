using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
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

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for HCIReportPage.xaml
    /// </summary>
    public partial class HCIReportPage : Page
    {
        private Patient patient;
        ObservableCollection<Receipt> weekTherapy = new ObservableCollection<Receipt>();
        ObservableCollection<TherapyViewModel> firstDay = new ObservableCollection<TherapyViewModel>();
        ObservableCollection<TherapyViewModel> secondDay = new ObservableCollection<TherapyViewModel>();
        ObservableCollection<TherapyViewModel> thirdDay = new ObservableCollection<TherapyViewModel>();
        ObservableCollection<TherapyViewModel> fourthDay = new ObservableCollection<TherapyViewModel>();
        ObservableCollection<TherapyViewModel> fifthDay = new ObservableCollection<TherapyViewModel>();
        ObservableCollection<TherapyViewModel> sixthDay = new ObservableCollection<TherapyViewModel>(); 
        ObservableCollection<TherapyViewModel> seventhDay = new ObservableCollection<TherapyViewModel>();

        Injector Inject;
        public HCIReportPage(Frame frame, Patient p)
        {
            Inject = new Injector();
            InitializeComponent();
            patient = p;
            FillLabels();

            FillGrids(DateTime.Now);
            this.DataContext = this;
            dayOneGrid.ItemsSource = firstDay;
            dayTwoGrid.ItemsSource = secondDay;
            dayThreeGrid.ItemsSource = thirdDay;
            dayFourGrid.ItemsSource = fourthDay;
            dayFiveGrid.ItemsSource = fifthDay;
            daySixGrid.ItemsSource = sixthDay;
            daySevenGrid.ItemsSource = seventhDay;

            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                    printDialog.PrintVisual(print, "report");



                MessageBox.Show("Izvjestaj uspjesno generisan!");
            }
            finally
            {
            }
            
        }
        private void FillGrids(DateTime firstDayOfWeek)
        {
            foreach (Receipt r in weekTherapy)
            {
                TherapyViewModel therapy = Inject.TherapyConverter.ConvertModelToViewModel(r);
                if (r.beginningDate.Date <= firstDayOfWeek.Date && r.endDate.Date >= firstDayOfWeek.Date)
                {
                    firstDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(1).Date && r.endDate.Date >= firstDayOfWeek.AddDays(1).Date)
                {
                    secondDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(2).Date && r.endDate.Date >= firstDayOfWeek.AddDays(2).Date)
                {
                    thirdDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(3).Date && r.endDate.Date >= firstDayOfWeek.AddDays(3).Date)
                {
                    fourthDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(4).Date && r.endDate.Date >= firstDayOfWeek.AddDays(4).Date)
                {
                    fifthDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(5).Date && r.endDate >= firstDayOfWeek.AddDays(5).Date)
                {
                    sixthDay.Add(therapy);
                }

                if (r.beginningDate.Date <= firstDayOfWeek.AddDays(6).Date && r.endDate.Date >= firstDayOfWeek.AddDays(6).Date)
                {
                    seventhDay.Add(therapy);
                }
            }
        }
        private void FillLabels()
        {   
            DateTime today = DateTime.Now;
            DateTime endOfWeek = DateTime.Now.AddDays(6);
            patientLabel.Content = patient.FirstName + " " + patient.LastName;
            periodLabel.Content = today.Day + "." + today.Month + "." + today.Year + ". " + " - " + endOfWeek.Day + "." + endOfWeek.Month + "." + endOfWeek.Year + ".";

            GetTherapiesForThisWeek(today, endOfWeek);
            SetDayRow();
        }
        private void GetTherapiesForThisWeek(DateTime start, DateTime end)
        {
            List<Receipt> receipts = App.receiptController.GetReceiptByPatientID(patient.ID);
            int count = 0;
            foreach(Receipt r in receipts)
            {
                if(r.beginningDate <= end && r.endDate >= start)
                {
                    weekTherapy.Add(r);
                    count ++;
                }
            }
            if(count == 0)
                numberLabel.Content = "Nema terapija za ovu nedelju!";
            else
                numberLabel.Content = count;
        }
        private void SetDayRow()
        {
            DateTime today = DateTime.Now;
            if(today.DayOfWeek == DayOfWeek.Monday)
            {
                dayOne.Content = "Ponedeljak";
                dayTwo.Content = "Utorak";
                dayThree.Content = "Sreda";
                dayFour.Content = "Četvrtak";
                dayFive.Content = "Petak";
                daySix.Content = "Subota";
                daySeven.Content = "Nedelja";
            }
            else if(today.DayOfWeek == DayOfWeek.Tuesday)
            {
                dayOne.Content = "Utorak";
                dayTwo.Content = "Sreda";
                dayThree.Content = "Četvrtak";
                dayFour.Content = "Petak";
                dayFive.Content = "Subota";
                daySix.Content = "Nedelja";
                daySeven.Content = "Ponedeljak";
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                dayOne.Content = "Sreda";
                dayTwo.Content = "Četvrtak";
                dayThree.Content = "Petak";
                dayFour.Content = "Subota";
                dayFive.Content = "Nedelja";
                daySix.Content = "Ponedeljak";
                daySeven.Content = "Utorak";
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                dayOne.Content = "Četvrtak";
                dayTwo.Content = "Petak";
                dayThree.Content = "Subota";
                dayFour.Content = "Nedelja";
                dayFive.Content = "Ponedeljak";
                daySix.Content = "Utorak";
                daySeven.Content = "Sreda";
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                dayOne.Content = "Petak";
                dayTwo.Content = "Subota";
                dayThree.Content = "Nedelja";
                dayFour.Content = "Ponedeljak";
                dayFive.Content = "Utorak";
                daySix.Content = "Sreda";
                daySeven.Content = "Četvrtak";
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                dayOne.Content = "Subota";
                dayTwo.Content = "Nedelja";
                dayThree.Content = "Ponedeljak";
                dayFour.Content = "Utorak";
                dayFive.Content = "Sreda";
                daySix.Content = "Četvrtak";
                daySeven.Content = "Petak";
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                dayOne.Content = "Nedelja";
                dayTwo.Content = "Ponedeljak";
                dayThree.Content = "Utorak";
                dayFour.Content = "Sreda";
                dayFive.Content = "Četvrtak";
                daySix.Content = "Petak";
                daySeven.Content = "Subota";
            }
        }
    }
}
