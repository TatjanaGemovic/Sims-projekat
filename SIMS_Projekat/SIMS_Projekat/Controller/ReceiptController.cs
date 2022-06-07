using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class ReceiptController
    {
        public ReceiptService receiptService;
        public Receipt SetReceipt(Receipt receipt)
        {
            return receiptService.SetReceipt(receipt);
        }

        public Receipt DeleteReceipt(Receipt receipt)
        {
            return receiptService.DeleteReceipt(receipt);
        }

        public Receipt AddReceipt(Receipt receipt)
        {
            return receiptService.AddReceipt(receipt);
        }

        public Receipt GetReceiptByID(int receiptID)
        {
            return receiptService.GetReceiptByID(receiptID);
        }

        public List<Receipt> GetReceiptByPatientID(string patientID)
        {
            return receiptService.GetReceiptByPatientID(patientID);
        }

        public List<Receipt> GetAllReceipts()
        {
            return receiptService.GetAllReceipts();
        }  
        public List<Receipt> GetActiveReceiptByPatientID(string patientID)
        {
            return receiptService.GetActiveReceiptByPatientID(patientID);
        }
    }
}
