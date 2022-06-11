using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using SIMS_Projekat;

namespace SIMS_Projekat.Repository
{
    public class AppointmentRepository
    {
        private string path { get; set; }
        private Serializer<Appointment> serializer;
        private List<Appointment> appointmentList;
        private int id;

        public List<Appointment> AppointmentList
        {
            get
            {
                if (appointmentList == null)
                    appointmentList = new List<Appointment>();
                return appointmentList;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }

        public AppointmentRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<Appointment>();
            id = 0;
        }
        

        public Appointment SetAppointment(Appointment appointment)
        {
            int index;
            foreach (Appointment appoint in appointmentList)
            {
                Appointment appointment1 = appointmentList.Find(appoint => appoint.appointmentID == appointment.appointmentID) ;
                index = appointmentList.IndexOf(appointment1);

                if (appointment1 != null)
                {
                    appointment1.patient = appointment.patient;
                    appointment1.beginningDate = appointment.beginningDate;
                    appointment1.endDate = appointment.endDate;
                    appointment1.doctor = appointment.doctor;
                    appointment1.room = appointment.room;
                    appointment1.operation = appointment.operation;
                    appointment1.isDelayedByPatient = appointment.isDelayedByPatient;
                    appointment1.isScheduledByPatient = appointment.isScheduledByPatient;
                    appointment1.reminderForPatientID = appointment.reminderForPatientID;
                    return appointment1;
                }

            }
            return null;
        }  

        public Appointment GetAppointmentByID(int appointmentID)
        {
            foreach (Appointment appointment in appointmentList)
            {
                Appointment appointment1 = appointmentList.Find(appointment => appointment.appointmentID == appointmentID);

                if (appointment1 != null)
                {
                    return appointment1;
                }
            }
            return null;
        }

        public List<Appointment> GetAppointmentByPatientID(string patientID)
        {
            List<Appointment> appointmentListForPatient = new List<Appointment>();
            foreach (Appointment appointment in appointmentList)
            {
                if (appointment.patient.ID.Equals(patientID))
                {
                    appointmentListForPatient.Add(appointment);
                }
            }
            return appointmentListForPatient;
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            List<Appointment> appointmentListForDoctor = new List<Appointment>();
            foreach (Appointment appointment in appointmentList)
            {
                if (appointment.doctor.LicenceNumber.Equals(licenceNumber))
                {
                    appointmentListForDoctor.Add(appointment);
                }
            }
            return appointmentListForDoctor;
        }

        public Appointment GetAppointmentByReminderID(int reminderID)
        {
            foreach (Appointment appointment in appointmentList)
            {
                Appointment appointment1 = appointmentList.Find(appointment => appointment.reminderForPatientID == reminderID);

                if (appointment1 != null)
                {
                    return appointment1;
                }
            }
            return null;
        }
        
        public List<Appointment> GetAllAppointments()
        {
            return appointmentList;
        }

        public Appointment AddAppointment(Appointment newAppointment)
        {
            id++;
            if (newAppointment == null)
                return null;
            if (this.appointmentList == null)
                this.appointmentList = new List<Appointment>();
            if (!this.appointmentList.Contains(newAppointment))
            {
                newAppointment.appointmentID = id; 
                this.appointmentList.Add(newAppointment);
            }
                
            return newAppointment;
        }

        public Appointment DeleteAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return null;
            if (this.appointmentList != null)
                if (this.appointmentList.Contains(oldAppointment))
                    this.appointmentList.Remove(oldAppointment);
            return oldAppointment;
        }

        public void RemoveAllAppointment()
        {
            if (appointmentList != null)
                appointmentList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, appointmentList);
        }

        public void Deserialize()
        {
            appointmentList = serializer.fromCSV(path);

            foreach (Appointment appointment in appointmentList)
            {
                id = appointment.appointmentID;
            }
            CheckForFinishedAppointmentID();
        }
        public void CheckForFinishedAppointmentID()
        {
            foreach (FinishedAppointment appointment in App.finishedAppointmentRepo.GetAllAppointments())
            {
                if(id < appointment.finishedAppointmentID)
                    id = appointment.finishedAppointmentID;
            }
        }


    }
}