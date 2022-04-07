using System;

namespace SIMS_Projekat.Model
{
    public class Person : Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Jmbg { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}