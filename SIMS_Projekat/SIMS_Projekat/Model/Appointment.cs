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

        public bool isDelayed { get; set; }

        public bool isScheduledByPatient { get; set; }

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
                isDelayed.ToString(),
                isScheduledByPatient.ToString()
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
            string delayed = values[7];
            if (delayed.Equals("False"))
            {
                isDelayed = false;
            }
            else
            {
                isDelayed = true;
            }
            string byPatient = values[8];
            if (byPatient.Equals("False"))
            {
                isScheduledByPatient = false;
            }
            else
            {
                isScheduledByPatient = true;
            }
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
            doctor = App.accountRepository.GetDoctorAccountByLicenceNumber(licenceNumber) as Doctor;
            room = App.roomController.GetRoomByID(roomID);
        }
    }
}