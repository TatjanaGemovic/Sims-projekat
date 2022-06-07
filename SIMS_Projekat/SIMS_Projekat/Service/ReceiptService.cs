using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class ReceiptService
    {
        public ReceiptRepository receiptRepository;
        public Receipt SetReceipt(Receipt receipt)
        {
            return receiptRepository.SetReceipt(receipt);
        }

        public Receipt DeleteReceipt(Receipt receipt)
        {
            return receiptRepository.DeleteReceipt(receipt);
        }

        public Receipt AddReceipt(Receipt receipt)
        {
            return receiptRepository.AddReceipt(receipt);
        }

        public Receipt GetReceiptByID(int receiptID)
        {
            return receiptRepository.GetReceiptByID(receiptID);
        }

        public List<Receipt> GetReceiptByPatientID(string patientID)
        {
            return receiptRepository.GetReceiptByPatientID(patientID);
        }

        public List<Receipt> GetAllReceipts()
        {
            return receiptRepository.GetAllReceipts();
        }
        public List<Receipt> GetActiveReceiptByPatientID(string patientID)
        {
            List<Receipt> allPatientReceipts = GetReceiptByPatientID(patientID);
            List<Receipt> activeReceipts = new List<Receipt>();
            foreach (Receipt r in allPatientReceipts)
            {
                if (r.endDate >= DateTime.Now)
                    activeReceipts.Add(r);
            }
            return activeReceipts;
        }
    }
}
