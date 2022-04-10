using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Service
{
    public class ScheduledOperationService
    {
        public ScheduledOperationRepository scheduledOperationRepository;

        public ScheduledOperationService(ScheduledOperationRepository scheduledOperationRepository)
        {
            this.scheduledOperationRepository = scheduledOperationRepository;
        }
        public Model.ScheduledOperation ScheduleOperation(Model.ScheduledOperation scheduledOperation)
        {
            scheduledOperationRepository.AddScheduledOperation(scheduledOperation);
            return scheduledOperation;
        }

        public Model.ScheduledOperation CancelOperation(Model.ScheduledOperation scheduledOperation)
        {
            scheduledOperationRepository.RemoveScheduledOperation(scheduledOperation);
            return scheduledOperation;
        }

        public Model.ScheduledOperation GetByID(string id)
        {
            int id2 = Int32.Parse("id");
            return scheduledOperationRepository.GetByScheduledOperationByID(id2);

        }

        public ObservableCollection<ScheduledOperation> GetAll()
        {
            return scheduledOperationRepository.GetScheduledOperations();
        }

        public ObservableCollection<ScheduledOperation> GetAllByDoctor(Model.Doctor doctor)
        {
            ObservableCollection<ScheduledOperation> operations = scheduledOperationRepository.GetScheduledOperations();
            foreach (Model.ScheduledOperation oScheduledOperation in operations)
            {
                if (oScheduledOperation.Doctor != doctor)
                {
                    operations.Remove(oScheduledOperation);
                }
            }
            return operations;
        }

        public ObservableCollection<ScheduledOperation> GetAllByPatient(Model.Patient patient)
        {
            ObservableCollection<ScheduledOperation> operations = scheduledOperationRepository.GetScheduledOperations();
            foreach (Model.ScheduledOperation oScheduledOperation in operations)
            {
                if (oScheduledOperation.Patient != patient)
                {
                    operations.Remove(oScheduledOperation);
                }
            }
            return operations;
        }

        public Model.ScheduledOperation Edit(Model.ScheduledOperation scheduledOperation)
        {
            return scheduledOperationRepository.EditScheduledOperation(scheduledOperation);
        }

    }
}