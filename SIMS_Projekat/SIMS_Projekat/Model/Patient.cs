using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Projekat.Model
{
    public class Patient : Account
    {
        public string HealthInsuranceID { get; set; }
        public BloodType BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Symptoms { get; set; }
        public bool IsUrgent { get; set; }

        public List<Appointment> appointment;
        public List<Allergen> Allergens { get; set; }
        
        public string doctorLicenceNumber { get; set; } 

        public string MedicalRecordID;
        public MedicalRecord MedicalRecord { get; set; }

        public override string[] toCSV()
        {
            List<string> allergens = new List<string>();
            if (Allergens != null)
            {
                allergens = (from allergen in Allergens
                            select allergen.Name).ToList();
            }
     
            string[] values =
            {
                base.FirstName,
                base.LastName,
                base.DateOfBirth.ToString(),
                base.Jmbg,
                base.PhoneNumber,
                base.Email,
                base.Username,
                base.Password,
                base.ID,
                doctorLicenceNumber,
                HealthInsuranceID,
                ((int)BloodType).ToString(),
                Height.ToString(),
                Weight.ToString(),
                Symptoms.ToString(),
                IsUrgent.ToString(),
                MedicalRecord.ID.ToString(),
                String.Join(",", allergens)       
                
            };
            return values;
        }

        public override void fromCSV(string[] values)
        {
            base.FirstName = values[0];
            base.LastName = values[1];
            base.DateOfBirth = DateTime.Parse(values[2]);
            base.Jmbg = values[3];
            base.PhoneNumber = values[4];
            base.Email = values[5];
            base.Username = values[6];
            base.Password = values[7];
            base.ID = values[8];
            doctorLicenceNumber = values[9];
            HealthInsuranceID = values[10];
            BloodType = (BloodType)int.Parse(values[11]);
            Height = double.Parse(values[12]);
            Weight = double.Parse(values[13]);
            Symptoms = values[14];
            IsUrgent = bool.Parse(values[15]);
            MedicalRecordID = values[16];
            MedicalRecord = App.medRecordRepository.GetMedicalRecordByID(MedicalRecordID);
            Allergens = new List<Allergen>();
            string[] allergensArray = values[17].Split(",");
            foreach(string arrPart in allergensArray)
            {
                Allergen newAllergen = App.AllergenRepository.GetAllergenByName(arrPart);
                if(newAllergen != null)
                    Allergens.Add(newAllergen);
            }  
            
        }




        public void AddAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.Allergens == null)
                this.Allergens = new List<Allergen>();
            if (!this.Allergens.Contains(newAllergen))
                this.Allergens.Add(newAllergen);
        }


        public void RemoveAllergen(Allergen oldAllergen)
        {
            if (oldAllergen == null)
                return;
            if (this.Allergens != null)
                if (this.Allergens.Contains(oldAllergen))
                    this.Allergens.Remove(oldAllergen);
        }


        public void RemoveAllAllergen()
        {
            if (Allergens != null)
                Allergens.Clear();
        }

        public List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new List<Appointment>();
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
                this.appointment = new List<Appointment>();
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
        
    }
}