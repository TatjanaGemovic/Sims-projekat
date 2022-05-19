using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class MedicineComponentDTORepository
    {
        public static List<MedicineComponentDTO> allDTO;
        private Serializer<MedicineComponentDTO> serializer;
        private string file;
        public static List<Medicine> _medicine;

        public List<MedicineComponentDTO> GetAllDTO()
        {
            return allDTO;
        }

        public DTO.MedicineComponentDTO GetDTOByID(string dtoID)
        {
            foreach (DTO.MedicineComponentDTO _dto in allDTO)
            {
                if (_dto.dtoID.Equals(dtoID))
                {
                    return _dto;
                }
            }

            return null;
        }

        public DTO.MedicineComponentDTO GetDTOByMedicineAndComponent(string medicineID, string component)
        {
            foreach (DTO.MedicineComponentDTO _dto in allDTO)
            {
                if (_dto.mainMedicineID.Equals(medicineID) && _dto.component.Equals(component))
                {
                    return _dto;
                }
            }

            return null;
        }

        public List<string> GetComponentsByMedicineID(string medicineID)
        {
            List<string> components = new List<string>();
            foreach (DTO.MedicineComponentDTO _dto in allDTO)
            {
                if (_dto.mainMedicineID.Equals(medicineID))
                {
                    components.Add(_dto.component);
                }
            }

            return components;
        }


        public MedicineComponentDTO DeleteDTO(MedicineComponentDTO _medicineComponentDTO)
        {
            allDTO.Remove(_medicineComponentDTO);
            Serialize();
            return _medicineComponentDTO;
        }


        public MedicineComponentDTO AddDTO(MedicineComponentDTO _medicineComponentDTO)
        {
            allDTO.Add(_medicineComponentDTO);
            Serialize();
            return _medicineComponentDTO;
        }


        public void RemoveAllMedicineComponent()
        {
            if (allDTO != null)
                allDTO.Clear();
        }

        public MedicineComponentDTORepository(string fileName, List<Medicine> medicine)
        {
            allDTO = new List<MedicineComponentDTO>();
            serializer = new Serializer<MedicineComponentDTO>();
            file = fileName;
            _medicine = medicine;

        }

        public void Serialize()
        {
            serializer.toCSV(file, allDTO);
        }

        public void Deserialize()
        {
            allDTO = serializer.fromCSV(file);
            foreach (Medicine medicine in App.medicineController.GetMedicine())
            {
                medicine.MedicineComponents = GetComponentsByMedicineID(medicine.MedicineID);
            }
        }
    }
}
