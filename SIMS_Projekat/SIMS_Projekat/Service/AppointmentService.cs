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

        public List<string> GetAvailableAppointmentsForDoctor(Doctor doctor, String pickedDate, Patient selectedPatient, bool op, Room selectedRoom)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            foreach (Appointment appointment in GetAllAppointments())
            {//uopste ne udje ovde
                DateTime dt = appointment.beginningDate;
                string dateTime = dt.ToString("MM/dd/yyyy HH:mm");
                String[] datePart = dateTime.Split(" ");
                string date = datePart[0]; //datum
                String[] deoDatuma = date.Split("/");
                int mesec = int.Parse(deoDatuma[0]);
                int dan = int.Parse(deoDatuma[1]);

                String[] deoDatuma2 = pickedDate.Split("/");
                int mesec2 = int.Parse(deoDatuma2[0]);
                int dan2 = int.Parse(deoDatuma2[1]);

                if (dan2 == dan) //za dan
                {
                    if (op) //ako trazim za operacijun
                    {
                        //if (appointment.operation)
                        //{
                        if (CheckRoomOccupancyOperation(appointment.beginningDate, selectedRoom)) //provera operacione sale
                        {
                            listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                        }
                        else
                        {
                            //Patient patient = (Patient)App.accountController.GetPatientAccountByID(selectedPatient.ID); //problem
                            if (!CheckIfPatientIsAvailable(selectedPatient, appointment.beginningDate))
                            {
                                listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                            }
                            else
                            {
                                if (!CheckIfDoctorIsAvailable(doctor, appointment.beginningDate))
                                {
                                    listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                                }
                            }
                        }
                        //}
                    }
                    else
                    {
                        //if (!appointment.operation)
                        //{
                        if (CheckRoomOccupancyOperation(appointment.beginningDate, selectedRoom)) //provera operacione sale
                        {
                            listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                        }
                        else
                        {
                            //Patient patient = (Patient)App.accountController.GetPatientAccountByID(selectedPatient.ID); //problem
                            if (!CheckIfPatientIsAvailable(selectedPatient, appointment.beginningDate))
                            {
                                listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                            }
                            else
                            {
                                if (!CheckIfDoctorIsAvailable(doctor, appointment.beginningDate))
                                {
                                    listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                                }
                            }
                        }
                        //}
                    }
                }
            }
            return listOfTakenAppointmentTime;
        }

        public bool CheckRoomOccupancy(DateTime dt)
        {
            ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);

            foreach (Appointment app in GetAllAppointments())
            {
                if (!app.operation)
                {
                    if (dt == app.beginningDate)
                    {
                        rooms.Remove(app.room);
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
        public bool CheckRoomOccupancyOperation(DateTime dt, Room selectedRoom)
        {
            //ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.operatingRoom);
            Room room = App.roomController.GetRoomByID(selectedRoom.RoomID);

            foreach (Appointment app in GetAllAppointments())
            {
                if (dt == app.beginningDate)
                {
                    if (room == app.room)
                    {
                        return true;
                    }
                }
            }
            return false;
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
    }
}