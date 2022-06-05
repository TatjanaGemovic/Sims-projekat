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
            filterCombo.SelectedIndex = 2;
        }

        private void DodajOpremuBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddEquipmentView();
        }

        private void ObrisiOpremeBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            App.equipmentController.DeleteEquipment(selectedEquipment.EquipmentID);

        }

        private void IzmenaOpremeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            ManagerHome.mainFrame.Content = new EditEquipmentView(selectedEquipment);
        }

        private void OpremaPremestanjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            ManagerHome.mainFrame.Content = new ExchangeRoomFromMagacin(selectedEquipment, null, 0);
        }


        private void PregledPoProstorijamaBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = (Equipment)datagGridEquipment.SelectedItem;
            ManagerHome.mainFrame.Content = new ProbaPregled(selectedEquipment);
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filterCombo.SelectedIndex == -1 || filterCombo.SelectedIndex == 2)
            {
                if (searchBox.Text != "")
                {
                    datagGridEquipment.ItemsSource = App.equipmentController.GetEquipment().Where(x => x.EquipmentName.ToLower().Contains(searchBox.Text.ToLower()));
                }
                else
                    datagGridEquipment.ItemsSource = EquipmentList;
            }
            else
            {

                if (searchBox.Text != "")
                {

                    datagGridEquipment.ItemsSource = App.equipmentController.GetEquipment().Where(x => x.EquipmentName.ToLower().Contains(searchBox.Text.ToLower()) && (int)x.pEquipmentType == filterCombo.SelectedIndex);
                }
                else
                {
                    datagGridEquipment.ItemsSource = App.equipmentController.GetEquipment().Where(x => (int)x.pEquipmentType == filterCombo.SelectedIndex);
                }

            }
        }

        private void filterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchBox.Clear();
            if (filterCombo.SelectedIndex != -1 && filterCombo.SelectedIndex != 2)
            {
                datagGridEquipment.ItemsSource = App.equipmentController.GetEquipment().Where(x => (int)x.pEquipmentType == filterCombo.SelectedIndex);
            }
            else
                datagGridEquipment.ItemsSource = EquipmentList;
        }

        private void datagGridEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagGridEquipment.SelectedItem != null)
            {
                PregledPoProstorijamaBtn.IsEnabled = true;
                IzmenaOpremuBtn.IsEnabled = true;
                ObrisiOpremuBtn.IsEnabled = true;
                OpremaPremestanjeBtn.IsEnabled = true;
            }

        }
    }
}
