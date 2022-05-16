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
        public TherapyNotification CreateNotification(Receipt receipt)
        {
            int dailyDose;
            foreach (DateTime day in EachDay(receipt.beginningDate, receipt.endDate))
            {
                if(receipt.DailyMed == 1)
                {
                    DateTime date = day.Date;
                    TimeSpan timeStart = TimeSpan.Parse("12:00");
                    date = date.Add(timeStart);
                    TherapyNotification notification = new TherapyNotification()
                    {
                        date = date,
                        receipt = receipt,
                        patient = receipt.patient,       
                    };
                    AddNotification(notification);
                }
                else if(receipt.DailyMed == 2)
                {
                    DateTime firstDate = day.Date;
                    TimeSpan timeStart = TimeSpan.Parse("00:00");
                    firstDate = firstDate.Add(timeStart);
                    TherapyNotification notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("12:00");
                    firstDate = firstDate.Add(timeStart);
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);
                }
                else if(receipt.DailyMed == 3)
                {
                    DateTime firstDate = day.Date;
                    TimeSpan timeStart = TimeSpan.Parse("00:00");
                    firstDate = firstDate.Add(timeStart);
                    TherapyNotification notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("08:00");
                    firstDate = firstDate.Add(timeStart);
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("16:00");
                    firstDate = firstDate.Add(timeStart);
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);
                }
                else
                {
                    DateTime firstDate = day.Date;
                    TimeSpan timeStart = TimeSpan.Parse("00:00");
                    firstDate = firstDate.Add(timeStart);
                    TherapyNotification notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("06:00");
                    firstDate = firstDate.Add(timeStart);
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("12:00");
                    firstDate = firstDate.Add(timeStart);
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                    firstDate = day.Date;
                    timeStart = TimeSpan.Parse("18:00");
                    firstDate = firstDate.Add(timeStart);
                    
                    notification = new TherapyNotification()
                    {
                        date = firstDate,
                        receipt = receipt,
                        patient = receipt.patient,
                    };
                    AddNotification(notification);

                }
            }
            return null;
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
