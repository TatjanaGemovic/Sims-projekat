using ConsoleApp.serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Repository
{
    public class ScheduledOperationRepository
    {
        public ObservableCollection<ScheduledOperation> scheduledOperation;
        private Serializer<ScheduledOperation> serializer;
        private string file;
        private int id;

        public ScheduledOperationRepository(string sOperations_CSV)
        {
            file = sOperations_CSV;
            serializer = new Serializer<ScheduledOperation>();
            //id = 0;
        }
        public Model.ScheduledOperation CancelScheduledOperation(Model.ScheduledOperation scheduledOperation)
        {
            throw new NotImplementedException();
        }

        public Model.ScheduledOperation GetByScheduledOperationByID(int operationID)
        {
            foreach (Model.ScheduledOperation oScheduledOperation in scheduledOperation)
            {
                if (oScheduledOperation.OperationID == operationID)
                {
                    return oScheduledOperation;
                }
            }
            return null;
        }

        public ObservableCollection<ScheduledOperation> GetScheduledOperations()
        {
            return scheduledOperation;
        }

        public Model.ScheduledOperation EditScheduledOperation(Model.ScheduledOperation sOperation)
        {
            foreach (Model.ScheduledOperation oScheduledOperation in scheduledOperation)
            {
                if (oScheduledOperation.OperationID == sOperation.OperationID)
                {
                    RemoveScheduledOperation(oScheduledOperation);
                    AddScheduledOperation(sOperation);
                    return sOperation;
                }
            };
            return null;
        }

     

        public ObservableCollection<ScheduledOperation> ScheduledOperation
        {
            get
            {
                if (scheduledOperation == null)
                    scheduledOperation = new ObservableCollection<ScheduledOperation>();
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

        public void Serialize()
        {
            //int max_id = 0;
            serializer.toCSV(file, scheduledOperation);
            /*foreach(Model.ScheduledOperation oScheduledOperation in scheduledOperation)
            {
                if(oScheduledOperation.OperationID > max_id)
                {
                    max_id = oScheduledOperation.OperationID;
                }
            }
            id = ++max_id;*/
        }
        public void Deserialize()
        {
            scheduledOperation = serializer.fromCSV(file);
        }

        public void AddScheduledOperation(Model.ScheduledOperation newScheduledOperation)
        {
            if (newScheduledOperation == null)
                return;
            if (this.scheduledOperation == null)
                this.scheduledOperation = new ObservableCollection<ScheduledOperation>();
            if (!this.scheduledOperation.Contains(newScheduledOperation))
            {
                //newScheduledOperation.OperationID = id++;
                this.scheduledOperation.Add(newScheduledOperation);
            }
                
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