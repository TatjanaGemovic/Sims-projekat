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
            App.medRecordRepository.RemoveMedicalRecord(patient.MedicalRecord);
            return AccountRepository.DeletePatientAccount(patient);
        }

        public Account EditPatientAccount(Patient patient, string patientID)
        {
            return AccountRepository.EditPatientAccount(patient, patientID);
        }

        public List<Patient> GetAllPatientAccounts()
        {
            return AccountRepository.GetAllPatientAccounts();
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

        public List<Doctor> GetAllDoctorAccounts()
        {
            return AccountRepository.GetAllDoctorAccounts();
        }

        public Account GetDoctorAccountByID(string doctorID)
        {
            return AccountRepository.GetDoctorAccountByID(doctorID);
        }

        public void Serialize()
        {
            AccountRepository.Serialize();
            App.medRecordRepository.Serialize();
        }

        public Account GetDoctorAccountByLicenceNumber(string licenceNumber)
        {
            return AccountRepository.GetDoctorAccountByLicenceNumber(licenceNumber);

        }
    }
}