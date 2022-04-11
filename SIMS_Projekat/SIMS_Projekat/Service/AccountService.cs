using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Service
{
    public class AccountService
    {
        public AccountRepository AccountRepository { get; set; }

        public Patient CreatePatientAccount(Patient patient)
        {
            return AccountRepository.CreatePatientAccount(patient);
        }

        public Account DeletePatientAccount(Patient patient)
        {
            return AccountRepository.DeletePatientAccount(patient);
        }

        public Account EditPatientAccount(Patient patient, string patientID)
        {
            return AccountRepository.EditPatientAccount(patient, patientID);
        }

        public List<Account> GetAllPatientAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetPatientAccountByID(string patientID)
        {
            throw new NotImplementedException();
        }

        public UrgentPatient CreateUrgentPatientAccount(UrgentPatient urgentPatient)
        {
            return AccountRepository.CreateUrgentPatientAccount(urgentPatient);
        }

        public UrgentPatient EditUrgentPatientAccount(UrgentPatient urgentPatient, string ID)
        {
            return AccountRepository.EditUrgentPatientAccount(urgentPatient, ID);
        }

        public UrgentPatient DeleteUrgentPatientAccount(UrgentPatient urgentPatient)
        {
            return AccountRepository.DeleteUrgentPatientAccount(urgentPatient);
        }

        public UrgentPatient GetUrgentPatientAccountByID(string urgentPatientID)
        {
            throw new NotImplementedException();
        }

        public List<UrgentPatient> GetAllUrgentPatients()
        {
            return AccountRepository.GetAllUrgentPatients();
        }


    }
}