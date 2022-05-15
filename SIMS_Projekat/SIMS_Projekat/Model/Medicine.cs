using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Medicine : Serialization.Serializable, INotifyPropertyChanged
    {
        private string medicineID;
        public string medicineName;
        public int medicineDose;
        public MedicineType medicineType;
        public MedicineUseType medicineUseType;
        public Boolean verify;
        public Boolean sendToDoctor;
        public Boolean onObservation;
        public string doctorComment;
        public List<Medicine> replacmentMedicine;
        public List<String> medicineComponents;
        public event PropertyChangedEventHandler PropertyChanged;

        public Medicine()
        {
            replacmentMedicine = new List<Medicine>();
            medicineComponents = new List<string>();
        }

        public void fromCSV(string[] values)
        {
            medicineID = values[0];
            medicineName = values[1];
            medicineDose = int.Parse(values[2]);
            medicineType = (MedicineType)int.Parse(values[3]);
            medicineUseType = (MedicineUseType)int.Parse(values[4]);
            if (values[5].Equals("True")) { verify = true; } else { verify = false; }
            if (values[6].Equals("True")) { sendToDoctor = true; } else { sendToDoctor = false; }
            if (values[7].Equals("True")) { onObservation = true; } else { onObservation  = false; }
            doctorComment = values[8];
        }

        public string[] toCSV()
        {
            string[] values = {
                medicineID,
                medicineName,
                medicineDose.ToString(),
                ((int)medicineType).ToString(),
                ((int)medicineUseType).ToString(),
                verify.ToString(),
                sendToDoctor.ToString(),
                onObservation.ToString(),
                doctorComment
            };

            return values;
        }

        public string MedicineID
        {

            get { return medicineID; }
            set
            {
                medicineID = value;
                OnPropertyChanged(nameof(MedicineID));
            }
        }

        public string MedicineName
        {

            get { return medicineName; }
            set
            {
                medicineName = value;
                OnPropertyChanged(nameof(MedicineName));
            }
        }
        public MedicineType pMedicineType
        {

            get { return medicineType; }
            set
            {
                medicineType = value;
                OnPropertyChanged(nameof(pMedicineType));
            }
        }

        public MedicineUseType pMedicineUseType
        {

            get { return medicineUseType; }
            set
            {
                medicineUseType = value;
                OnPropertyChanged(nameof(pMedicineUseType));
            }
        }

        public int MedicineDose
        {

            get { return medicineDose; }
            set
            {
                medicineDose = value;
                OnPropertyChanged(nameof(MedicineDose));
            }
        }

        public Boolean Verify
        {
            get { return verify; }
            set
            {
                verify = value;
                OnPropertyChanged(nameof(Verify));
            }
        }

        public Boolean SendToDoctor
        {
            get { return sendToDoctor; }
            set
            {
                sendToDoctor = value;
                OnPropertyChanged(nameof(SendToDoctor));
            }
        }

        public Boolean OnObservation
        {
            get { return onObservation; }
            set
            {
                onObservation = value;
                OnPropertyChanged(nameof(OnObservation));
            }
        }

        public string DoctorComment
        {

            get { return doctorComment; }
            set
            {
                doctorComment = value;
                OnPropertyChanged(nameof(DoctorComment));
            }
        }

        public List<Medicine> ReplacmentMedicine
        {

            get { return replacmentMedicine; }
            set
            {
                replacmentMedicine = value;
                OnPropertyChanged(nameof(ReplacmentMedicine));
            }
        }

        public List<string> MedicineComponents
        {

            get { return medicineComponents; }
            set
            {
                medicineComponents = value;
                OnPropertyChanged(nameof(MedicineComponents));
            }
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
