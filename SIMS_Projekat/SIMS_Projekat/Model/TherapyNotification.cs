﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class TherapyNotification : Serialization.Serializable
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public string medicineName { get; set; }
        public string patientID { get; set; }
        public DateTime date { get; set; } 
        public Receipt receipt { get; set; }
        public Patient patient { get; set; }

        public void fromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            receiptID = Convert.ToInt32(values[1]);
            medicineName = values[2];
            patientID = values[3];
            date = DateTime.Parse(values[4]);

            receipt = App.receiptRepository.GetReceiptByID(receiptID);
            patient = App.accountRepository.GetPatientAccountByID(patientID) as Patient;
        }

        public string[] toCSV()
        {
            string[] values =
            {
                ID.ToString(),
                receipt.receiptID.ToString(),
                medicineName,
                patient.ID,
                date.ToString(),
            };
            return values;
        }
    }
}
