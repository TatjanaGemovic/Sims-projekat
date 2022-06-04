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
        public string content { get; set; }
        public string patientID { get; set; }
       
        public Patient patient { get; set; }
        public void fromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            startTime = DateTime.Parse(values[1]);
            content = values[2];
            patientID = values[3];
           
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID.ToString(),
                startTime.ToString(),
                content,
                patient.ID,      
            };
            return values;
        }
    }
}
