using SIMS_Projekat.DTOModel;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class RenovationRequestRepository
    {
        public static List<RenovationRequest> requests;
        private Serializer<RenovationRequest> serializer;
        private string file;

        public RenovationRequestRepository(string fileName)
        {
            requests = new List<RenovationRequest>();
            serializer = new Serializer<RenovationRequest>();
            file = fileName;


        }

        public List<RenovationRequest> GetAllRequest()
        {
            Deserialize();
            return requests;
        }

        public RenovationRequest GetRequestByID(string id)
        {
            foreach (RenovationRequest request in requests)
            {
                if (request.requestID.Equals(id))
                {
                    return request;
                }
            }

            return null;
        }

        public RenovationRequest AddRequest(RenovationRequest newRequest)
        {
            requests.Add(newRequest);
            Serialize();
            Deserialize();
            return newRequest;
        }
        public RenovationRequest DeleteRequest(RenovationRequest deleteRequest)
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
