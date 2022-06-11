using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Receipt : Serialization.Serializable
    {
        public int receiptID { get; set; }
        public int appointmentID { get; set; }
        public string Record { get; set; }
        public string medicineID { get; set; }
        public Medicine medicine { get; set; }
        public DateTime beginningDate { get; set; }
        public DateTime endDate { get; set; }
        public string patientID { get; set; }
        public Patient patient { get; set; }
        public int DailyMed { get; set; }
        public int patientNoteID { get; set; }
        public void fromCSV(string[] values)
        {
            receiptID = Convert.ToInt32(values[0]);
            beginningDate = DateTime.Parse(values[1]);
            endDate = DateTime.Parse(values[2]);
            patientID = values[3];
            Record = values[4];
            DailyMed = int.Parse(values[5]);
            appointmentID = Convert.ToInt32(values[6]);
            medicineID = values[7];
            patientNoteID = Convert.ToInt32(values[8]);

            medicine = App.medicineController.GetMedicineByID(medicineID);
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                receiptID.ToString(),
                beginningDate.ToString(),
                endDate.ToString(),
                patient.ID,
                Record,
                DailyMed.ToString(),
                appointmentID.ToString(),
                medicine.MedicineID,
                patientNoteID.ToString()
            };
            return values;
        }
    }
}
