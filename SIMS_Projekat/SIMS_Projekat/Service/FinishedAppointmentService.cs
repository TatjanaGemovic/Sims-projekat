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

        public FinishedAppointment AddFinishedAppointment(FinishedAppointment appointment, int id)
        {
            return finishedAppointmentRepository.AddFinishedAppointment(appointment, id);
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
    }
}
