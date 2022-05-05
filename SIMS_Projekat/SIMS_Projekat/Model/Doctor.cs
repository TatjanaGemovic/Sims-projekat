using System;

namespace SIMS_Projekat.Model
{
    public class Doctor : Account
    {
        public string LicenceNumber { get; set; }
        
        public override string[] toCSV()
        {
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
                LicenceNumber
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
            LicenceNumber = values[9];
        }

    }
}