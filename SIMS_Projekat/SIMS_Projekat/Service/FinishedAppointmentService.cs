using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class FinishedAppointmentService
    {
        public FinishedAppointmentRepository finishedAppointmentRepository;
        public FinishedAppointment SetAppointment(FinishedAppointment appointment)
        {
            return finishedAppointmentRepository.SetAppointment(appointment);

        }

        public FinishedAppointment DeleteAppointment(FinishedAppointment appointment)
        {
            return finishedAppointmentRepository.DeleteAppointment(appointment);
        }

        public FinishedAppointment AddFinishedAppointment(FinishedAppointment appointment)
        {
            return finishedAppointmentRepository.AddFinishedAppointment(appointment);
        }

        public FinishedAppointment GetAppointmentByID(int appointmentID)
        {
            return finishedAppointmentRepository.GetAppointmentByID(appointmentID);
        }

        public List<FinishedAppointment> GetAppointmentByPatientID(string patientID)
        {
            return finishedAppointmentRepository.GetAppointmentByPatientID(patientID);
        }

        public List<FinishedAppointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            return finishedAppointmentRepository.GetAppointmentByDoctorLicenceNumber(licenceNumber);
        }

        public List<FinishedAppointment> GetAllAppointments()
        {
            return finishedAppointmentRepository.GetAllAppointments();
        }
        public FinishedAppointment AddNoteToAppointment(int appointmentID, int noteID)
        {
            FinishedAppointment appointment = GetAppointmentByID(appointmentID);
            appointment.patientNoteID = noteID;
            return appointment;
        }
        public bool EraseNoteForAppointmentIfExists(int noteID, Patient patient)
        {
            List<FinishedAppointment> appointmentList = GetAppointmentByPatientID(patient.ID);

            FinishedAppointment appointment = appointmentList.Find(appoint => appoint.patientNoteID == noteID);
            if(appointment != null)
            {
                appointment.patientNoteID = 0;
                return true;
            }
            return false;
        }

        public FinishedAppointment GetAppointmentByNoteID(int noteiD)
        {
            return finishedAppointmentRepository.GetAppointmentByNoteID(noteiD);
        }
    }
}
