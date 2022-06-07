using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.VMPatientConverters
{
    public class TherapyConverter
    {
        static int id;
        public TherapyViewModel ConvertModelToViewModel(Receipt receipt)
        {
            TherapyViewModel therapyViewModel = new TherapyViewModel();

            therapyViewModel.TherapyInformation = receipt.Record;
            therapyViewModel.DailyDose = receipt.DailyMed;
            therapyViewModel.MedicineName = receipt.medicine.MedicineName;
            therapyViewModel.Index = id;
            therapyViewModel.BeginningDate = receipt.beginningDate.Date.ToString("dd.MM.yyyy.");
            therapyViewModel.EndDate = receipt.endDate.Date.ToString("dd.MM.yyyy.");
            therapyViewModel.FinishedAppointmentID = receipt.appointmentID;
            therapyViewModel.ReceiptID = receipt.receiptID;
            therapyViewModel.NoteID = receipt.patientNoteID.ToString();

            FinishedAppointment appointment = App.finishedAppointmentController.GetAppointmentByID(receipt.appointmentID);
            therapyViewModel.Doctor = appointment.doctor.FirstName + " " + appointment.doctor.LastName;

            id++;
            return therapyViewModel;
        }

        public ObservableCollection<TherapyViewModel> ConvertCollectionToViewModel(ObservableCollection<Receipt> receipts)
        {
            id = 1;
            ObservableCollection<TherapyViewModel> vmTherapies = new ObservableCollection<TherapyViewModel>();
            TherapyViewModel therapyViewModel;
            foreach (Receipt r in receipts)
            {
                therapyViewModel = ConvertModelToViewModel(r);
                vmTherapies.Add(therapyViewModel);
            }
            return vmTherapies;
        }

        public Reminder ConvertViewModelToModel(TherapyViewModel therapyViewModel, Patient patient)
        {
            //DateTime start = ConvertFromStringToDate(reminderFromView);
            //Reminder reminder = new Reminder()
            //{
            //    content = reminderFromView.Content,
            //    startTime = start,
            //    patient = patient,
            //    type = "Podsetnik: ",
            //    isRepeatable = reminderFromView.IsRepeatable,
            //    ID = Convert.ToInt32(reminderFromView.ReminderID)
            //};
            //return reminder;
            return null;
        }

        public DateTime ConvertFromStringToDate(ReminderViewModel reminderFromView)
        {
            string[] dateParts = reminderFromView.Date.Split(".");
            string Date = dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
            DateTime start = DateTime.Parse(Date);
            DateTime startDate = start.Date;

            TimeSpan timeStart = TimeSpan.Parse(reminderFromView.Time);
            startDate = startDate.Add(timeStart);
            return startDate;
        }
    }
}
