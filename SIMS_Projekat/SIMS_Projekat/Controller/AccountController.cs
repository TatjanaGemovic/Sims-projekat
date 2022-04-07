using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
    public class AccountController
   {
      public Account CreatePatientAccount(Patient patient, string username, string password)
      {
         throw new NotImplementedException();
      }
      
      public Account DeletePatientAccount(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Account EditPatientAccount(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public List<Account> GetAllPatientAccounts()
      {
         throw new NotImplementedException();
      }
      
      public Account GetPatientAccountByID(string patientID)
      {
         throw new NotImplementedException();
      }
      
      public UrgentPatient CreateUrgentPatientAccount(UrgentPatient urgentPatient)
      {
         throw new NotImplementedException();
      }
      
      public UrgentPatient DeleteUrgentPatientAccount(UrgentPatient urgentPatient)
      {
         throw new NotImplementedException();
      }
      
      public UrgentPatient GetUrgentPatientAccountByID(string urgentPatientID)
      {
         throw new NotImplementedException();
      }
      
      public List<UrgentPatient> GetAllUrgentPatients()
      {
         throw new NotImplementedException();
      }
      
      public AccountService accountService;
   
   }
}