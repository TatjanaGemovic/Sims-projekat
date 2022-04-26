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

        public List<string> GetAvailableAppointmentsForPatient(Patient p, DateTime dt, String licence)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (dt.Date == appointment.beginningDate.Date) //za dan
                {
                    if (!appointment.operation)     // samo ako nije operacija
                    {
                        if (CheckRoomOccupancy(appointment.beginningDate)) //1.5. 2022. 15:30
                        {
                            listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));            // 15:30
                        }
                        else
                        {
                           
                            Doctor doctor = App.accountController.GetDoctorAccountByLicenceNumber(licence) as Doctor;
                            if (!CheckIfDoctorIsAvailable(doctor, appointment.beginningDate))
                            {
                                listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                            }
                            else
                            {
                                //ako pacijent bas taj ima termin kod drugog dr, a ne svog, ne moze u to vreme opet da zakaze
                                if (!CheckIfPatientIsAvailable(p, appointment.beginningDate))
                                {
                                    listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                                }
                            }
                            
                           
                        }
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
            
            foreach (Appointment appointment in GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
         
                if (dt == appointment.beginningDate)
                {
                    
                    return false;           //doktor je zauzet, znaci termin se dodaje u listu zauzetih termina
                    
                }
                
            }
            return true;       

        }
        public bool CheckIfPatientIsAvailable(Patient patient, DateTime dt)
        {

            foreach (Appointment appointment in GetAppointmentByPatientID(patient.ID))
            {
                
                if (dt == appointment.beginningDate)
                {
                   
                   return false;           //pacijent je zauzet, znaci termin se dodaje u listu zauzetih termina
                    
                }
                
            }
            return true;

        }

        public Room GetAvailableRoom(DateTime start)
        {
            ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    if (start == appointment.beginningDate)
                    {
                        rooms.Remove(appointment.room);
                    }
                }
            }
            return rooms[0];
        }

        
    }
}