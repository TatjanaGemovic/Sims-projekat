namespace SIMS_Projekat.Model
{
    public class UrgentPatient : Serialization.Serializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BloodType { get; set; }
        public string HealthInsuranceID { get; set; }
        public string Informations { get; set; }

        public void fromCSV(string[] values)
        {
            throw new System.NotImplementedException();
        }

        public string[] toCSV()
        {
            throw new System.NotImplementedException();
        }
    }
}