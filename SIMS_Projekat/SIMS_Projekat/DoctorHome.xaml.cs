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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for DoctorHome.xaml
    /// </summary>
    public partial class DoctorHome : Window
    {
        public List<ScheduledOperation> operations;
        public static ObservableCollection<ScheduledOperation> Operations { get; set; }
        public DoctorHome()
        {
            InitializeComponent();
            this.DataContext = this;
            Operations = App.scheduledOperationRepository.GetScheduledOperations();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.scheduledOperationRepository.Serialize();
        }

        private void Otkazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            ScheduledOperation s = (ScheduledOperation)OperationsList.SelectedItem;
            App.ScheduledOperationController.CancelOperation(s);
        }

        private void Izmenite_Termin_Click(object sender, RoutedEventArgs e)
        {
            ScheduledOperation s = (ScheduledOperation)OperationsList.SelectedItem;
            EditScheduledOperation editScheduledOperation = new EditScheduledOperation(s);
           // this.Close();
            editScheduledOperation.Show();
        }

        private void Zakazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            AddScheduledOperation addScheduledOperation = new AddScheduledOperation();
            //this.Close();
            addScheduledOperation.Show();
        }
    }
}
