using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Meeting : Serializable
    {
        public string ID { get; set; }
        public string Topic { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<Account> InvitedStaff { get; set; }
        public string Description { get; set; }

        public void fromCSV(string[] values)
        {
            ID = values[0];
            Topic = values[1];
            StartDateTime = DateTime.Parse(values[2]);
            EndDateTime = DateTime.Parse(values[3]);
            Description = values[4];
            InvitedStaff = new List<Account>();
            string[] staffArray = values[5].Split(",");
            foreach (string arrPart in staffArray)
            {
                Account account = App.accountRepository.GetDoctorAccountByID(arrPart);
                if (account != null)
                    InvitedStaff.Add(account);
            }
        }

        public string[] toCSV()
        {
            List<string> staff = new List<string>();
            if (InvitedStaff != null)
            {
                staff = (from person in InvitedStaff
                         select person.ID).ToList();
            }

            string[] values =
          {
                ID,
                Topic,
                StartDateTime.ToString(),
                EndDateTime.ToString(),
                Description,
                String.Join(",", staff)

            };
            return values;
        }

    }
}
