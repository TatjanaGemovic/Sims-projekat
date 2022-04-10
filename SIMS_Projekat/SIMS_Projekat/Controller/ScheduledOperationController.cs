using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Controller
{
   public class ScheduledOperationController
   { 
        
      public ScheduledOperationService scheduledOperationService;

        public ScheduledOperationController(ScheduledOperationService scheduledOperationService)
        {
            this.scheduledOperationService = scheduledOperationService;
        }

        public Model.ScheduledOperation ScheduleOperation(Model.ScheduledOperation scheduledOperation)
      {
            scheduledOperationService.ScheduleOperation(scheduledOperation);
            return scheduledOperation;
      }
      
      public Model.ScheduledOperation CancelOperation(Model.ScheduledOperation scheduledOperation)
      {
            scheduledOperationService.CancelOperation(scheduledOperation);
            return scheduledOperation;
        }
      
      public Model.ScheduledOperation GetByID(string id)
      {
            return scheduledOperationService.GetByID(id);
      }

        public ObservableCollection<ScheduledOperation> GetAll()
      {
            return scheduledOperationService.GetAll();
      }
      
      public ObservableCollection<ScheduledOperation> GetAllByDoctor(Model.Doctor doctor)
      {
            return scheduledOperationService.GetAllByDoctor(doctor);
      }
      
      public ObservableCollection<ScheduledOperation> GetAllByPatient(Model.Patient patient)
      {
            return scheduledOperationService.GetAllByPatient(patient);
      }
      
      public Model.ScheduledOperation Edit(Model.ScheduledOperation scheduledOperation)
      {
            return scheduledOperationService.Edit(scheduledOperation);
      }

   }
}