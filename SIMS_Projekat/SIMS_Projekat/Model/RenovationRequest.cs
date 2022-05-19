using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DTOModel
{
    public class RenovationRequest: Serialization.Serializable ,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string requestID{get; set;}
        public DateTime scheduleDateStart { get; set; }
        public DateTime scheduleDateEnd { get; set; }
        public Boolean check { get; set; }
        public RenovationType renovationType { get; set; }
        public string reason { get; set; }
        public string roomsForRenovation { get; set; }

        public string roomForMerge { get; set; }


        public void fromCSV(string[] values)
        {
            requestID = values[0];
            scheduleDateStart = DateTime.Parse(values[1]);
            scheduleDateEnd = DateTime.Parse(values[2]);
            if (values[3].Equals("True")) { check = true; } else { check = false; }
            renovationType = (RenovationType)int.Parse(values[4]);
            reason = values[5];
            roomsForRenovation = values[6];
            roomForMerge = values[7];
        }

        public string[] toCSV()
        {
            string[] values = {
                requestID,
                scheduleDateStart.ToString() ,
                scheduleDateEnd.ToString(),
                check.ToString(),
                ((int)renovationType).ToString(),
                reason,
                roomsForRenovation,
                roomForMerge
            };

            return values;
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
