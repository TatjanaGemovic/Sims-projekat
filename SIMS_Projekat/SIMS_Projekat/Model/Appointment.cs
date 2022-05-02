using System;

namespace SIMS_Projekat.Model
{
    public class Appointment : Serialization.Serializable
    {
        public DateTime beginningDate { get; set; }

        public DateTime endDate { get; set; }
        public int appointmentID { get; set;}
        public Doctor doctor { get; set; }
        public Room room { get; set; }

        public string roomID { get; set; }

        public string licenceNumber { get; set; }
        public string patientID { get; set; }

        public bool operation { get; set; } //true jeste operacija

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
                beginningDate.ToString(),
                endDate.ToString(),
                appointmentID.ToString(),
                patient.ID,
                doctor.LicenceNumber,
                room.RoomID,
                operation.ToString(),
            };
            return values;
        }

        public void fromCSV(string[] values)
        {
            beginningDate = DateTime.Parse(values[0]);
            endDate = DateTime.Parse(values[1]);
            appointmentID = Convert.ToInt32(values[2]);
            patientID = values[3];
            licenceNumber = values[4];
            roomID = values[5];
            string operation1 = values[6];
            if (operation1.Equals("False"))
            {
                operation = false;
            }
            else
            {
                operation = true;
            }
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
            doctor = App.accountRepository.GetDoctorAccountByLicenceNumber(licenceNumber) as Doctor;
            room = App.roomController.GetRoomByID(roomID);
        }
    }
}