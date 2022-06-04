using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class ReminderService
    {
        public ReminderRepository reminderRepository;
        private static ObservableCollection<Reminder> RemindersForPatient;

        public ReminderService()
        {
            RemindersForPatient = new ObservableCollection<Reminder>();
        }
        public Reminder SetReminder(Reminder reminder)
        {
            return reminderRepository.SetReminder(reminder);
        }

        public Reminder DeleteReminder(Reminder reminder)
        {
            RemindersForPatient.Remove(reminder);
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

        public static List<Reminder> GetRemindersByPatientID(string patientID)     
        {
            return ReminderRepository.GetRemindersByPatientID(patientID);
        }

        public List<Reminder> GetAllReminders()
        {
            return reminderRepository.GetAllReminders();
        }

        public static void TickTimer(object state)
        {
            foreach (Reminder reminder in GetRemindersByPatientID(LoginWindow.patient.ID))
            {
                if (DateTime.Now >= reminder.startTime)
                {
                    if (RemindersForPatient != null)
                    {
                        if (!RemindersForPatient.Contains(reminder))
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                RemindersForPatient.Add(reminder);
                            });

                        }
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            RemindersForPatient.Add(reminder);
                        });
                    }

                }
            }
        }
        public ObservableCollection<Reminder> GetActiveReminders()
        {
            return RemindersForPatient;
        }

        public void DeleteActiveNotifications()
        {
            RemindersForPatient.Clear();
        }

        public void CreateNotificationForTherapy(Receipt receipt)
        {
            int dailyDose;
            foreach (DateTime day in EachDay(receipt.beginningDate, receipt.endDate))
            {
                DateTime date = day.Date;
                TimeSpan timeStart = TimeSpan.Parse("00:00");
                date = date.Add(timeStart);

                for (dailyDose = 0; dailyDose < receipt.DailyMed; dailyDose++)
                {
                    if (receipt.DailyMed == 1)
                        date = date.AddHours(12);

                    Reminder reminder = new Reminder()
                    {
                        startTime = date,
                        type = "Uzmite terapiju: ",
                        content = receipt.medicine.MedicineName,
                        patient = receipt.patient,
                        isRepeatable = "Nikada"
                    };
                    AddReminder(reminder);

                    if (receipt.DailyMed == 2)
                        date = date.AddHours(12);
                    else if (receipt.DailyMed == 3)
                        date = date.AddHours(8);
                    else
                        date = date.AddHours(6);
                }
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public List<Reminder> GetRemindersByTypeAndPatient(string type, string patientID)
        {
            List<Reminder> allPatientReminders = ReminderRepository.GetRemindersByPatientID(patientID);
            List<Reminder> neededReminders = new List<Reminder>();
            foreach(Reminder r in allPatientReminders)
            {
                if (r.type.Equals(type))
                    neededReminders.Add(r);
            }
            return neededReminders;
        }
        public Reminder CreateNewReminderIfItIsRepeatableAndDeleteIfNot(Reminder reminder)
        {
            RemindersForPatient.Remove(reminder);
            if (reminder.isRepeatable.Equals("Nikada"))
                DeleteReminder(reminder);
            else if (reminder.isRepeatable.Equals("Svaki dan"))
                reminder.startTime = reminder.startTime.AddDays(1);
            else if (reminder.isRepeatable.Equals("Nedeljno"))
                reminder.startTime = reminder.startTime.AddDays(7);
            else if (reminder.isRepeatable.Equals("Mesečno"))
                reminder.startTime = reminder.startTime.AddMonths(1);

            return reminder;
        }
    }
}
