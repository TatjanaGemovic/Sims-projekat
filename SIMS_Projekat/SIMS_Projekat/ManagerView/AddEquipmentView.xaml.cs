using SIMS_Projekat.Controller;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private EquipmentController equipmentController;
        public AddEquipmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            equipmentController = App.equipmentController;

        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            equipmentController.AddEquipment(getEquipmentFromForm());
            ManagerHome.mainFrame.Content = new EquipmentView();
        }

        private Equipment getEquipmentFromForm()
        {
            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = Guid.NewGuid().ToString();
            newEquipment.EquipmentName = equipmentName.Text;
            newEquipment.Quantity = int.Parse(equipmentQuantity.Text);
            newEquipment.pEquipmentType = (EquipmentType)equipmentType.SelectedIndex;
            return newEquipment;
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new EquipmentView();
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
