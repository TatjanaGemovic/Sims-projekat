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
        private Serializer<UrgentPatient> urgentPatientSerializer;
        private string patientsFile;
        private string urgentPatientsFile;

        private int ID;
        private int urgentPatientID;

        public AccountRepository(string patientsFileName, string urgentPatientsFileName)
        {
            Accounts = new List<Account>();
            Patients = new List<Patient>();
            UrgentPatients = new List<UrgentPatient>();
            serializer = new Serializer<Patient>();
            urgentPatientSerializer = new Serializer<UrgentPatient>();
            patientsFile = patientsFileName;
            urgentPatientsFile = urgentPatientsFileName;
            ID = 100;
            urgentPatientID = 100;
        }

        public void Serialize()
        {
            serializer.toCSV(patientsFile, Patients);
            urgentPatientSerializer.toCSV(urgentPatientsFile, UrgentPatients);
        }

        public void Deserialize()
        {
            Patients = serializer.fromCSV(patientsFile);
            UrgentPatients = urgentPatientSerializer.fromCSV(urgentPatientsFile);


            int maxID = 100;
            foreach(Patient patient in Patients)
            {
                if (int.Parse(patient.ID) > maxID)
                    maxID = int.Parse(patient.ID);
            }
            ID = ++maxID;

            maxID = 100;
            foreach (UrgentPatient patient in UrgentPatients)
            {
                if (int.Parse(patient.ID) > maxID)
                    maxID = int.Parse(patient.ID);
            }
            urgentPatientID = ++maxID;
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
                    oldPatient.IsUrgent = String.IsNullOrEmpty(patient.Username) || String.IsNullOrEmpty(patient.Password);
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
            foreach (Patient patient in Patients)
            {
                if (patient.ID.Equals(patientID))
                    return patient;
            }
            return null;
        }
    }
}