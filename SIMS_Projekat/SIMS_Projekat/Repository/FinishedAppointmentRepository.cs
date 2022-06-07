using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class FinishedAppointmentRepository
    {
        private string path { get; set; }
        private Serializer<FinishedAppointment> serializer;
        private List<FinishedAppointment> finishedAppointmentList;
        //private int id;

        public List<FinishedAppointment> AppointmentList
        {
            get
            {
                if (finishedAppointmentList == null)
                    finishedAppointmentList = new List<FinishedAppointment>();
                return finishedAppointmentList;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (FinishedAppointment oAppointment in value)
                        AddFinishedAppointment(oAppointment);
                }
            }
        }

        public FinishedAppointmentRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<FinishedAppointment>();
            //id = 0;
        }


        public FinishedAppointment SetAppointment(FinishedAppointment appointment)
        {
            int index;
            foreach (FinishedAppointment appoint in finishedAppointmentList)
            {
                FinishedAppointment appointment1 = finishedAppointmentList.Find(appoint => appoint.finishedAppointmentID == appointment.finishedAppointmentID);
                index = finishedAppointmentList.IndexOf(appointment1);

                if (appointment1 != null)
                {
                    appointment1.patient = appointment.patient;
                    appointment1.beginningDate = appointment.beginningDate;
                    appointment1.endDate = appointment.endDate;
                    appointment1.doctor = appointment.doctor;
                    appointment1.operation = appointment.operation;
                    appointment1.isScheduledByPatient = appointment.isScheduledByPatient;
                    appointment1.patientNoteID = appointment.patientNoteID;
                    return appointment1;
                }

            }
            return null;
        }

        public FinishedAppointment GetAppointmentByID(int appointmentID)
        {
            foreach (FinishedAppointment appointment in finishedAppointmentList)
            {
                FinishedAppointment appointment1 = finishedAppointmentList.Find(appointment => appointment.finishedAppointmentID == appointmentID);

                if (appointment1 != null)
                {
                    return appointment1;
                }
            }
            return null;
        }

        public List<FinishedAppointment> GetAppointmentByPatientID(string patientID1)
        {
            List<FinishedAppointment> appointmentListForPatient = new List<FinishedAppointment>();
            foreach (FinishedAppointment appointment in finishedAppointmentList)
            {
                if (appointment.patient.ID.Equals(patientID1))
                {
                    appointmentListForPatient.Add(appointment);
                }
            }
            return appointmentListForPatient;
        }

        public List<FinishedAppointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            List<FinishedAppointment> appointmentListForDoctor = new List<FinishedAppointment>();
            foreach (FinishedAppointment appointment in finishedAppointmentList)
            {
                //Appointment appointment1 = appointmentList.FindLast(appointment => appointment.Patient.PatientID == patientID);

                if (appointment.doctor.LicenceNumber.Equals(licenceNumber))
                {
                    appointmentListForDoctor.Add(appointment);
                }
            }
            return appointmentListForDoctor;
        }

        public FinishedAppointment GetAppointmentByNoteID(int noteiD)
        {
            foreach (FinishedAppointment appointment in finishedAppointmentList)
            {
                FinishedAppointment appointment1 = finishedAppointmentList.Find(appointment => appointment.patientNoteID == noteiD);

                if (appointment1 != null)
                {
                    return appointment1;
                }
            }
            return null;
        }

        public List<FinishedAppointment> GetAllAppointments()
        {
            return finishedAppointmentList;
        }

        public FinishedAppointment AddFinishedAppointment(FinishedAppointment newAppointment)
        {
            //id++;
            if (newAppointment == null)
                return null;
            if (this.finishedAppointmentList == null)
                this.finishedAppointmentList = new List<FinishedAppointment>();
            if (!this.finishedAppointmentList.Contains(newAppointment))
            {
                this.finishedAppointmentList.Add(newAppointment);
            }

            return newAppointment;
        }

        public FinishedAppointment DeleteAppointment(FinishedAppointment oldAppointment)
        {
            if (oldAppointment == null)
                return null;
            if (this.finishedAppointmentList != null)
                if (this.finishedAppointmentList.Contains(oldAppointment))
                    this.finishedAppointmentList.Remove(oldAppointment);
            return oldAppointment;
        }

        public void RemoveAllAppointment()
        {
            if (finishedAppointmentList != null)
                finishedAppointmentList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, finishedAppointmentList);
        }

        public void Deserialize()
        {
            finishedAppointmentList = serializer.fromCSV(path);
        }
    }
}
