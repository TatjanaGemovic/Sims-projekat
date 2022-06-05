using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class MedicineRepository
    {
        public static List<Medicine> medicine;
        private Serializer<Medicine> serializer;
        private string file;

        public List<Medicine> GetMedicine()
        {
            return medicine;
        }

        public Medicine GetMedicineByID(string medicineID)
        {
            foreach (Medicine _medicine in medicine)
            {
                if (_medicine.MedicineID.Equals(medicineID))
                {
                    return _medicine;
                }
            }

            return null;
        }

        public List<Medicine> GetVerifyMedicine()
        {
            var medicineList = new List<Medicine>();
            foreach (Medicine _medicine in medicine)
            {
                if (_medicine.Verify == true)
                {
                    medicineList.Add(_medicine);
                }
            }

            return medicineList;
        }

        public List<Medicine> GetRejectMedicine()
        {
            var medicineList = new List<Medicine>();
            foreach (Medicine _medicine in medicine)
            {
                if (_medicine.Verify == false && _medicine.SendToDoctor == true && _medicine.OnObservation == false)
                {
                    medicineList.Add(_medicine);
                }
            }

            return medicineList;
        }

        public List<Medicine> GetSendToDoctorMedicine(string doctor)
        {
            var medicineList = new List<Medicine>();
            foreach (Medicine _medicine in medicine)
            {
                if (_medicine.SendToDoctor == true && _medicine.OnObservation == true && _medicine.DoctorRevision.Equals(doctor))
                {
                    medicineList.Add(_medicine);
                }
            }

            return medicineList;
        }

        public Medicine DeleteMedicine(Medicine _medicine)
        {
            medicine.Remove(_medicine);
            Serialize();
            return _medicine;
        }

        public Medicine EditMedicine(Medicine oldMedicine, Medicine newMedicine)
        {
            changeMedicine(oldMedicine,newMedicine);
            Serialize();
            return newMedicine;

        }

        private void changeMedicine(Medicine oldMedicine, Medicine newMedicine) 
        {
            oldMedicine.MedicineName = newMedicine.MedicineName;
            oldMedicine.MedicineDose = newMedicine.MedicineDose;
            oldMedicine.pMedicineType = newMedicine.pMedicineType;
            oldMedicine.Verify = newMedicine.Verify;
            oldMedicine.SendToDoctor = newMedicine.SendToDoctor;
            oldMedicine.OnObservation = newMedicine.OnObservation;
            oldMedicine.pMedicineUseType = newMedicine.pMedicineUseType;
            oldMedicine.DoctorComment = newMedicine.DoctorComment;

        }

        public Model.Medicine AddMedicine(Medicine newMedicine)
        {
            medicine.Add(newMedicine);
            Serialize();
            return newMedicine;
        }


        public List<Medicine> Medicine
        {
            get
            {
                if (medicine == null)
                    medicine = new List<Medicine>();
                return medicine;
            }
            set
            {
                RemoveAllMedicine();
                if (value != null)
                {
                    foreach (Medicine oMedicine in value)
                        AddMedicine(oMedicine);
                }
            }
        }

        public void RemoveAllMedicine()
        {
            if (medicine != null)
                medicine.Clear();
        }

        public MedicineRepository(string fileName)
        {
            medicine = new List<Medicine>();
            serializer = new Serializer<Medicine>();
            file = fileName;

        }

        public void Serialize()
        {
            serializer.toCSV(file, medicine);
        }

        public void Deserialize()
        {
            medicine = serializer.fromCSV(file);
        }
    }
}
