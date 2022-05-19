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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddUrgentPatientUserControl .xaml
    /// </summary>
    public partial class OrderEquipmentUserControl : UserControl
    {
        public EquipmentOrderController EquipmentOrderController { get; set; }
        public EquipmentController EquipmentController { get; set; }

        private ContentControl contentControl;
        private UserControl equipmentUserControl;

        public ObservableCollection<Equipment> Equipment { get; set; }



        public OrderEquipmentUserControl(EquipmentController equipmentController, EquipmentOrderController equipmentOrderController, 
            ContentControl contentControl, UserControl userControl)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            equipmentUserControl = userControl;
            EquipmentOrderController = equipmentOrderController;
            EquipmentController = equipmentController;

            Equipment = new ObservableCollection<Equipment>
                (EquipmentController.GetEquipment().Where(x => x.pEquipmentType == EquipmentType.dynamicc));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newEquipmentOrder = new EquipmentOrder()
            {
                Equipment = (Equipment)EquipmentListBox.SelectedItem,
                ArrivalDate = (DateTime)DatePicker.SelectedDate,
                Quantity = int.Parse(EquipmentQuantity.Text)
            };
            EquipmentOrderController.AddEquipmentOrder(newEquipmentOrder);
            contentControl.Content = equipmentUserControl;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = equipmentUserControl;
        }


    }
}
