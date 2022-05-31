using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SIMS_Projekat.Model;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class EvaluationViewModel : BindableBase
    {
        public ObservableCollection<Evaluation> Evaluations { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand SendCommand { get; set; }
        //public NavigationService NavService { get; set; }

        private Evaluation selectedEvaluation;
        private string doctorFullName;

        private string comment;
        private int staff;
        private int waitingTime;
        private int recommendHospital;
        private int doctorIsUnderstandable;
        private int doctorIsInterested;
        private int recommendDoctor;

        private Frame mainFrame;
        public EvaluationViewModel(Frame frame, Evaluation evaluation)
        {
            mainFrame = frame;
            selectedEvaluation = evaluation;
            LoadEvaluation();
            Doctor doctor = App.accountController.GetDoctorAccountByID(evaluation.doctor.ID) as Doctor;
            doctorFullName = doctor.FirstName + " " + doctor.LastName;

            SendCommand = new MyICommand(OnSend, CanSend);
            BackCommand = new MyICommand(OnBack);

        }

        private bool CanSend()
        {
            if (Staff == 0 || WaitingTime == 0 || RecommendHospital == 0 || DoctorIsUnderstandable == 0
                || DoctorIsInterested == 0 || RecommendDoctor == 0)
                return false;
            
            return true;

        }

        private void OnSend()
        {
            Evaluation newEvaluation = new Evaluation
            {
                patient = selectedEvaluation.patient,
                doctor = selectedEvaluation.doctor,
                doctorIsInterested = DoctorIsInterested,
                doctorIsUnderstandable = DoctorIsUnderstandable,
                waitingTime = WaitingTime,
                recommendDoctor = RecommendDoctor,
                recommendHospital = RecommendHospital,
                staff = Staff,
                comment = Comment,
                evaluationCreated = DateTime.Now,
                isFilled = true,
                evaluationID = selectedEvaluation.evaluationID
            };
            App.evaluationController.SetEvaluation(newEvaluation);
            mainFrame.Content = new Homepage(mainFrame, selectedEvaluation.patient);
        }
        private void OnBack()
        {
            Homepage homepage = new Homepage(mainFrame, selectedEvaluation.patient);
            mainFrame.NavigationService.Navigate(homepage);
        }

        public void LoadEvaluation()
        {
            ObservableCollection<Evaluation> evaluations = new ObservableCollection<Evaluation>();

            evaluations.Add(selectedEvaluation);

            Evaluations = evaluations;
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                    
                }
            }
        }
        public int Staff
        {
            get { return staff; }
            set
            {
                if (staff != value)
                {
                    staff = value;
                    OnPropertyChanged("Staff");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public int WaitingTime
        {
            get { return waitingTime; }
            set
            {
                if (waitingTime != value)
                {
                    waitingTime = value;
                    OnPropertyChanged("WaitingTime");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public int RecommendHospital
        {
            get { return recommendHospital; }
            set
            {
                if (recommendHospital != value)
                {
                    recommendHospital = value;
                    OnPropertyChanged("RecommendHospital");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public int DoctorIsUnderstandable
        {
            get { return doctorIsUnderstandable; }
            set
            {
                if (doctorIsUnderstandable != value)
                {
                    doctorIsUnderstandable = value;
                    OnPropertyChanged("DoctorIsUnderstandable");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public int DoctorIsInterested
        {
            get { return doctorIsInterested; }
            set
            {
                if (doctorIsInterested != value)
                {
                    doctorIsInterested = value;
                    OnPropertyChanged("DoctorIsInterested");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public int RecommendDoctor
        {
            get { return recommendDoctor; }
            set
            {
                if (recommendDoctor != value)
                {
                    recommendDoctor = value;
                    OnPropertyChanged("RecommendDoctor");
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string DoctorFullName
        {
            get { return doctorFullName; }
            set
            {
                if (doctorFullName != value)
                {
                    doctorFullName = value;
                    OnPropertyChanged("DoctorFullName");

                }
            }
        }


    }
}
