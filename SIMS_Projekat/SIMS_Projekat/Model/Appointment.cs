using System;

namespace SIMS_Projekat.Model
{
    public class Appointment : Serialization.Serializable
    {
        public DateTime date { get; set; }
        public int appointmentID { get; set;}
        
        //public int AppointmentID
        //{
        //    get
        //    {
        //        return appointmentID;
        //    }
        //}
        public Doctor doctor { get; set; }
        public Room room { get; set; }

        public string roomID { get; set; }

        public string licenceNumber { get; set; }
        public string patientID { get; set; }

        public Patient patient { get; set; }

        //public Patient Patient
        //{
        //    get
        //    {
        //        return Patient;
        //    }
        //    set
        //    {
        //        if (this.Patient == null || !this.Patient.Equals(value))
        //        {
        //            if (this.Patient != null)
        //            {
        //                Patient oldPatient = this.Patient;
        //                this.Patient = null;
        //                oldPatient.RemoveAppointment(this);
        //            }
        //            if (value != null)
        //            {
        //                this.Patient = value;
        //                this.Patient.AddAppointment(this);
        //            }
        //        }
        //    }
        //}
       
        public string[] toCSV()
        {
            string[] values =
            {
                date.ToString(),
                appointmentID.ToString(),
                patient.PatientID,
                doctor.LicenceNumber,
                room.RoomID,
            };
            return values;
        }

        public void fromCSV(string[] values)
        {
            date = DateTime.Parse(values[0]);
            appointmentID = Convert.ToInt32(values[1]);
            patientID = values[2];
            licenceNumber = values[3];
            roomID = values[4];
        }
    }
}