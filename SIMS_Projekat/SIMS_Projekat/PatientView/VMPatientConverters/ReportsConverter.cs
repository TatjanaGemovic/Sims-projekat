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
    public class ReportsConverter
    {
        static int id;
        public ReportViewModel ConvertModelToViewModel(FinishedAppointment finishedAppointment)
        {
            ReportViewModel rpViewModel = new ReportViewModel();

            rpViewModel.FinishedAppointmentID = finishedAppointment.finishedAppointmentID;
            rpViewModel.Treatment = finishedAppointment.Treatment;
            rpViewModel.Anamnesis = finishedAppointment.Anamnesis;
            rpViewModel.Index = id;
            rpViewModel.DoctorName = finishedAppointment.doctor.FirstName + " " + finishedAppointment.doctor.LastName;
            rpViewModel.Date = finishedAppointment.beginningDate.Date.ToString("dd.MM.yyyy.");
            rpViewModel.Time = finishedAppointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
            rpViewModel.NoteID = finishedAppointment.patientNoteID.ToString();

            if (finishedAppointment.operation)
                rpViewModel.OperationOrExam = "Operacija";
            else
                rpViewModel.OperationOrExam = "Pregled";

            rpViewModel.ReceiptID = finishedAppointment.ReceiptID;

            id++;
            return rpViewModel;
        }

        public ObservableCollection<ReportViewModel> ConvertCollectionToViewModel(ObservableCollection<FinishedAppointment> finishedAppointments)
        {
            id = 1;
            ObservableCollection<ReportViewModel> vmReports = new ObservableCollection<ReportViewModel>();
            ReportViewModel rpViewModel;
            foreach (FinishedAppointment f in finishedAppointments)
            {
                rpViewModel = ConvertModelToViewModel(f);
                vmReports.Add(rpViewModel);
            }
            return vmReports;
        }

        public FinishedAppointment ConvertViewModelToModel(ReportViewModel reportFromView, Patient patient)
        {
            return App.finishedAppointmentController.AddNoteToAppointment(reportFromView.FinishedAppointmentID, Convert.ToInt32(reportFromView.NoteID));
        }
    }
}
