using System;

namespace SIMS_Projekat.Model
{
    public class Appointment : Serialization.Serializable
    {
        public DateTime Date { get; set; }
        public int AppointmentID { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }


        public Patient Patient
        {
            get
            {
                return Patient;
            }
            set
            {
                if (this.Patient == null || !this.Patient.Equals(value))
                {
                    if (this.Patient != null)
                    {
                        Patient oldPatient = this.Patient;
                        this.Patient = null;
                        oldPatient.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.Patient = value;
                        this.Patient.AddAppointment(this);
                    }
                }
            }
        }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }
    }
}