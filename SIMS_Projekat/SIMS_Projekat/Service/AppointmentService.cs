using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Service
{
    public class AppointmentService
    {
        public AppointmentRepository appointmentRepository;
        public Appointment SetAppointment(Appointment appointment)
        {
            return appointmentRepository.SetAppointment(appointment);

        }

        public Model.Appointment DeleteAppointment(Model.Appointment appointment)
        {
            return appointmentRepository.DeleteAppointment(appointment);
        }

        public Model.Appointment AddAppointment(Model.Appointment appointment)
        {
            return appointmentRepository.AddAppointment(appointment);
        }

        public Model.Appointment GetAppointmentByID(int appointmentID)
        {
            return appointmentRepository.GetAppointmentByID(appointmentID);
        }

        public List<Appointment> GetAppointmentByPatientID(string patientID)
        {
            return appointmentRepository.GetAppointmentByPatientID(patientID);
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            return appointmentRepository.GetAppointmentByDoctorLicenceNumber(licenceNumber);
        }

        public List<Appointment> GetAllAppointments()
        {
            return appointmentRepository.GetAllAppointments();
        }

        public List<string> GetAvailableAppointmentsForPatient(Patient p, DateTime dt)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (dt.Date == appointment.beginningDate.Date) 
                {
                    if (!appointment.operation)
                    {
                        if (CheckRoomOccupancy(appointment.beginningDate)) 
                        {
                            listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString("HH:mm"));
                        }
                        //ovde sad ide else ako ima soba, proverava se da li dr slobodan tada a mozda i ne, 
                    }
                }

            }
            return listOfTakenAppointmentTime;
        }

        public bool CheckRoomOccupancy(DateTime dt) 
        {
            ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);
            //int numberOfAvailableRooms = rooms.Count;
            //Room availableRoom = new Room();

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    if (dt == appointment.beginningDate)
                    {
                        //numberOfAvailableRooms--;
                        rooms.Remove(appointment.room);
                    }
                }              
            }

            if (rooms.Count == 0) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckIfDoctorIsAvailable(Doctor doctor, DateTime dt)
        {
            
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    if (dt == appointment.beginningDate)
                    {
                        if (appointment.doctor.Equals(doctor))
                        {
                            return false;           //doktor je zauzet, znaci termin se dodaje u listu zauzetih termina
                        }
                    }
                }
            }
            return true;       

        }
    }
}