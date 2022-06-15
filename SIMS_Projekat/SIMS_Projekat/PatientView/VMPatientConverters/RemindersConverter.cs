﻿using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.VMPatientConverters
{
    public class RemindersConverter
    {
        static int id;
        public ReminderViewModel ConvertModelToViewModel(Reminder reminder)
        {
            ReminderViewModel reminderViewModel = new ReminderViewModel();

            reminderViewModel.Content = reminder.content;
            reminderViewModel.Index = id;
            reminderViewModel.Date = reminder.startTime.Date.ToString("dd.MM.yyyy.");
            reminderViewModel.Time = reminder.startTime.TimeOfDay.ToString(@"hh\:mm");
            reminderViewModel.ReminderID = reminder.ID.ToString();
            reminderViewModel.IsRepeatable = reminder.isRepeatable;

            id++;
            return reminderViewModel;
        }

        public ObservableCollection<ReminderViewModel> ConvertCollectionToViewModel(ObservableCollection<Reminder> reminders)
        {
            id = 1;
            ObservableCollection<ReminderViewModel> vmReminders = new ObservableCollection<ReminderViewModel>();
            ReminderViewModel reminderViewModel;
            foreach (Reminder r in reminders)
            {
                reminderViewModel = ConvertModelToViewModel(r);
                vmReminders.Add(reminderViewModel);
            }
            return vmReminders;
        }

        public Reminder ConvertViewModelToModel(ReminderViewModel reminderFromView, Patient patient, bool creation)
        {
            DateTime start = ConvertFromStringToDate(reminderFromView, creation);
            Reminder reminder = new Reminder()
            {
                content = reminderFromView.Content,
                startTime = start,
                patient = patient,
                type = "Podsetnik: ",
                isRepeatable = reminderFromView.IsRepeatable,
                ID = Convert.ToInt32(reminderFromView.ReminderID)
            };
            return reminder;
        }

        public DateTime ConvertFromStringToDate(ReminderViewModel reminderFromView, bool creation)
        {
            string Date;
            if (!creation)
            {
                string[] dateParts = reminderFromView.Date.Split(".");
                Date = dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
            }
            else
            {
                Date = reminderFromView.Date;
            }
            
            DateTime start = DateTime.Parse(Date);
            DateTime startDate = start.Date;

            TimeSpan timeStart = TimeSpan.Parse(reminderFromView.Time);
            startDate = startDate.Add(timeStart);
            return startDate;
        }
    }
}
