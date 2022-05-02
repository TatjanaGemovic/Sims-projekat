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
