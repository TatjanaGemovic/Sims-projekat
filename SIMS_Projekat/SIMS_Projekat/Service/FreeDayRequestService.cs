using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Service
{
    public class FreeDayRequestService
    {
        public FreeDayRequestRepository freeDayRequestRepository;

        public FreeDayRequestService(FreeDayRequestRepository Repository)
        {
            freeDayRequestRepository = Repository;
        }

        public List<FreeDayRequest> GetRequests()
        {
            return freeDayRequestRepository.GetRequests();
        }

        public List<FreeDayRequest> GetRequestsByDoctor(Doctor d)
        {
            return freeDayRequestRepository.GetRequestsByDoctor(d);
        }

        public FreeDayRequest AddRequest(FreeDayRequest newRequest)
        {
            return freeDayRequestRepository.AddRequest(newRequest);
        }

        public bool CanSendRequest(bool urgent, DateTime from, DateTime until, Doctor doctor)
        {
            bool canSendRequest = true;
            int var = from.CompareTo(until);
            if (var == 0 || var > 0)
                canSendRequest = false;

            if (!urgent)
            {
                foreach (Doctor d in App.accountRepository.GetAllDoctorAccountBySpeciality(doctor.Speciality.ToString()))
                {
                    foreach (Model.FreeDayRequest request in App.freeDayRequestRepository.GetRequests())
                    {
                        int temp = request.from.CompareTo(from); //pocetni isti
                        int temp2 = request.until.CompareTo(until); //krajnji isti
                        int temp3 = request.until.CompareTo(from); //kraj-pocetak
                        int temp4 = request.from.CompareTo(until); // pocetak-kraj

                        if (temp == 0 || temp2 == 0 || temp3 == 0 || temp4 == 0)
                            canSendRequest = false;
                        else if (temp > 0 && temp4 < 0 || temp2 < 0 && temp3 > 0 || temp > 0 && temp2 < 0 || temp < 0 && temp2 > 0)
                            canSendRequest = false;
                    }
                }
            }

            return canSendRequest;
        }
    }
}
