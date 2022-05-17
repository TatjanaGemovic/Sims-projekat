using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class EvaluationController
    {
        public EvaluationService evaluationService;

        public Evaluation SetEvaluation(Evaluation evaluation)
        {
            return evaluationService.SetEvaluation(evaluation);
        }

        public Evaluation GetEvaluationByID(int evaluationID)
        {
            return evaluationService.GetEvaluationByID(evaluationID);
        }

        public List<Evaluation> GetEvaluationByPatientID(string patientID)
        {
            return evaluationService.GetEvaluationByPatientID(patientID);
        }

        public List<Evaluation> GetEvaluationByDoctorID(string doctorID)
        {
            return evaluationService.GetEvaluationByDoctorID(doctorID);
        }

        public List<Evaluation> GetAllEvaluations()
        {
            return evaluationService.GetAllEvaluations();
        }

        public Evaluation AddEvaluation(Evaluation newEvaluation)
        {
            return evaluationService.AddEvaluation(newEvaluation);

        }

        public Evaluation DeleteEvaluation(Evaluation oldEvaluation)
        {
            return evaluationService.DeleteEvaluation(oldEvaluation);

        }
    }
}
