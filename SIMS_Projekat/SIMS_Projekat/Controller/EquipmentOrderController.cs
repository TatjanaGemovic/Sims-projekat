using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class EquipmentOrderController
    {
        public EquipmentOrderService EquipmentOrderService { get; set; }

        public EquipmentOrder AddEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            return EquipmentOrderService.AddEquipmentOrder(equipmentOrder);
        }

        public EquipmentOrder DeleteEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            return EquipmentOrderService.DeleteEquipmentOrder(equipmentOrder);
        }

        public List<EquipmentOrder> GetAllEquipmentOrders()
        {
            return EquipmentOrderService.GetAllEquipmentOrders();
        }
        public void Serialize()
        {
            EquipmentOrderService.Serialize();
        }

        public void Deserialize()
        {
            EquipmentOrderService.Deserialize();
        }
    }
}
