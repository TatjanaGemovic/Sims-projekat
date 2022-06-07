using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class ReceiptRepository
    {
        private string path { get; set; }
        private Serializer<Receipt> serializer;
        private List<Receipt> receiptList;
        private int id;

        public ReceiptRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<Receipt>();
            id = 0;
        }
        public Receipt GetReceiptByID(int receiptID)
        {
            foreach (Receipt receipt in receiptList)
            {
                Receipt receipt1 = receiptList.Find(receipt => receipt.receiptID == receiptID);

                if (receipt1 != null)
                {
                    return receipt1;
                }
            }
            return null;

        }
        public Receipt SetReceipt(Receipt receipt)
        {
            int index;
            foreach (Receipt r in receiptList)
            {
                Receipt receipt1 = receiptList.Find(r => r.receiptID == receipt.receiptID);
                index = receiptList.IndexOf(receipt1);

                if (receipt1 != null)
                {
                    receipt1.patient = receipt.patient;
                    receipt1.beginningDate = receipt.beginningDate;
                    receipt1.endDate = receipt.endDate;
                    receipt1.appointmentID = receipt.appointmentID;
                    receipt1.DailyMed = receipt.DailyMed;
                    receipt1.Record = receipt.Record;
                    receipt1.medicine = receipt.medicine;

                    return receipt1;
                }

            }
            return null;
        }
        public List<Receipt> GetReceiptByPatientID(string patientID1)
        {
            List<Receipt> receiptListForPatient = new List<Receipt>();
            foreach (Receipt receipt in receiptList)
            {
                if (receipt.patient.ID.Equals(patientID1))
                {
                    receiptListForPatient.Add(receipt);
                }
            }
            return receiptListForPatient;
        }

        public Receipt AddReceipt(Receipt newReceipt)
        {
            id++;
            if (newReceipt == null)
                return null;
            if (this.receiptList == null)
                this.receiptList = new List<Receipt>();
            if (!this.receiptList.Contains(newReceipt))
            {
                newReceipt.receiptID = id;
                this.receiptList.Add(newReceipt);
            }

            return newReceipt;
        }
        public List<Receipt> GetAllReceipts()
        {
            return receiptList;
        }

        public Receipt DeleteReceipt(Receipt oldReceipt)
        {
            if (oldReceipt == null)
                return null;
            if (receiptList != null)
                if (receiptList.Contains(oldReceipt))
                    receiptList.Remove(oldReceipt);
            return oldReceipt;
        }

        public void RemoveAllReceipts()
        {
            if (receiptList != null)
                receiptList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, receiptList);
        }

        public void Deserialize()
        {
            receiptList = serializer.fromCSV(path);

            foreach (Receipt receipt in receiptList)
            {
                id = receipt.receiptID;
            }
        }
    }
}
