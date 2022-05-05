using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for EquipmentView.xaml
    /// </summary>
    public partial class EquipmentView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<Equipment> equipment;

        public ObservableCollection<Equipment> EquipmentList
        {
            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged(nameof(EquipmentList));
            }
        }

        public EquipmentView()
        {


            InitializeComponent();
            this.DataContext = this;
            EquipmentList = new ObservableCollection<Equipment>(App.equipmentController.GetEquipment());


        }

        private void DodajOpremuBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddEquipmentView();
        }

        private void ObrisiOpremeBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            if (selectedEquipment != null)
            {
                App.equipmentController.DeleteEquipment(selectedEquipment.EquipmentID);

            }
            else
            {

            }
        }

        private void IzmenaOpremeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            ManagerHome.mainFrame.Content = new EditEquipmentView(selectedEquipment);
        }

        private void OpremaPremestanjeBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void PregledPoProstorijamaBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
