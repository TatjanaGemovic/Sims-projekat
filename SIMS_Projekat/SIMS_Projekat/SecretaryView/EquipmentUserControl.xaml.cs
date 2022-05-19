using SIMS_Projekat.Controller;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class EquipmentUserControl : UserControl
    {
    
        private ContentControl contentControl;
        private EquipmentController equipmentController;
        private EquipmentOrderController equipmentOrderController;
        private OrderEquipmentUserControl orderEquipmentUserControl;
        private EquipmentOrderListUserControl equipmentOrderListUserControl;

        public ObservableCollection<Equipment> Equipment { get; set; }

        public EquipmentUserControl(EquipmentController equipmentController, 
            EquipmentOrderController equipmentOrderController, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataContext = this;
            this.equipmentController = equipmentController;
            this.contentControl = contentControl;
            this.equipmentOrderController = equipmentOrderController;

            Equipment = new ObservableCollection<Equipment>
                (this.equipmentController.GetEquipment().Where(x => x.pEquipmentType == EquipmentType.dynamicc));

            orderEquipmentUserControl = new OrderEquipmentUserControl(equipmentController, equipmentOrderController, 
                contentControl, this);
            equipmentOrderListUserControl = new EquipmentOrderListUserControl(equipmentOrderController, 
                contentControl, this);
            HasEquipmentArrived();
        }


        private void HasEquipmentArrived()
        {
            List<EquipmentOrder> equipmentOrders = new List<EquipmentOrder>(equipmentOrderController.GetAllEquipmentOrders());
            DateTime todayDate = DateTime.Now;

            foreach (EquipmentOrder equipmentOrder in equipmentOrders)
            {
                if(todayDate > equipmentOrder.ArrivalDate)
                {
                    ReceiveOrder(equipmentOrder.Equipment, equipmentOrder.Quantity);
                    equipmentOrderController.DeleteEquipmentOrder(equipmentOrder);
                }
            }
        }

        private void ReceiveOrder(Equipment receivedEquipment, int quantity)
        {
            foreach(Equipment equipment in Equipment)
            {
                if(equipment.EquipmentID == receivedEquipment.EquipmentID)
                {
                    equipment.Quantity += quantity;
                }
            }
        }

        private void OrderEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            orderEquipmentUserControl = new OrderEquipmentUserControl(equipmentController, equipmentOrderController, contentControl, this);
            contentControl.Content = orderEquipmentUserControl;
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            equipmentOrderListUserControl = new EquipmentOrderListUserControl(equipmentOrderController, contentControl, this);
            contentControl.Content = equipmentOrderListUserControl;
        }
    }
}
