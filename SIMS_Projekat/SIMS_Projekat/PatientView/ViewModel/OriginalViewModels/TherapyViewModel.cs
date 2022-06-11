using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class TherapyViewModel : BindableBase
    {
        private string beginningDate { get; set; }
        private string endDate { get; set; }
        private string therapyInformation { get; set; }
        private string medicineName { get; set; }
        private string doctor { get; set; }
        private string dailyDose { get; set; }
        private int index { get; set; }
        private int finishedAppointmentID { get; set; }
        private int receiptID { get; set; }
        private string noteID { get; set; }

        public string BeginningDate
        {
            get { return beginningDate; }
            set
            {
                if (beginningDate != value)
                {
                    beginningDate = value;
                    OnPropertyChanged("BeginningDate");

                }
            }
        }
        
        public string EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");

                }
            }
        }
        
        public string Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");

                }
            }
        }
        public string TherapyInformation
        {
            get { return therapyInformation; }
            set
            {
                if (therapyInformation != value)
                {
                    therapyInformation = value;
                    OnPropertyChanged("TherapyInformation");

                }
            }
        }   
        public string MedicineName
        {
            get { return medicineName; }
            set
            {
                if (medicineName != value)
                {
                    medicineName = value;
                    OnPropertyChanged("MedicineName");

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
        public string DailyDose
        {
            get { return dailyDose; }
            set
            {
                if (dailyDose != value)
                {
                    dailyDose = value;
                    OnPropertyChanged("DailyDose");

                }
            }
        }
        public int FinishedAppointmentID
        {
            get { return finishedAppointmentID; }
            set
            {
                if (finishedAppointmentID != value)
                {
                    finishedAppointmentID = value;
                    OnPropertyChanged("FinishedAppointmentID");

                }
            }
        }
        public int ReceiptID
        {
            get { return receiptID; }
            set
            {
                if (receiptID != value)
                {
                    receiptID = value;
                    OnPropertyChanged("ReceiptID");

                }
            }
        }
        
        public string NoteID
        {
            get { return noteID; }
            set
            {
                if (noteID != value)
                {
                    noteID = value;
                    OnPropertyChanged("NoteID");

                }
            }
        }

    }
}
