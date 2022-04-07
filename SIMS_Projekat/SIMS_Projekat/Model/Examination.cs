using System;

namespace SIMS_Projekat.Model
{
    public class Examination : Serialization.Serializable
    {
        public DateTime Date { get; set; }
        public string Record { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }
    }
}