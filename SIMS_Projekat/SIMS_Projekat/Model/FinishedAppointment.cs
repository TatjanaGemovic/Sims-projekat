using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class FinishedAppointment : Serialization.Serializable
    {
        public DateTime beginningDate { get; set; }
        public DateTime endDate { get; set; }
        public int finishedAppointmentID { get; set; }
        public string Anamnesis { get; set; }
        public string Treatment { get; set; }
        public Doctor doctor { get; set; }
        public string ReceiptID { get; set; }
        public bool operation { get; set; } //true jeste operacija
        public Patient patient { get; set; }
        public string patientID { get; set; }
        public string licenceNumber { get; set; }

        public void fromCSV(string[] values)
        {
            beginningDate = DateTime.Parse(values[0]);
            endDate = DateTime.Parse(values[1]);
            finishedAppointmentID = Convert.ToInt32(values[2]);
            patientID = values[3];
            licenceNumber = values[4];
            string operation1 = values[5];
            if (operation1.Equals("False"))
            {
                operation = false;
            }
            else
            {
                operation = true;
            }
            Anamnesis = values[6];
            Treatment = values[7];
            //ReceiptID = values[8];
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
            doctor = App.accountRepository.GetDoctorAccountByLicenceNumber(licenceNumber) as Doctor;
        }

        public string[] toCSV()
        {
            string[] values =
           {
                beginningDate.ToString(),
                endDate.ToString(),
                finishedAppointmentID.ToString(),
                patient.ID,
                doctor.LicenceNumber,
                operation.ToString(),
                Anamnesis.ToString(),
                Treatment.ToString(),
                //Receipt.ID.ToString(),
            };
            return values;
        }
    }
}
