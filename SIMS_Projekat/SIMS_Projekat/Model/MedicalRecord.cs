using System;

namespace SIMS_Projekat.Model
{
    public class MedicalRecord : Serialization.Serializable
    {
        public Examination Examination { get; set; }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }
    }
}