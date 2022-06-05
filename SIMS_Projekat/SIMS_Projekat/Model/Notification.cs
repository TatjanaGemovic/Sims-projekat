using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Notification : Serializable
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead{ get; set; }
        public string RecipientID { get; set; }

        public Notification()
        {
            SentDate = DateTime.Today;
            IsRead = false;
        }

        public void fromCSV(string[] values)
        {
            ID = values[0];
            Content = values[1];
            SentDate = DateTime.Parse(values[2]);
            IsRead = bool.Parse(values[3]);
            RecipientID = values[4];
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID,
                Content,
                SentDate.ToString(),
                IsRead.ToString(),
                RecipientID
            };

            return values;

        }
    }
}
