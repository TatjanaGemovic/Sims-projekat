namespace SIMS_Projekat.Model
{
    public class Patient : Person
    {
        public string PatientID { get; set; }
        public string HealthInsuranceID { get; set; }
        public BloodType BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public System.Collections.Generic.List<Allergen> Allergen {get; set;}

        //public System.Collections.Generic.List<Allergen> Allergen
        //{
        //    get
        //    {
        //        if (Allergen == null)
        //            Allergen = new System.Collections.Generic.List<Allergen>();
        //        return Allergen;
        //    }
        //    set
        //    {
        //        RemoveAllAllergen();
        //        if (value != null)
        //        {
        //            foreach (Allergen oAllergen in value)
        //                AddAllergen(oAllergen);
        //        }
        //    }
        //}


        public void AddAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.Allergen == null)
                this.Allergen = new System.Collections.Generic.List<Allergen>();
            if (!this.Allergen.Contains(newAllergen))
                this.Allergen.Add(newAllergen);
        }


        public void RemoveAllergen(Allergen oldAllergen)
        {
            if (oldAllergen == null)
                return;
            if (this.Allergen != null)
                if (this.Allergen.Contains(oldAllergen))
                    this.Allergen.Remove(oldAllergen);
        }


        public void RemoveAllAllergen()
        {
            if (Allergen != null)
                Allergen.Clear();
        }
        public System.Collections.Generic.List<MedicalRecord> medicalRecord;



        public System.Collections.Generic.List<MedicalRecord> MedicalRecord
        {
            get
            {
                if (medicalRecord == null)
                    medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
                return medicalRecord;
            }
            set
            {
                RemoveAllMedicalRecord();
                if (value != null)
                {
                    foreach (MedicalRecord oMedicalRecord in value)
                        AddMedicalRecord(oMedicalRecord);
                }
            }
        }


        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            if (newMedicalRecord == null)
                return;
            if (this.medicalRecord == null)
                this.medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
            if (!this.medicalRecord.Contains(newMedicalRecord))
                this.medicalRecord.Add(newMedicalRecord);
        }


        public void RemoveMedicalRecord(MedicalRecord oldMedicalRecord)
        {
            if (oldMedicalRecord == null)
                return;
            if (this.medicalRecord != null)
                if (this.medicalRecord.Contains(oldMedicalRecord))
                    this.medicalRecord.Remove(oldMedicalRecord);
        }


        public void RemoveAllMedicalRecord()
        {
            if (medicalRecord != null)
                medicalRecord.Clear();
        }
        public System.Collections.Generic.List<Appointment> appointment;



        public System.Collections.Generic.List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.Generic.List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }


        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.Generic.List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.patient = this;
            }
        }


        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                {
                    this.appointment.Remove(oldAppointment);
                    oldAppointment.patient = null;
                }
        }


        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointment)
                    tmpAppointment.Add(oldAppointment);
                appointment.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.patient = null;
                tmpAppointment.Clear();
            }
        }
        public System.Collections.Generic.List<ScheduledOperation> scheduledOperation;



        public System.Collections.Generic.List<ScheduledOperation> ScheduledOperation
        {
            get
            {
                if (scheduledOperation == null)
                    scheduledOperation = new System.Collections.Generic.List<ScheduledOperation>();
                return scheduledOperation;
            }
            set
            {
                RemoveAllScheduledOperation();
                if (value != null)
                {
                    foreach (ScheduledOperation oScheduledOperation in value)
                        AddScheduledOperation(oScheduledOperation);
                }
            }
        }


        public void AddScheduledOperation(ScheduledOperation newScheduledOperation)
        {
            if (newScheduledOperation == null)
                return;
            if (this.scheduledOperation == null)
                this.scheduledOperation = new System.Collections.Generic.List<ScheduledOperation>();
            if (!this.scheduledOperation.Contains(newScheduledOperation))
            {
                this.scheduledOperation.Add(newScheduledOperation);
                newScheduledOperation.Patient = this;
            }
        }


        public void RemoveScheduledOperation(ScheduledOperation oldScheduledOperation)
        {
            if (oldScheduledOperation == null)
                return;
            if (this.scheduledOperation != null)
                if (this.scheduledOperation.Contains(oldScheduledOperation))
                {
                    this.scheduledOperation.Remove(oldScheduledOperation);
                    oldScheduledOperation.Patient = null;
                }
        }


        public void RemoveAllScheduledOperation()
        {
            if (scheduledOperation != null)
            {
                System.Collections.ArrayList tmpScheduledOperation = new System.Collections.ArrayList();
                foreach (ScheduledOperation oldScheduledOperation in scheduledOperation)
                    tmpScheduledOperation.Add(oldScheduledOperation);
                scheduledOperation.Clear();
                foreach (ScheduledOperation oldScheduledOperation in tmpScheduledOperation)
                    oldScheduledOperation.Patient = null;
                tmpScheduledOperation.Clear();
            }
        }

    }
}