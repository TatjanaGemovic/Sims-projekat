using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
    public class AccountController
    {
        public AccountService AccountService { get; set; }

        public Patient CreatePatientAccount(Patient patient)
        {
            return AccountService.CreatePatientAccount(patient);
        }

        public Account DeletePatientAccount(Patient patient)
        {
            return AccountService.DeletePatientAccount(patient);
        }

        public Account EditPatientAccount(Patient patient, string patientID)
        {
            return AccountService.EditPatientAccount(patient, patientID);
        }

        public List<Patient> GetAllPatientAccounts()
        {
            return AccountService.GetAllPatientAccounts();
        }

        public Account GetPatientAccountByID(string patientID)
        {
            throw new NotImplementedException();
        }

        public Doctor CreateDoctorAccount(Doctor doctor)
        {
            return AccountService.CreateDoctorAccount(doctor);
        }

        public Account DeleteDoctorAccount(Doctor doctor)
        {
            return AccountService.DeleteDoctorAccount(doctor);
        }

        public Account EditDoctorAccount(Doctor doctor, string doctorID)
        {
            return AccountService.EditDoctorAccount(doctor, doctorID);
        }

        public List<Doctor> GetAllDoctorAccounts()
        {
            return AccountService.GetAllDoctorAccounts();
        }

        public Account GetDoctorAccountByID(string doctorID)
        {
            throw new NotImplementedException();
        }
    }
}