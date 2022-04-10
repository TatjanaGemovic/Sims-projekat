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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for AddScheduledOperation.xaml
    /// </summary>
    public partial class AddScheduledOperation : Window
    {
        public AddScheduledOperation()
        {
            InitializeComponent();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.scheduledOperationRepository.Serialize();
        }

        private void Dodaj_operaciju_Click(object sender, RoutedEventArgs e)
        {
            ScheduledOperation s = new ScheduledOperation();
            s.Start = DateTime.Parse(Vreme_pocetka.Text);
            s.End = DateTime.Parse(Vreme_zavrsetka.Text);
            s.OperationType = Tip_operacije.Text;
            s.OperationID = int.Parse(ID_operacije.Text);
            App.ScheduledOperationController.ScheduleOperation(s);
            App.scheduledOperationRepository.Serialize();
            this.Close();
            //MainWindow main = new MainWindow();
            //main.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
