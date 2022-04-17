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

        public Doctor CreateDoctorAccount(Doctor doctor)
        {
            return AccountRepository.CreateDoctorAccount(doctor);
        }

        public Account DeleteDoctorAccount(Doctor doctor)
        {
            return AccountRepository.DeleteDoctorAccount(doctor);
        }

        public Account EditDoctorAccount(Doctor doctor, string doctorID)
        {
            return AccountRepository.EditDoctorAccount(doctor, doctorID);
        }

        public List<Account> GetAllDoctorAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetDoctorAccountByID(string doctorID)
        {
            throw new NotImplementedException();
        }
    }
}