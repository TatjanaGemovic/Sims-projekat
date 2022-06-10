using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class EvaluationService
    {
        public EvaluationRepository evaluationRepository;

        public Evaluation SetEvaluation(Evaluation evaluation)
        {
            return evaluationRepository.SetEvaluation(evaluation);
        }

        public Evaluation GetEvaluationByID(int evaluationID)
        {
            return evaluationRepository.GetEvaluationByID(evaluationID);
        }

        public List<Evaluation> GetEvaluationByPatientID(string patientID)
        {
            return evaluationRepository.GetEvaluationByPatientID(patientID);
        }

        public List<Evaluation> GetEvaluationByDoctorID(string doctorID)
        {
            return evaluationRepository.GetEvaluationByDoctorID(doctorID);
        }

        public List<Evaluation> GetAllEvaluations()
        {
            return evaluationRepository.GetAllEvaluations();
        }

        public Evaluation AddEvaluation(Evaluation newEvaluation)
        {
            return evaluationRepository.AddEvaluation(newEvaluation);

        }

        public Evaluation DeleteEvaluation(Evaluation oldEvaluation)
        {
            return evaluationRepository.DeleteEvaluation(oldEvaluation);

        }
        public List<Evaluation> GetEmptyEvaluationsForPatient(Patient patient)
        {
            List<Evaluation> evaluationList = GetEvaluationByPatientID(patient.ID);
            List<Evaluation> emptyEvaluations = new List<Evaluation>();

            foreach(Evaluation evaluation in evaluationList)
            {
                if(!evaluation.isFilled)
                    emptyEvaluations.Add(evaluation);
            }
            return emptyEvaluations;
        }
        public bool DeleteEvaluationIfMoreThanFiveDaysPassedForPatient(Patient patient)
        {
            foreach (Evaluation evaluation in GetEvaluationByPatientID(patient.ID))
            {
                if (!evaluation.isFilled && (evaluation.evaluationCreated.AddDays(5)) < DateTime.Now)
                    DeleteEvaluation(evaluation);
            }
            return true;
        }

        public List<Evaluation> GetPollsForInterval(DateTime start, DateTime end)
        {
            List<Evaluation> evaluationList = GetAllEvaluations();
            List<Evaluation> polls = new List<Evaluation>();

            foreach (Evaluation evaluation in evaluationList)
            {
                if (evaluation.evaluationCreated>= start && evaluation.evaluationCreated<=end)
                    polls.Add(evaluation);
            }
            return polls;
        }


    }
}
