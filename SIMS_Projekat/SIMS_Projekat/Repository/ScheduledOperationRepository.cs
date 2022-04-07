using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
   public class ScheduledOperationRepository
   {
      
      public Model.ScheduledOperation CancelScheduledOperation(Model.ScheduledOperation scheduledOperation)
      {
         throw new NotImplementedException();
      }
      
      public Model.ScheduledOperation GetByScheduledOperationByID(int operationID)
      {
         throw new NotImplementedException();
      }
      
      public List<ScheduledOperation> GetScheduledOperations()
      {
         throw new NotImplementedException();
      }
      
      public Model.ScheduledOperation EditScheduledOperation(Model.ScheduledOperation scheduledOperation)
      {
         throw new NotImplementedException();
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
               foreach (Model.ScheduledOperation oScheduledOperation in value)
                  AddScheduledOperation(oScheduledOperation);
            }
         }
      }
      
      
      public void AddScheduledOperation(Model.ScheduledOperation newScheduledOperation)
      {
         if (newScheduledOperation == null)
            return;
         if (this.scheduledOperation == null)
            this.scheduledOperation = new System.Collections.Generic.List<ScheduledOperation>();
         if (!this.scheduledOperation.Contains(newScheduledOperation))
            this.scheduledOperation.Add(newScheduledOperation);
      }
      
      
      public void RemoveScheduledOperation(Model.ScheduledOperation oldScheduledOperation)
      {
         if (oldScheduledOperation == null)
            return;
         if (this.scheduledOperation != null)
            if (this.scheduledOperation.Contains(oldScheduledOperation))
               this.scheduledOperation.Remove(oldScheduledOperation);
      }
      
      
      public void RemoveAllScheduledOperation()
      {
         if (scheduledOperation != null)
            scheduledOperation.Clear();
      }
   
   }
}