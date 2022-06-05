using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class ReminderViewModel : BindableBase
    {
        private string date { get; set; }
        private string time { get; set; }
        private string reminderID { get; set; }
        private string content { get; set; }
        private string isRepeatable { get; set; }
        private int index { get; set; }

        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");

                }
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");

                }
            }
        }

        public string IsRepeatable
        {
            get { return isRepeatable; }
            set
            {
                if (isRepeatable != value)
                {
                    isRepeatable = value;
                    OnPropertyChanged("IsRepeatable");

                }
            }
        }
        public string ReminderID
        {
            get { return reminderID; }
            set
            {
                if (reminderID != value)
                {
                    reminderID = value;
                    OnPropertyChanged("ReminderID");

                }
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");

                }
            }
        }
        
        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnPropertyChanged("Index");

                }
            }
        }
    }
}
