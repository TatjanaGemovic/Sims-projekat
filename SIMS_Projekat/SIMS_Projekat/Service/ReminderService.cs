using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class ReminderService
    {
        public ReminderRepository reminderRepository;
        public Reminder SetReminder(Reminder reminder)
        {
            return reminderRepository.SetReminder(reminder);
        }

        public Reminder DeleteReminder(Reminder reminder)
        {
            return reminderRepository.DeleteReminder(reminder);
        }

        public Reminder AddReminder(Reminder reminder)
        {
            return reminderRepository.AddReminder(reminder);
        }

        public Reminder GetReminderByID(int reminderID)
        {
            return reminderRepository.GetReminderByID(reminderID);
        }

        public List<Reminder> GetRemindersByPatientID(string patientID)     //bilo je static u repository
        {
            return reminderRepository.GetRemindersByPatientID(patientID);
        }

        public List<Reminder> GetAllReminders()
        {
            return reminderRepository.GetAllReminders();
        }
    }
}
