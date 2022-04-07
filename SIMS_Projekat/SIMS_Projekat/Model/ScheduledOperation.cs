using System;

namespace SIMS_Projekat.Model
{
    public class ScheduledOperation : Serialization.Serializable
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string OperationType { get; set; }
        public int OperationID { get; set; }

        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public Patient Patient
        {
            get
            {
                return Patient;
            }
            set
            {
                if (this.Patient == null || !this.Patient.Equals(value))
                {
                    if (this.Patient != null)
                    {
                        Patient oldPatient = this.Patient;
                        this.Patient = null;
                        oldPatient.RemoveScheduledOperation(this);
                    }
                    if (value != null)
                    {
                        this.Patient = value;
                        this.Patient.AddScheduledOperation(this);
                    }
                }
            }
        }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }
    }
}