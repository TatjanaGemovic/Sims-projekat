using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
    public class AccountController
   {
      public Account CreatePatientAccount(Model.Patient patient, string username, string password)
      {
         throw new NotImplementedException();
      }
      
      public Account DeletePatientAccount(Model.Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Account EditPatientAccount(Model.Patient patient)
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
      
      public Model.UrgentPatient CreateUrgentPatientAccount(Model.UrgentPatient urgentPatient)
      {
         throw new NotImplementedException();
      }
      
      public Model.UrgentPatient DeleteUrgentPatientAccount(Model.UrgentPatient urgentPatient)
      {
         throw new NotImplementedException();
      }
      
      public Model.UrgentPatient GetUrgentPatientAccountByID(string urgentPatientID)
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