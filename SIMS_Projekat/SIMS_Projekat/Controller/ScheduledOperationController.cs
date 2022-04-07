using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
   public class ScheduledOperationController
   {
      public Model.ScheduledOperation ScheduleOperation(Model.ScheduledOperation scheduledOperation)
      {
         throw new NotImplementedException();
      }
      
      public Model.ScheduledOperation CancelOperation(Model.ScheduledOperation scheduledOperation)
      {
         throw new NotImplementedException();
      }
      
      public Model.ScheduledOperation GetByID(string id)
      {
         throw new NotImplementedException();
      }
      
      public List<ScheduledOperation> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public List<ScheduledOperation> GetAllByDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }
      
      public List<ScheduledOperation> GetAllByPatient(Model.Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Model.ScheduledOperation Edit(Model.ScheduledOperation scheduledOperation)
      {
         throw new NotImplementedException();
      }
      
      public ScheduledOperationService scheduledOperationService;
   
   }
}