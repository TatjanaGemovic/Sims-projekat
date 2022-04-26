using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Model
{
    public class MedicalRecord : Serialization.Serializable
    {
        public string ID { get; set; }
        //sadrzi istoriju pregleda
        //public List<Examination> Examinations { get; set; }
        public string CurrentTherapy { get; set; }
        public string Note { get; set; }
        public List<string> PreviousDiseases { get; set; }
        public List<string> PreviousTherapy { get; set; }
        public bool BeenHospitalized { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }

        public void fromCSV(string[] values)
        {
            ID = values[0];
            CurrentTherapy = values[1];
            Note = values[2];
            BeenHospitalized = bool.Parse(values[3]);
            Diagnosis = values[4];
            Treatment = values[5];
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID,
                CurrentTherapy,
                Note,
                BeenHospitalized.ToString(),
                Diagnosis,
                Treatment
            };
            return values;
        }
    }
}