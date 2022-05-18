using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class FreeDayRequest : Serialization.Serializable
    {
        public DateTime from { get; set; }
        public DateTime until { get; set; }
        public DateTime sentDate { get; set; }
        public Doctor doctor { get; set; }
        public string licenceNumber { get; set; }
        public Status status { get; set; }
        public string secretaryComment { get; set; }

        public bool isUrgent { get; set; }
        public void fromCSV(string[] values)
        {
            from = DateTime.Parse(values[0]);
            until = DateTime.Parse(values[1]);
            sentDate = DateTime.Parse(values[2]);
            licenceNumber = values[3];
            string status1 = values[4];
            if (status1.Equals("Waiting"))
            {
                status = Status.Waiting;
            }
            else if(status1.Equals("Accepted"))
            {
                status = Status.Accepted;
            }
            else
            {
                status = Status.Denied;
            }

            secretaryComment = values[5];

            string urgent = values[6];
            if (urgent.Equals("False"))
            {
                isUrgent = false;
            }
            else
            {
                isUrgent = true;
            }

            doctor = App.accountRepository.GetDoctorAccountByLicenceNumber(licenceNumber) as Doctor;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                from.ToString(),
                until.ToString(),
                sentDate.ToString(),
                doctor.LicenceNumber,
                status.ToString(),
                secretaryComment.ToString(),
                isUrgent.ToString(),
            };
            return values;
        }
    }
}
