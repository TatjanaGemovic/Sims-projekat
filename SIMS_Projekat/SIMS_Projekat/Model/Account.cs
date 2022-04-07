using System;

namespace SIMS_Projekat.Model
{
    public class Account : Serialization.Serializable
    {
        public string username { get; set; }
        public string password { get; set; }
        public string id { get; set; }

        public string FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public void ToCSV()
        {
            throw new NotImplementedException();
        }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }
    }
}