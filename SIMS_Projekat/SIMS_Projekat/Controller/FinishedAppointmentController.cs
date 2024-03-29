﻿using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class FinishedAppointmentController
    {
        public FinishedAppointmentService finishedAppointmentService;
        public FinishedAppointment SetAppointment(FinishedAppointment appointment)
        {
            return finishedAppointmentService.SetAppointment(appointment);

        }

        public FinishedAppointment DeleteAppointment(FinishedAppointment appointment)
        {
            return finishedAppointmentService.DeleteAppointment(appointment);
        }

        public FinishedAppointment AddFinishedAppointment(FinishedAppointment appointment, int id)
        {
            return finishedAppointmentService.AddFinishedAppointment(appointment);
        }

        public FinishedAppointment GetAppointmentByID(int appointmentID)
        {
            return finishedAppointmentService.GetAppointmentByID(appointmentID);
        }

        public List<FinishedAppointment> GetAppointmentByPatientID(string patientID)
        {
            return finishedAppointmentService.GetAppointmentByPatientID(patientID);
        }

        public List<FinishedAppointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            return finishedAppointmentService.GetAppointmentByDoctorLicenceNumber(licenceNumber);
        }

        public List<FinishedAppointment> GetAllAppointments()
        {
            return finishedAppointmentService.GetAllAppointments();
        }

        public FinishedAppointment AddNoteToAppointment(int appointmentID, int noteID)
        {
            return finishedAppointmentService.AddNoteToAppointment(appointmentID, noteID);
        }

        public bool EraseNoteForAppointmentIfExists(int noteID, Patient patient)
        {
            return finishedAppointmentService.EraseNoteForAppointmentIfExists(noteID, patient);
        }
        public FinishedAppointment GetAppointmentByNoteID(int noteiD)
        {
            return finishedAppointmentService.GetAppointmentByNoteID(noteiD);
        }
        
    }
}
