using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AppointmentRepository
    {
        public string Path { get; set; }

        public Model.Appointment SetAppointment(Model.Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Model.Appointment DeleteAppointment(Model.Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Model.Appointment GetAppointmentByID(int appointmentID)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentByPatientID(string patientID)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Appointment> appointment;



        public System.Collections.Generic.List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.Generic.List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Model.Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }


        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.Generic.List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
                this.appointment.Add(newAppointment);
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                    this.appointment.Remove(oldAppointment);
        }


        public void RemoveAllAppointment()
        {
            if (appointment != null)
                appointment.Clear();
        }

    }
}