using System;

namespace SIMS_Projekat.Model
{
    public class MedicalRecord : Serialization.Serializable
    {
        //sadrzi istoriju pregleda
        public System.Collections.Generic.List<Examination> examination;

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