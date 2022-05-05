using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class TherapyNotification : Serialization.Serializable//, INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public string patientID { get; set; }
        public DateTime date { get; set; } 
        public Receipt receipt { get; set; }
        public Patient patient { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(String propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //public int IDD
        //{

        //    get { return ID; }
        //    set
        //    {
        //        ID = value;
        //        OnPropertyChanged(nameof(IDD));
        //    }
        //}

        //public int ReceiptID
        //{

        //    get { return receiptID; }
        //    set
        //    {
        //        receiptID = value;
        //        OnPropertyChanged(nameof(ReceiptID));
        //    }
        //}

        //public string PatientID
        //{

        //    get { return patientID; }
        //    set
        //    {
        //        patientID = value;
        //        OnPropertyChanged(nameof(PatientID));
        //    }
        //}

        //public DateTime Date
        //{

        //    get { return date; }
        //    set
        //    {
        //        date = value;
        //        OnPropertyChanged(nameof(Date));
        //    }
        //}

        //public Receipt Receipt
        //{

        //    get { return receipt; }
        //    set
        //    {
        //        receipt = value;
        //        OnPropertyChanged(nameof(Receipt));
        //    }
        //}

        //public Patient Patient
        //{

        //    get { return patient; }
        //    set
        //    {
        //        patient = value;
        //        OnPropertyChanged(nameof(Patient));
        //    }
        //}

        
        public void fromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            receiptID = Convert.ToInt32(values[1]);
            patientID = values[2];
            date = DateTime.Parse(values[3]);

            receipt = App.receiptRepository.GetReceiptByID(receiptID);
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID.ToString(),
                receipt.receiptID.ToString(),
                patient.ID,
                date.ToString(),
            };
            return values;
        }
    }
}
