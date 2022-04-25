using System;

namespace SIMS_Projekat.Model
{
    public class Examination : Serialization.Serializable
    {
        //pregled 
        //recept?
        public DateTime Date { get; set; }
        public string Record { get; set; }

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