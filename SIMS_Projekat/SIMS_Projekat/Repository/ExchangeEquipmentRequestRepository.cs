using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{

        public class ExchangeEquipmentRequestRepository
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

            public List<ExchangeEquipmentRequest> GetAllRequest()
            {
                return requests;
            }

            public ExchangeEquipmentRequest GetRequestByID(string id)
            {
                foreach (ExchangeEquipmentRequest request in requests)
                {
                    if (request.requestID.Equals(id))
                    {
                        return request;
                    }
                }

                return null;
            }

            public ExchangeEquipmentRequest AddRequest(ExchangeEquipmentRequest newRequest)
            {
                requests.Add(newRequest);
                Serialize();
                Deserialize();
                return newRequest;
            }
            public ExchangeEquipmentRequest DeleteRequest(ExchangeEquipmentRequest deleteRequest)
            {

                requests.Remove(deleteRequest);
                Serialize();
                Deserialize();
                return deleteRequest;

            }


            public void Serialize()
            {
                serializer.toCSV(file, requests);
            }

            public void Deserialize()
            {
                requests = serializer.fromCSV(file);
            }

        }
    
}
