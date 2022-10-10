using SIMS.CompositeComon;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.ManagerViewModel
{
    public class PollViewModel
    {
        private Evaluation _selectedPolll;
        private RelayCommand back;

        public PollViewModel(Evaluation poll)
        {
            _selectedPolll = poll;
        }

        public string IzabaraniLekar
        {
            get { return _selectedPolll.doctor.FirstName + " " + _selectedPolll.doctor.LastName; }
           
        }

        public string OcenaDoktor
        {
            get { return _selectedPolll.doctorIsUnderstandable.ToString()+"/5"; }

        }

        public string DoktorSlusanje
        {
            get { return _selectedPolll.doctorIsInterested.ToString() + "/5"; }

        }

        public string DoktorPreporuka
        {
            get { return _selectedPolll.recommendDoctor.ToString() + "/5"; }

        }

        public string OcenaOprema
        {
            get { return _selectedPolll.staff.ToString() + "/5"; }

        }

        public string BrzinaPregleda
        {
            get { return _selectedPolll.waitingTime.ToString() + "/5"; }

        }

        public string BolnicaPreporuka
        {
            get { return _selectedPolll.recommendHospital.ToString() + "/5"; }

        }

        public string SrednjaDoktor
        {
            get { return _selectedPolll.averageDoctorRating.ToString() + "/5"; }

        }

        public string SrednjaBolnica
        {
            get { return _selectedPolll.averageHospitalRating.ToString() + "/5"; }

        }

        public string PacijentKomentar
        {
            get { return _selectedPolll.comment; }

        }

        


        public RelayCommand Back
        {
            get
            {
                return back ?? (new RelayCommand(param => BackBtn_Click()));
            }
        }

        private void BackBtn_Click()
        {
            ManagerHome.mainFrame.Content = new PollsView();
        }

    }
}
