using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class MedicalRecordRepository
    {
        private string medRecordFile;
        private int ID;
        public List<MedicalRecord> Records { get; set; }
        private Serializer<MedicalRecord> recordsSerializer;

        public MedicalRecordRepository(string medRecordFileName)
        {
            medRecordFile = medRecordFileName;
            Records = new List<MedicalRecord>();
            ID = 0;
            recordsSerializer = new Serializer<MedicalRecord>();
        }

        public void Serialize()
        {
            recordsSerializer.toCSV(medRecordFile, Records);
        }

        public void Deserialize()
        {
            Records = recordsSerializer.fromCSV(medRecordFile);

            int maxID = 0;
            foreach (MedicalRecord r in Records)
            {
                if (int.Parse(r.ID) > maxID)
                    maxID = int.Parse(r.ID);
            }
            ID = ++maxID;
        }

        public MedicalRecord CreateMedicalRecord(MedicalRecord medRec)
        {
            medRec.ID = ID++.ToString();
            Records.Add(medRec);
            return medRec;
        }

        public MedicalRecord GetMedicalRecordByID(string id)
        {
            foreach(MedicalRecord r in Records)
            {
                if (r.ID.Equals(id))
                {
                    return r;
                }
            }
            return null;
        }

        public void RemoveMedicalRecord(MedicalRecord medicalRecord)
        {
            Records.Remove(medicalRecord);
        }
    }
}
