using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class MedicineController
    {
        private readonly MedicineService _medicineService;

        public MedicineController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public List<Medicine> GetMedicine()
        {
            return _medicineService.GetMedicine();
        }

        public Medicine GetMedicineByID(string medicineID)
        {
            return _medicineService.GetMedicineByID(medicineID);
        }

        public List<Medicine> GetVerifyMedicine()
        {
            return _medicineService.GetVerifyMedicine();
        }

        public List<Medicine> GetRejectMedicine()
        {
            return _medicineService.GetRejectMedicine();
        }

        public List<Medicine> GetSendToDoctorMedicine(string doctor)
        {
            return _medicineService.GetSendToDoctorMedicine(doctor);
        }

        public Medicine DeleteMedicine(Medicine _medicine)
        {
            return _medicineService.DeleteMedicine(_medicine);
        }

        public Medicine EditMedicine(Medicine oldMedicine, Medicine newMedicine)
        {
            
            return _medicineService.EditMedicine(oldMedicine, newMedicine);

        }

        public Model.Medicine AddMedicine(Medicine newMedicine)
        {
            
            return _medicineService.AddMedicine(newMedicine);
        }



        public void Serialize()
        {
            _medicineService.Serialize();
        }

        public void Deserialize()
        {
            _medicineService.Deserialize();
        }

    }
}
