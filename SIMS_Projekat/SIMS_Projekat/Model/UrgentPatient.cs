namespace SIMS_Projekat.Model
{
    public class UrgentPatient : Serialization.Serializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BloodType BloodType { get; set; }
        public string Informations { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string ID { get; set; }

        public void fromCSV(string[] values)
        {
            FirstName = values[0];
            LastName = values[1];
            ID = values[2];
            BloodType = (BloodType)int.Parse(values[3]);
            Height = double.Parse(values[4]);
            Weight = double.Parse(values[5]);
            Informations = values[6];
        }

        public string[] toCSV()
        {
            string[] values =
          {
                FirstName,
                LastName,
                ID,
                ((int)BloodType).ToString(),
                Height.ToString(),
                Weight.ToString(),
                Informations
            };
            return values;
        }
    }
}