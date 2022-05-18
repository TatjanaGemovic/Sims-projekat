using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class FreeDayRequestRepository
    {
        public static List<FreeDayRequest> requests;
        private Serializer<FreeDayRequest> serializer;
        private string file;

        public FreeDayRequestRepository(string fileName)
        {
            requests = new List<FreeDayRequest>();
            serializer = new Serializer<FreeDayRequest>();
            file = fileName;

        }

        public List<FreeDayRequest> GetRequests()
        {
            return requests;
        }

        public FreeDayRequest AddRequest(FreeDayRequest newRequest)
        {
            requests.Add(newRequest);
            Serialize();
            return newRequest;
        }

        public List<FreeDayRequest> Requests
        {
            get
            {
                if (requests == null)
                    requests = new List<FreeDayRequest>();
                return requests;
            }
            set
            {
                RemoveAllRequests();
                if (value != null)
                {
                    foreach (FreeDayRequest oRequest in value)
                        AddRequest(oRequest);
                }
            }
        }

        public void RemoveAllRequests()
        {
            if (requests != null)
                requests.Clear();
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
