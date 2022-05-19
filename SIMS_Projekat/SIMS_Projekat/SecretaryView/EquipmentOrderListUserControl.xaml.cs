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
    public partial class EquipmentOrderListUserControl : UserControl
    {
        public EquipmentOrderController EquipmentOrderController { get; set; }

        private ContentControl contentControl;
        private UserControl equipmentUserControl;

        public ObservableCollection<EquipmentOrder> EquipmentOrders { get; set; }



        public EquipmentOrderListUserControl(EquipmentOrderController equipmentOrderController, 
            ContentControl contentControl, UserControl userControl)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            equipmentUserControl = userControl;
            EquipmentOrderController = equipmentOrderController;

            EquipmentOrders = new ObservableCollection<EquipmentOrder>(EquipmentOrderController.GetAllEquipmentOrders());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = equipmentUserControl;
        }


    }
}
