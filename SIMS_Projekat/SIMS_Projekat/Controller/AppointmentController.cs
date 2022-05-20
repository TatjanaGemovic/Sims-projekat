using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
    public class AppointmentController
    {
        public AppointmentService appointmentService;
        public Model.Appointment SetAppointment(Model.Appointment appointment)
        {
            return appointmentService.SetAppointment(appointment);

        }

        public Model.Appointment DeleteAppointment(Model.Appointment appointment)
        {
            return appointmentService.DeleteAppointment(appointment);
        }

        public Model.Appointment AddAppointment(Model.Appointment appointment)
        {
            return appointmentService.AddAppointment(appointment);
        }

        public Model.Appointment GetAppointmentByID(int appointmentID)
        {
            return appointmentService.GetAppointmentByID(appointmentID);
        }

        public List<Appointment> GetAppointmentByPatientID(string patientID)
        {
            return appointmentService.GetAppointmentByPatientID(patientID);
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            return appointmentService.GetAppointmentByDoctorLicenceNumber(licenceNumber);
        }

        public List<Appointment> GetAllAppointments()
        {
            return appointmentService.GetAllAppointments();
        }

        public List<string> GetAvailableAppointmentsForDoctor(Doctor doctor, string pickedDate, Patient selectedPatient, bool op, Room selectedRoom)
        {
            return appointmentService.GetAvailableAppointmentsForDoctor(doctor, pickedDate, selectedPatient, op, selectedRoom);
        }

        public List<string> GetAvailableAppointmentsForPatient(Patient p, DateTime dt, string licence) 
        {
            return appointmentService.GetAvailableAppointmentsForPatient(p, dt, licence);
        } 

        public Room GetAvailableRoom(DateTime start)
        {
            return appointmentService.GetAvailableRoom(start);
        }

        public List<Appointment> GetAppointmentsByRoomIdAndDate(string roomID, DateTime date)
        {
            return appointmentService.GetAppointmentsByRoomIdAndDate(roomID, date);
        }

        public bool CheckIfDoctorIsAvailable(Doctor doctor, DateTime dt)
        {
            return appointmentService.CheckIfDoctorIsAvailable(doctor, dt);
        }

        public bool CheckIfPatientIsAvailable(Patient patient, DateTime dt)
        {
            return appointmentService.CheckIfPatientIsAvailable(patient, dt);
        }
        public Appointment CreateRandomAppointment(Patient p)
        {
            return appointmentService.CreateRandomAppointment(p);
        }

        public List<string> CreateAppointmentTime()
        {
            return appointmentService.CreateAppointmentTime();
        }

        public bool CheckForScheduledAppointments(Patient patient, DateTime beginningOfMonth)
        {
            return appointmentService.CheckForScheduledAppointments(patient, beginningOfMonth);
        }

        public void RescheduleCurrentAppointmentForDoctor(Doctor doctor, DateTime time)
        {
            appointmentService.RescheduleCurrentAppointmentForDoctor(doctor, time);
        }

    }
}