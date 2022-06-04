using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class ReminderController
    {
        public ReminderService reminderService;
        public Reminder SetReminder(Reminder reminder)
        {
            return reminderService.SetReminder(reminder);
        }

        public Reminder DeleteReminder(Reminder reminder)
        {
            return reminderService.DeleteReminder(reminder);
        }

        public Reminder AddReminder(Reminder reminder)
        {
            return reminderService.AddReminder(reminder);
        }

        public Reminder GetReminderByID(int reminderID)
        {
            return reminderService.GetReminderByID(reminderID);
        }

        public List<Reminder> GetRemindersByPatientID(string patientID)
        {
            return reminderService.GetRemindersByPatientID(patientID);
        }

        public List<Reminder> GetAllReminders()
        {
            return reminderService.GetAllReminders();
        }
    }
}
