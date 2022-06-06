using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class FreeDayRequestRepository : IRepository<FreeDayRequest>
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

        public List<FreeDayRequest> GetAll()
        {
            return requests;
        }

        public FreeDayRequest Add(FreeDayRequest newRequest)
        {
            requests.Add(newRequest);
            Serialize();
            return newRequest;
        }

        public FreeDayRequest Delete(FreeDayRequest entity)
        {
            requests.Remove(entity);
            return entity;
        }

        public void Serialize()
        {
            serializer.toCSV(file, requests);
        }

        public void Deserialize()
        {
            requests = serializer.fromCSV(file);
        }

        public FreeDayRequest Edit(FreeDayRequest entity)
        {
            throw new NotImplementedException();
        }

        public FreeDayRequest GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<FreeDayRequest> GetRequestsByDoctor(Doctor d)
        {
            List<FreeDayRequest> requests1 = new List<FreeDayRequest>();
            foreach (FreeDayRequest r in requests)
            {
                if (r.doctor.Equals(d))
                {
                    requests1.Add(r);
                }
            }
            return requests1;
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
                        Add(oRequest);
                }
            }
        }

        public void RemoveAllRequests()
        {
            if (requests != null)
                requests.Clear();
        }
    }
}
