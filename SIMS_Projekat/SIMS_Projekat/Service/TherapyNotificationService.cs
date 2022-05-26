using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SIMS_Projekat.PatientView;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SIMS_Projekat.Service
{
    public class TherapyNotificationService
    {
        public TherapyNotificationRepository therapyNotificationRepository;

        private static ObservableCollection<TherapyNotification> TherapyNotificationsForPatient;

        public TherapyNotificationService()
        {
            TherapyNotificationsForPatient = new ObservableCollection<TherapyNotification>();
        }
        public TherapyNotification SetNotification(TherapyNotification notification)
        {
            return therapyNotificationRepository.SetNotification(notification);
        }

        public TherapyNotification GetNotificationByID(int notificationtID)
        {
            return therapyNotificationRepository.GetNotificationByID(notificationtID);
        }

        public static List<TherapyNotification> GetNotificationByPatientID(string patientID)
        {
            return TherapyNotificationRepository.GetNotificationByPatientID(patientID);
        }

        public List<TherapyNotification> GetAllNotifications()
        {
            return therapyNotificationRepository.GetAllNotifications();
        }

        public TherapyNotification AddNotification(TherapyNotification newNotification)
        {
            return therapyNotificationRepository.AddNotification(newNotification);
        }
        public TherapyNotification DeleteNotification(TherapyNotification oldNotification)
        {
            TherapyNotificationsForPatient.Remove(oldNotification);
            return therapyNotificationRepository.DeleteNotification(oldNotification);
        }
        public void CreateNotification(Receipt receipt)
        {
            int dailyDose;
            foreach (DateTime day in EachDay(receipt.beginningDate, receipt.endDate))
            {
                DateTime date = day.Date;
                TimeSpan timeStart = TimeSpan.Parse("00:00");
                date = date.Add(timeStart);

                for (dailyDose = 0; dailyDose < receipt.DailyMed; dailyDose++)
                {
                    if(receipt.DailyMed == 1)
                        date = date.AddHours(12);
                    
                    TherapyNotification notification = new TherapyNotification()
                    {
                        date = date,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    if (receipt.DailyMed == 2)
                        date = date.AddHours(12);
                    else if(receipt.DailyMed == 3)
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
        public static void TickTimer(object state)
        {
            foreach(TherapyNotification notification in GetNotificationByPatientID(LoginWindow.patient.ID))
            {
                if(DateTime.Now >= notification.date)
                {
                    if(TherapyNotificationsForPatient != null)
                    {
                        if (!TherapyNotificationsForPatient.Contains(notification))
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                TherapyNotificationsForPatient.Add(notification);
                            });
                            
                        }
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            TherapyNotificationsForPatient.Add(notification);
                        });
                    }
                   
                }
            }
        }
        public ObservableCollection<TherapyNotification> GetActiveNotifications()
        {
            return TherapyNotificationsForPatient;
        }

        public void DeleteActiveNotifications()
        {
            TherapyNotificationsForPatient.Clear();
        }

    }
}
