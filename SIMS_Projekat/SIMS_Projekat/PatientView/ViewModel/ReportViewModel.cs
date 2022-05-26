using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class ReportViewModel : BindableBase
    {
        private string date { get; set; }
        private string time { get; set; }
        private int finishedAppointmentID { get; set; }
        private string anamnesis { get; set; }
        private string treatment { get; set; }
        private string doctorName { get; set; }
        private string receiptID { get; set; }
        private string operationOrExam { get; set; } 
        //private Patient patient { get; set; }
        private int index { get; set; }

        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");

                }
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");

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
        public string Anamnesis
        {
            get { return anamnesis; }
            set
            {
                if (anamnesis != value)
                {
                    anamnesis = value;
                    OnPropertyChanged("Anamnesis");

                }
            }
        }
        public string Treatment
        {
            get { return treatment; }
            set
            {
                if (treatment != value)
                {
                    treatment = value;
                    OnPropertyChanged("Treatment");

                }
            }
        }
        public string DoctorName
        {
            get { return doctorName; }
            set
            {
                if (doctorName != value)
                {
                    doctorName = value;
                    OnPropertyChanged("DoctorName");

                }
            }
        }
        public string ReceiptID
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
        public string OperationOrExam
        {
            get { return operationOrExam; }
            set
            {
                if (operationOrExam != value)
                {
                    operationOrExam = value;
                    OnPropertyChanged("OperationOrExam");

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
    }
}
