using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class EvaluationRepository
    {
        private string path { get; set; }
        private Serializer<Evaluation> serializer;
        private List<Evaluation> evaluationList;
        private int id;

        public List<Evaluation> EvaluationList
        {
            get
            {
                if (evaluationList == null)
                    evaluationList = new List<Evaluation>();
                return evaluationList;
            }
            set
            {
                RemoveAllEvaluations();
                if (value != null)
                {
                    foreach (Evaluation oEvaluation in value)
                        AddEvaluation(oEvaluation);
                }
            }
        }

        public EvaluationRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<Evaluation>();
            id = 0;
        }


        public Evaluation SetEvaluation(Evaluation evaluation)
        {
            int index;
            foreach (Evaluation eval in evaluationList)
            {
                Evaluation evaluation1 = evaluationList.Find(eval => eval.evaluationID == evaluation.evaluationID);
                index = evaluationList.IndexOf(evaluation1);

                if (evaluation1 != null)
                {
                    evaluation1.patient = evaluation.patient;
                    evaluation1.doctor = evaluation.doctor;
                    evaluation1.comment = evaluation.comment;
                    evaluation1.doctorIsInterested = evaluation.doctorIsInterested;
                    evaluation1.doctorIsUnderstandable = evaluation.doctorIsUnderstandable;
                    evaluation1.evaluationCreated = evaluation.evaluationCreated;
                    evaluation1.recommendHospital = evaluation.recommendHospital;
                    evaluation1.recommendDoctor = evaluation.recommendDoctor;
                    evaluation1.waitingTime = evaluation.waitingTime;
                    evaluation1.staff = evaluation.staff;
                    evaluation1.isFilled = evaluation.isFilled;
                    return evaluation1;
                }

            }
            return null;
        }

        public Evaluation GetEvaluationByID(int evaluationID)
        {
            foreach (Evaluation evaluation in evaluationList)
            {
                Evaluation evaluation1 = evaluationList.Find(evaluation => evaluation.evaluationID == evaluationID);

                if (evaluation1 != null)
                {
                    return evaluation1;
                }
            }
            return null;
        }

        public List<Evaluation> GetEvaluationByPatientID(string patientID)
        {
            List<Evaluation> evaluationListForPatient = new List<Evaluation>();
            foreach (Evaluation evaluation in evaluationList)
            {
                if (evaluation.patient.ID.Equals(patientID))
                {
                    evaluationListForPatient.Add(evaluation);
                }
            }
            return evaluationListForPatient;
        }

        public List<Evaluation> GetEvaluationByDoctorID(string doctorID)
        {
            List<Evaluation> evaluationListForDoctor = new List<Evaluation>();
            foreach (Evaluation evaluation in evaluationList)
            {
                if (evaluation.doctor.LicenceNumber.Equals(doctorID))
                {
                    evaluationListForDoctor.Add(evaluation);
                }
            }
            return evaluationListForDoctor;
        }

        public List<Evaluation> GetAllEvaluations()
        {
            return evaluationList;
        }

        public Evaluation AddEvaluation(Evaluation newEvaluation)
        {
            id++;
            if (newEvaluation == null)
                return null;
            if (this.evaluationList == null)
                this.evaluationList = new List<Evaluation>();
            if (!this.evaluationList.Contains(newEvaluation))
            {
                newEvaluation.evaluationID = id;
                this.evaluationList.Add(newEvaluation);
            }

            return newEvaluation;
        }

        public Evaluation DeleteEvaluation(Evaluation oldEvaluation)
        {
            if (oldEvaluation == null)
                return null;
            if (this.evaluationList != null)
                if (this.evaluationList.Contains(oldEvaluation))
                    this.evaluationList.Remove(oldEvaluation);
            return oldEvaluation;
        }

        public void RemoveAllEvaluations()
        {
            if (evaluationList != null)
                evaluationList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, evaluationList);
        }

        public void Deserialize()
        {
            evaluationList = serializer.fromCSV(path);

            foreach (Evaluation evaluation in evaluationList)
            {
                id = evaluation.evaluationID;
            }
        }
    }
}
