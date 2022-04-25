using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Model
{
    public class MedicalRecord : Serialization.Serializable
    {
        //sadrzi istoriju pregleda
        public List<Examination> Examinations { get; set; }
        public string CurrentTherapy { get; set; }
        public string Note { get; set; }
        public List<string> PreviousDiseases { get; set; }
        public List<string> PreviousTherapy { get; set; }
        public bool BeenHospitalized { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }



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