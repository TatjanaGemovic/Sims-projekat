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
    }
}