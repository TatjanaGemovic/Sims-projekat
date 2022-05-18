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
    public class MedicineReplacmentDTORepository
    {
        public static List<ReplacmentMedicineDTO> allDTO;
        private Serializer<ReplacmentMedicineDTO> serializer;
        private string file;
        private readonly MedicineRepository _medicineRepository;

        public List<ReplacmentMedicineDTO> GetAllDTO()
        {
            return allDTO;
        }

        public DTO.ReplacmentMedicineDTO GetDTOByID(string dtoID)
        {
            foreach (DTO.ReplacmentMedicineDTO _dto in allDTO)
            {
                if (_dto.dtoID.Equals(dtoID))
                {
                    return _dto;
                }
            }

            return null;
        }

        public List<Medicine> GetReplacmentMedicineByMainMedicineID(string mainMedicineID)
        {
            List<Medicine> replacmentMedicine = new List<Medicine>();
            foreach (DTO.ReplacmentMedicineDTO _dto in allDTO)
            {
                if (_dto.mainMedicineID.Equals(mainMedicineID))
                {
                    replacmentMedicine.Add(_medicineRepository.GetMedicineByID(_dto.replacmentMedicineID));
                }
            }

            return replacmentMedicine;
        }


        public ReplacmentMedicineDTO DeleteDTO(ReplacmentMedicineDTO _medicineComponentDTO)
        {
            allDTO.Remove(_medicineComponentDTO);
            Serialize();
            return _medicineComponentDTO;
        }


        public ReplacmentMedicineDTO AddDTO(ReplacmentMedicineDTO _medicineComponentDTO)
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

        public MedicineReplacmentDTORepository(string fileName, MedicineRepository medicineRepository)
        {
            allDTO = new List<ReplacmentMedicineDTO>();
            serializer = new Serializer<ReplacmentMedicineDTO>();
            file = fileName;
            _medicineRepository = medicineRepository;

        }

        public void Serialize()
        {
            serializer.toCSV(file, allDTO);
        }

        public void Deserialize()
        {
            allDTO = serializer.fromCSV(file);
            foreach (Medicine medicine in _medicineRepository.GetMedicine())
            {
                medicine.ReplacmentMedicine = GetReplacmentMedicineByMainMedicineID(medicine.MedicineID);
            }
        }
    }
}
