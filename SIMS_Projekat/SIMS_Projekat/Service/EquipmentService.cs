using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class EquipmentService
    {
        
        private readonly EquipmentRepository _equipmentRepository;
        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public void Deserialize()
        {
            _equipmentRepository.Deserialize();
        }
        public List<Equipment> GetEquipment()
        {
            return this._equipmentRepository.GetEquipment();
        }

        public Model.Equipment GetEquipmentByID(string equipmentID)
        {
            return this._equipmentRepository.GetEquipmentByID(equipmentID);
        }

        public Model.Equipment AddEquipment(Model.Equipment newEquipment)
        {
            if(GetEquipmentByID(newEquipment.EquipmentID) == null )
                return this._equipmentRepository.AddEquipment(newEquipment);

            return null;
        }

        public Model.Equipment DeleteEquipment(string equipmentID)
        {
            var equipmentForDelete = GetEquipmentByID(equipmentID);
            if (equipmentForDelete != null)
                return this._equipmentRepository.DeleteEquipment(equipmentForDelete);

            return null;
        }

        public Model.Equipment EditEquipment(string equipmentID, Model.Equipment newEquipment)
        {
            var equipmentForEdit = GetEquipmentByID(equipmentID);
            if (equipmentForEdit != null)
                return this._equipmentRepository.EditEquipment(equipmentForEdit, newEquipment);

            return null;
        }


        public void Serialize()
        {
            _equipmentRepository.Serialize();
        }


    }
}
