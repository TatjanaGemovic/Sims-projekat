using SIMS_Projekat.Serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AccountRepository
    {
        public List<Account> Accounts { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors{ get; set; }

        private Serializer<Patient> patientSerializer;
        private Serializer<Doctor> doctorSerializer;


        private string patientsFile;
        private string doctorFile;


        private int ID;

        public AccountRepository(string patientsFileName, string doctorFileName)
        {
            Accounts = new List<Account>();
            Patients = new List<Patient>();
            Doctors = new List<Doctor>();
            patientSerializer = new Serializer<Patient>();
            doctorSerializer = new Serializer<Doctor>();
            patientsFile = patientsFileName;
            doctorFile = doctorFileName;
            ID = 100;
        }

        public void Serialize()
        {
            patientSerializer.toCSV(patientsFile, Patients);
            doctorSerializer.toCSV(doctorFile, Doctors);
        }

        public void Deserialize()
        {
            Patients = patientSerializer.fromCSV(patientsFile);
            Doctors = doctorSerializer.fromCSV(doctorFile);

            int maxID = 100;
            foreach(Patient patient in Patients)
            {
                if (int.Parse(patient.ID) > maxID)
                    maxID = int.Parse(patient.ID);
            }
            foreach (Doctor doctor in Doctors)
            {
                if (int.Parse(doctor.ID) > maxID)
                    maxID = int.Parse(doctor.ID);
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
            return Patients.Remove(patient) ? patient : null;
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
                    oldPatient.Allergens = patient.Allergens;
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

        public Doctor CreateDoctorAccount(Doctor doctor)
        {
            doctor.ID = ID++.ToString();
            Doctors.Add(doctor);
            return doctor;
        }

        public Account DeleteDoctorAccount(Doctor doctor)
        {
            return Doctors.Remove(doctor) ? doctor : null;
        }

        public Account EditDoctorAccount(Doctor doctor, string doctorID)
        {
            foreach (Doctor oldDoctor in Doctors)
            {
                if (oldDoctor.ID.Equals(doctorID))
                {
                    oldDoctor.FirstName = doctor.FirstName;
                    oldDoctor.LastName = doctor.LastName;
                    oldDoctor.Jmbg = doctor.Jmbg;
                    oldDoctor.Password = doctor.Password;
                    oldDoctor.PhoneNumber = doctor.PhoneNumber;
                    oldDoctor.Username = doctor.Username;
                    oldDoctor.DateOfBirth = doctor.DateOfBirth;
                    oldDoctor.Email = doctor.Email;
                    oldDoctor.LicenceNumber = doctor.LicenceNumber;

                    return oldDoctor;
                }
            }
            return null;
        }

        public List<Doctor> GetAllDoctorAccounts()
        {
            return Doctors;
        }

        public Account GetDoctorAccountByID(string doctorID)
        {
            foreach (Doctor doctor in Doctors)
            {
                if (doctor.ID.Equals(doctorID))
                    return doctor;
            }
            return null;
        }
        public Account GetDoctorAccountByLicenceNumber(string licence)
        {
            foreach (Doctor doctor in Doctors)
            {
                if (doctor.LicenceNumber.Equals(licence))
                    return doctor;
            }
            return null;
        }

    }
}