﻿using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView.ViewModel
{
    public class FreeDayRequestViewModel : BindableBase
    {
        private Frame frame;
        private Doctor doctor;
        private string razlog;
        private string fromDate;
        private string untilDate;
        private string hitno;
        public MyICommand sendCommand { get; set; }
        public MyICommand backCommand { get; set; }

        public FreeDayRequestViewModel(Frame main, Doctor d)
        {
            frame = main;
            doctor = d;

            sendCommand = new MyICommand(OnSend);
            backCommand = new MyICommand(OnBack);
        }

        private void OnSend()
        {
            String from1 = Od;
            String until1 = Do;
            DateTime from2 = DateTime.Parse(from1);
            DateTime until2 = DateTime.Parse(until1);
            bool can = true;
            bool urgent;
            if (Hitno == "True")
            {
                urgent = true;
            }
            else
            {
                urgent = false;
            }
            int var = from2.CompareTo(until2);
            if (var == 0 || var > 0)
            {
                can = false;
            }

            if (!urgent)
            {
                foreach (Doctor d in App.accountRepository.GetAllDoctorAccountBySpeciality(doctor.Speciality.ToString()))
                {
                    foreach (Model.FreeDayRequest f in App.freeDayRequestRepository.GetRequests())
                    {
                        int temp = f.from.CompareTo(from2); //pocetni isti
                        int temp2 = f.until.CompareTo(until2); //krajnji isti
                        int temp3 = f.until.CompareTo(from2); //kraj-pocetak
                        int temp4 = f.from.CompareTo(until2); // ppocetak-kraj
                        if (temp == 0 || temp2 == 0 || temp3 == 0 || temp4 == 0)
                        {
                            can = false;
                        }
                        else if (temp > 0 && temp4 < 0 || temp2 < 0 && temp3 > 0 || temp > 0 && temp2 < 0 || temp < 0 && temp2 > 0)
                        {
                            can = false;
                        }
                    }
                }
            }
            if (can)
            {
                Model.FreeDayRequest request = new Model.FreeDayRequest()
                {
                    from = DateTime.Parse(from1),
                    until = DateTime.Parse(until1),
                    sentDate = DateTime.Now,
                    doctor = doctor,
                    secretaryComment = Razlog,
                    status = Status.Waiting,
                    isUrgent = urgent
                };

                App.freeDayRequestRepository.AddRequest(request);

                App.freeDayRequestRepository.Serialize();
                frame.Content = new Requests(frame, doctor);
            }
        }

        private void OnBack()
        {
            frame.Content = new Requests(frame, doctor);
        }

        public string Razlog
        {
            get { return razlog; }
            set
            {
                if (Razlog != value)
                {
                    razlog = value;
                    OnPropertyChanged("Razlog");
                }
            }
        }

        public string Hitno
        {
            get { return hitno; }
            set
            {
                if (Hitno != value)
                {
                    hitno = value;
                    OnPropertyChanged("Hitno");
                }
            }
        }

        public String Od
        {
            get { return fromDate; }
            set
            {
                if(Od != value)
                {
                    fromDate = value;
                    OnPropertyChanged("Od");
                }
            }
        }
        public String Do
        {
            get { return untilDate; }
            set
            {
                if (Do != value)
                {
                    untilDate = value;
                    OnPropertyChanged("Do");
                }
            }
        }

        public DateTime? StartDate { get; set; } = DateTime.Now.AddDays(2);
        public DateTime? EndDate { get; set; } = DateTime.Now.AddDays(3);
    }
}
