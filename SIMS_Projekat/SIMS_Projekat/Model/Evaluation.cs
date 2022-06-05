using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Evaluation : Serialization.Serializable
    {
        public int evaluationID { get; set; }
        public bool isFilled { get; set; }
        public int staff { get; set; }
        public int waitingTime { get; set; }
        public int recommendHospital { get; set; }
        public int doctorIsUnderstandable { get; set; }
        public int doctorIsInterested { get; set; }
        public int recommendDoctor { get; set; }
        public string doctorID { get; set; }
        public string patientID { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
        public string comment { get; set; }

        public double averageDoctorRating { get; set; }
        public double averageHospitalRating { get; set; }

        public DateTime evaluationCreated { get; set; }

        public Evaluation()
        {
            this.averageDoctorRating = (recommendDoctor + doctorIsInterested + doctorIsUnderstandable) / 3.0;
            this.averageHospitalRating = (staff + waitingTime + recommendHospital) / 3.0;
        
        }

        public void fromCSV(string[] values)
        {
            patientID = values[0];
            doctorID = values[1];
            comment = values[2];
            staff = Convert.ToInt32(values[3]);
            waitingTime = Convert.ToInt32(values[4]);
            recommendHospital = Convert.ToInt32(values[5]);
            evaluationCreated = DateTime.Parse(values[6]);
            doctorIsUnderstandable = Convert.ToInt32(values[7]);
            doctorIsInterested = Convert.ToInt32(values[8]);
            recommendDoctor = Convert.ToInt32(values[9]);
            evaluationID = Convert.ToInt32(values[10]);
            isFilled = bool.Parse(values[11]);
            averageDoctorRating = Convert.ToDouble(values[12]);
            averageHospitalRating = Convert.ToDouble(values[13]);
            
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
            doctor = App.accountRepository.GetDoctorAccountByID(doctorID) as Doctor;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                patient.ID,
                doctor.ID,
                comment,
                staff.ToString(),
                waitingTime.ToString(),
                recommendHospital.ToString(),
                evaluationCreated.ToString(),
                doctorIsUnderstandable.ToString(),
                doctorIsInterested.ToString(),
                recommendDoctor.ToString(),
                evaluationID.ToString(),
                isFilled.ToString(),
                averageDoctorRating.ToString(),
                averageHospitalRating.ToString()
            };
            return values;
        }
    }
}
