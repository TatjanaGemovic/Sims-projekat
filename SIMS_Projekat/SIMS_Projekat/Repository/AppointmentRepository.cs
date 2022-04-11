using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;

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
                    appointment1.date = appointment.date;
                    appointment1.doctor = appointment.doctor;
                    appointment1.room = appointment.room;
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
            //foreach (Appointment appointment in appointmentList)
            //{
            //    Appointment appointment1 = appointmentList.FindLast(appointment => appointment.Patient.PatientID == patientID);

            //    if (appointment1 != null)
            //    {
            //        return appointment1;
            //    }
            //}
            return null;
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            //foreach (Appointment appointment in appointmentList)
            //{
            //    Appointment appointment1 = appointmentList.FindLast(appointment => appointment.Doctor.LicenceNumber == licenceNumber);

            //    if (appointment1 != null)
            //    {
            //        return appointment1;
            //    }
            //}
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


        //public void RemoveAppointment(Appointment oldAppointment)
        //{
        //    if (oldAppointment == null)
        //        return;
        //    if (this.appointmentList != null)
        //        if (this.appointmentList.Contains(oldAppointment))
        //            this.appointmentList.Remove(oldAppointment);
        //}

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
               
                appointment.patient = new Patient()
                {
                    ID = appointment.patientID,
                    FirstName = "Ivana",
                    LastName = "Ivanovic",
                    Email = "ivana@gmail.com",
                    Jmbg = "512155120",
                    Username = "icka",
                    Password = "icka123",
                    PhoneNumber = "0645554442",
                    DateOfBirth = new DateTime(2000, 10, 15),
                    BloodType = BloodType.A_Positive,
                    Height = 178.0,
                    Weight = 80.0,
                    HealthInsuranceID = "005426",
                    

                };
                appointment.room = new Room()
                {
                    RoomID = appointment.roomID,
                    Floor = 1,
                    Type = RoomType.examRoom,
                    RoomNumber = 4,
                    Available = false
                };
                appointment.doctor = new Doctor()
                {
                    LicenceNumber = appointment.licenceNumber,
                    FirstName = "Pera",
                    LastName = "Peric",
                    Email = "pera@gmail.com",
                    Jmbg = "111122440",
                    Username = "pera",
                    Password = "pera123",
                    PhoneNumber = "0641111111",
                    DateOfBirth = new DateTime(1994, 5, 15),
                    ID = "10"
                };

                id = appointment.appointmentID;
            }
        }

        
    }
}