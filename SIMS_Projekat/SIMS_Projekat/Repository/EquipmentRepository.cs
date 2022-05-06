using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class EquipmentRepository
    {

        public static List<Equipment> equipment;
        private Serializer<Equipment> serializer;
        private string file;

        public List<Equipment> GetEquipment()
        {

            return equipment;
        }

        public Model.Equipment GetEquipmentByID(string equipmentID)
        {
            foreach (Equipment _equipment in equipment)
            {
                if (_equipment.EquipmentID.Equals(equipmentID))
                {
                    return _equipment;
                }
            }

            return null;
        }

        public Model.Equipment DeleteEquipment(Equipment _equipment)
        {
            equipment.Remove(_equipment);
            Serialize();
            return _equipment;
        }

        public Model.Equipment EditEquipment(Equipment oldEquipment, Equipment newEquipment)
        {
            oldEquipment.EquipmentName = newEquipment.EquipmentName;
            oldEquipment.Quantity = newEquipment.Quantity;
            oldEquipment.pEquipmentType = newEquipment.pEquipmentType;
            Serialize();
            Deserialize();
            return newEquipment;
        
        }

        public Model.Equipment AddEquipment(Model.Equipment newEquipment)
        {
            equipment.Add(newEquipment);
            Serialize();
            return newEquipment;
        }


        public List<Equipment> Equipment
        {
            get 
            {
                if (equipment == null)
                    equipment = new List<Equipment>();
                return equipment;
            }
            set 
            {
                RemoveAllEquipment();
                if (value != null)
                {
                    foreach (Model.Equipment oEquipment in value)
                        AddEquipment(oEquipment);
                }
            }
        }

        public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
        }

        public EquipmentRepository(string fileName)
        {
            equipment = new List<Equipment>();
            serializer = new Serializer<Equipment>();
            file = fileName;

        }

        public void Serialize()
        {
            serializer.toCSV(file, equipment);
        }

        public void Deserialize()
        {
            equipment = serializer.fromCSV(file);
        }


    }
}
