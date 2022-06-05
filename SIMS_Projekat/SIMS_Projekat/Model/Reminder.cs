using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Reminder : Serialization.Serializable
    {
        public int ID { get; set; }
        public DateTime startTime { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public string patientID { get; set; }    
        public string isRepeatable { get; set; }
        public Patient patient { get; set; }
        public void fromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            startTime = DateTime.Parse(values[1]);
            content = values[2];
            type = values[3];
            patientID = values[4];
            isRepeatable= values[5];
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID.ToString(),
                startTime.ToString(),
                content,
                type,
                patient.ID,
                isRepeatable
            };
            return values;
        }
    }
}
