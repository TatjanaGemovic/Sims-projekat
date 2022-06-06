using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{

    public class ExchangeEquipmentRequestRepository : IRepository<ExchangeEquipmentRequest>
        {
            List<ExchangeEquipmentRequest> requests;
            private Serializer<ExchangeEquipmentRequest> serializer;
            private string file;

            public ExchangeEquipmentRequestRepository(string fileName)
            {
                requests = new List<ExchangeEquipmentRequest>();
                serializer = new Serializer<ExchangeEquipmentRequest>();
                file = fileName;

            }

            public ExchangeEquipmentRequest Edit(ExchangeEquipmentRequest entity)
            {
                throw new NotImplementedException();
            }

            public ExchangeEquipmentRequest GetByID(string id)
            {
                return requests.Where(x => x.requestID == id).FirstOrDefault();
            }

            public List<ExchangeEquipmentRequest> GetAll()
            {
                return requests;
            }

            public void Serialize()
            {
                serializer.toCSV(file, requests);
            }

            public void Deserialize()
            {
                requests = serializer.fromCSV(file);
            }

            public ExchangeEquipmentRequest Add(ExchangeEquipmentRequest entity)
            {
                requests.Add(entity);
                Serialize();
                Deserialize();
                return entity;
            }

            public ExchangeEquipmentRequest Delete(ExchangeEquipmentRequest entity)
            {
                requests.Remove(entity);
                Serialize();
                Deserialize();
                return entity;
            }
    }
    
}
