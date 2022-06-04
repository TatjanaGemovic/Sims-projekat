using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class ReminderRepository
    {
        private string path { get; set; }
        private Serializer<Reminder> serializer;
        private static List<Reminder> reminderList;
        private int id;

        public List<Reminder> ReminderList
        {
            get
            {
                if (reminderList == null)
                    reminderList = new List<Reminder>();
                return reminderList;
            }
            set
            {
                RemoveAllReminders();
                if (value != null)
                {
                    foreach (Reminder reminder in value)
                        AddReminder(reminder);
                }
            }
        }

        public ReminderRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<Reminder>();
            id = 0;
        }


        public Reminder SetReminder(Reminder reminder)
        {
            int index;
            foreach (Reminder rem in reminderList)
            {
                Reminder reminder1 = reminderList.Find(rem => rem.ID == reminder.ID);
                index = reminderList.IndexOf(reminder1);

                if (reminder1 != null)
                {
                    reminder1.patient = reminder.patient;
                    reminder1.content = reminder.content;
                    reminder1.startTime = reminder.startTime;
                    reminder1.type = reminder.type;
                    return reminder1;
                }

            }
            return null;
        }

        public Reminder GetReminderByID(int reminderID)
        {
            foreach (Reminder reminder in reminderList)
            {
                Reminder reminder1 = reminderList.Find(reminder => reminder.ID == reminderID);

                if (reminder1 != null)
                {
                    return reminder1;
                }
            }
            return null;
        }

        public static List<Reminder> GetRemindersByPatientID(string patientID)
        {
            List<Reminder> reminderListForPatient = new List<Reminder>();
            foreach (Reminder reminder in reminderList)
            {
                if (reminder.patient.ID.Equals(patientID))
                {
                    reminderListForPatient.Add(reminder);
                }
            }
            return reminderListForPatient;
        }

        public List<Reminder> GetAllReminders()
        {
            return reminderList;
        }

        public Reminder AddReminder(Reminder newReminder)
        {
            id++;
            if (newReminder == null)
                return null;
            if (reminderList == null)
                reminderList = new List<Reminder>();
            if (!reminderList.Contains(newReminder))
            {
                newReminder.ID = id;
                reminderList.Add(newReminder);
            }

            return newReminder;
        }
        public Reminder DeleteReminder(Reminder oldReminder)
        {
            if (oldReminder == null)
                return null;
            if (reminderList != null)
                if (reminderList.Contains(oldReminder))
                    reminderList.Remove(oldReminder);
            return oldReminder;
        }

        public void RemoveAllReminders()
        {
            if (reminderList != null)
                reminderList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, reminderList);
        }

        public void Deserialize()
        {
            reminderList = serializer.fromCSV(path);

            foreach (Reminder reminder in reminderList)
            {
                id = reminder.ID;
            }
        }
    }
}
