using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class MedicineService
    {
        private readonly MedicineRepository _medicineRepository;

        public MedicineService(MedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public List<Medicine> GetMedicine()
        {
            return _medicineRepository.GetMedicine();
        }

        public Medicine GetMedicineByID(string medicineID)
        {
            return _medicineRepository.GetMedicineByID(medicineID);
        }

        public List<Medicine> GetVerifyMedicine()
        {
            return _medicineRepository.GetVerifyMedicine();
        }

        public List<Medicine> GetRejectMedicine()
        {
            return _medicineRepository.GetRejectMedicine();
        }

        public List<Medicine> GetSendToDoctorMedicine(string doctor)
        {
            return _medicineRepository.GetSendToDoctorMedicine(doctor);
        }

        public Medicine DeleteMedicine(Medicine _medicine)
        {
            return _medicineRepository.DeleteMedicine(_medicine);
        }

        public Medicine EditMedicine(Medicine oldMedicine, Medicine newMedicine)
        {
            foreach (Medicine oMedicine in GetMedicine())
            {
                if (newMedicine.MedicineName.Equals(oMedicine.MedicineName) && !newMedicine.MedicineID.Equals(oMedicine.MedicineID) )
                    return null;

            }
            return _medicineRepository.EditMedicine(oldMedicine, newMedicine);

        }

        public Model.Medicine AddMedicine(Medicine newMedicine)
        {
            foreach (Medicine oMedicine in GetMedicine())
            {
                if (newMedicine.MedicineName.Equals(oMedicine.MedicineName))
                    return null;
            
            }

            return _medicineRepository.AddMedicine(newMedicine);
        }



        public void Serialize()
        {
            _medicineRepository.Serialize();
        }

        public void Deserialize()
        {
            _medicineRepository.Deserialize();
        }



    }
}
