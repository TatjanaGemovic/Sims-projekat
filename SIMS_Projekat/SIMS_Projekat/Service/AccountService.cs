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

        public bool CheckIfItsTheBeginningOfANewMonth(Patient patient)
        {
            if (patient.currentYearUsableForCancellingAppointmentsByPatient != DateTime.Now.Year || 
                patient.currentMonthUsableForCancellingAppointmentsByPatient != DateTime.Now.Month)
                return true;

            return false;
        }

        public List<Doctor> GetAvailableDoctors(DateTime dateTime)
        {
            List<Doctor> availableDoctors = new List<Doctor>();
            foreach(Doctor doctor in GetAllDoctorAccounts())
            {
                if(App.appointmentService.CheckIfDoctorIsAvailable(doctor, dateTime))
                {
                    availableDoctors.Add(doctor);
                }
            }
            return availableDoctors;
        }
    }
}