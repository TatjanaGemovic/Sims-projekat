using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Note : Serialization.Serializable
    {
        public DateTime creationDate { get; set; }
        public int noteID { get; set; }
        public Patient patient { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string patientID { get; set; }
        public void fromCSV(string[] values)
        {
            noteID = Convert.ToInt32(values[0]);
            creationDate = DateTime.Parse(values[1]);
            patientID = values[2];
            title = values[3];
            content = values[4];
            
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                noteID.ToString(),
                creationDate.ToString(),
                patient.ID,
                title,
                content
            };
            return values;
        }
    }
}
