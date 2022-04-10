using SIMS_Projekat.Serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AccountRepository
    {
        public List<Account> Accounts { get; set; }
        public static List<Patient> Patients { get; set; }
        public List<UrgentPatient> UrgentPatients { get; set; }

        private Serializer<Patient> serializer;
        private string file;
        private int ID;

        public AccountRepository(string fileName)
        {
            Accounts = new List<Account>();
            Patients = new List<Patient>();
            serializer = new Serializer<Patient>();
            file = fileName;
            ID = 100;
        }

        public void Serialize()
        {
            serializer.toCSV(file, Patients);
        }

        public void Deserialize()
        {
            Patients = serializer.fromCSV(file);

            int maxID = 0;
            foreach(Patient patient in Patients)
            {
                if (int.Parse(patient.ID) > maxID)
                    maxID = int.Parse(patient.ID);
            }
            ID = ++maxID;
        }

        public Patient CreatePatientAccount(Patient patient)
        {
            patient.ID = ID++.ToString();
            Patients.Add(patient);
            return patient;
        }

        public Account DeletePatientAccount(Patient patient)
        {
            if (Patients.Remove(patient))
                return patient;
            return null;
        }

        public Account EditPatientAccount(Patient patient, string patientID)
        {
            foreach (Patient oldPatient in Patients)
            {
                if (oldPatient.ID.Equals(patientID))
                {
                    oldPatient.FirstName = patient.FirstName;
                    oldPatient.LastName = patient.LastName;
                    oldPatient.Jmbg = patient.Jmbg;
                    oldPatient.HealthInsuranceID = patient.HealthInsuranceID;
                    oldPatient.Height = patient.Height;
                    oldPatient.Password = patient.Password;
                    oldPatient.PhoneNumber = patient.PhoneNumber;
                    oldPatient.Username = patient.Username;
                    oldPatient.Weight = patient.Weight;
                    oldPatient.BloodType = patient.BloodType;
                    oldPatient.DateOfBirth = patient.DateOfBirth;
                    oldPatient.Email = patient.Email;
                }
            }
            return null;
        }

        public List<Patient> GetAllPatientAccounts()
        {
            return Patients;
        }

        public Account GetPatientAccountByID(string patientID)
        {
            foreach(Patient patient in Patients)
            {
                if (patient.ID.Equals(patientID))
                    return patient;
            }
            return null;
        }

        public UrgentPatient CreateUrgentPatientAccount(Model.UrgentPatient urgentPatient)
        {
            throw new NotImplementedException();
        }

        public UrgentPatient DeleteUrgentPatientAccount(Model.UrgentPatient urgentPatient)
        {
            throw new NotImplementedException();
        }

        public UrgentPatient GetUrgentPatientAccountByID(string urgentPatientID)
        {
            throw new NotImplementedException();
        }

        public List<UrgentPatient> GetAllUrgentPatients()
        {
            throw new NotImplementedException();
        }




    }
}