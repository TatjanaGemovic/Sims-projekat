using System;

namespace SIMS_Projekat.Model
{
    public class Allergen : Serialization.Serializable
    {
        public string Name { get; set; }

        public void fromCSV(string[] values)
        {
            Name = values[0];
        }

        public string[] toCSV()
        {
            string[] values =
            {
                Name
            };
            return values;
        }
    }
}