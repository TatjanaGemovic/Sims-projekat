using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class EquipmentController
    {
        public EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        public List<Equipment> GetEquipment()
        {
            return this._equipmentService.GetEquipment();
        }

        public Model.Equipment GetEquipmentByID(string equipmentID)
        {
            return this._equipmentService.GetEquipmentByID(equipmentID);
        }

        public Model.Equipment AddEquipment(Model.Equipment newEquipment)
        {
            return this._equipmentService.AddEquipment(newEquipment);
        }

        public Model.Equipment EditEquipment(string oldEquipmentID, Model.Equipment newEquipment)
        {
            return this._equipmentService.EditEquipment(oldEquipmentID, newEquipment);
        }

        public Model.Equipment DeleteEquipment(string equipmentID)
        {
            return this._equipmentService.DeleteEquipment(equipmentID);
        }

        public void Serialize()
        {
            this._equipmentService.Serialize();
        }

        public void Deserialize()
        {
            this._equipmentService.Deserialize();
        }
    }
}
