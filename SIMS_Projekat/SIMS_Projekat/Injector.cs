using SIMS_Projekat.PatientView.VMPatientConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class Injector
    { 

        private ReportsConverter reportsConverter = new ReportsConverter();

        //private SubjectConverter subjectConverter = new SubjectConverter();

        //private StudentConverter studentConverter = new StudentConverter();



        public ReportsConverter ReportsConverter
        {
            get { return reportsConverter; }
        }

        //public TeacherConverter TeacherConverter
        //{
        //    get { return teacherConverter; }
        //}

        //public StudentConverter StudentConverter
        //{
        //    get { return studentConverter; }
        //}
    }
}
