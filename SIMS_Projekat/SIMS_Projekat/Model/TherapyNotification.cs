using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class TherapyNotification : Serialization.Serializable
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public string patientID { get; set; }
        public DateTime date { get; set; } 
        public Receipt receipt { get; set; }
        public Patient patient { get; set; }


        public void fromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            receiptID = Convert.ToInt32(values[1]);
            patientID = values[2];
            date = DateTime.Parse(values[3]);

            receipt = App.receiptRepository.GetReceiptByID(receiptID);
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID.ToString(),
                receipt.receiptID.ToString(),
                patient.ID,
                date.ToString(),
            };
            return values;
        }
    }
}
