using ConsoleApp.serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AccountRepository
    {
        public List<Account> Accounts { get; set; }
        public List<Patient> Patients { get; set; }
        public List<UrgentPatient> UrgentPatients { get; set; }

        private Serializer<Patient> serializer;
        private string file;

        public AccountRepository(string fileName)
        {
            Accounts = new List<Account>();
            Patients = new List<Patient>();
            serializer = new Serializer<Patient>();
            file = fileName;
        }

        public void Serialize()
        {
            serializer.toCSV(file, Patients);
        }

        public void Deserialize()
        {
            Patients = serializer.fromCSV(file);
        }

        public Account CreatePatientAccount(Patient patient)
        {
            Patients.Add(patient);
            return patient;
        }

        public Account DeletePatientAccount(Patient patient)
        {
            if (Accounts.Remove(patient))
                return patient;
            return null;
        }

        public Account EditPatientAccount(Patient patient)
        {
            foreach (Patient oldPatient in Patients)
            {
                if (oldPatient.ID.Equals(patient.ID))
                {
                    Patients.Remove(oldPatient);
                    Patients.Add(patient);
                    return patient;
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