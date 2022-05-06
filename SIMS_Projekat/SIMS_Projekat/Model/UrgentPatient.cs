namespace SIMS_Projekat.Model
{
    public class UrgentPatient : Patient
    {
        public string Symptoms { get; set; }

        public override void fromCSV(string[] values)
        {
            FirstName = values[0];
            LastName = values[1];
            ID = values[2];
            BloodType = (BloodType)int.Parse(values[3]);
            Height = double.Parse(values[4]);
            Weight = double.Parse(values[5]);
            Symptoms = values[6];
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
                Symptoms
            };
            return values;
        }
    }
}