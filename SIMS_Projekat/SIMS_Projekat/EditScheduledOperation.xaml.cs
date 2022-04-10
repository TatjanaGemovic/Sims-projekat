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
    /// Interaction logic for EditScheduledOperation.xaml
    /// </summary>
    public partial class EditScheduledOperation : Window
    {
        public EditScheduledOperation(Model.ScheduledOperation s)
        {
            InitializeComponent();
            Vreme_pocetka.Text = s.Start.ToString();
            Vreme_zavrsetka.Text = s.End.ToString();
            Tip_operacije.Text = s.OperationType;
            ID_operacije.Text = s.OperationID.ToString();
            
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.scheduledOperationRepository.Serialize();
        }

        private void Promeni_Click(object sender, RoutedEventArgs e)
        {
            ScheduledOperation s = new ScheduledOperation();
            s.Start = DateTime.Parse(Vreme_pocetka.Text);
            s.End = DateTime.Parse(Vreme_zavrsetka.Text);
            s.OperationType = Tip_operacije.Text;
            //s.OperationID = int.Parse(ID_operacije.Text);
            App.ScheduledOperationController.Edit(s);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
