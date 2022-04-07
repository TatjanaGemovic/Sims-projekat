using System;

namespace SIMS_Projekat.Model
{
    public class Allergen : Serialization.Serializable
    {
        public Patient[] patient { get; set; }

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