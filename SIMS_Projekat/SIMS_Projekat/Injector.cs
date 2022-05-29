﻿using SIMS_Projekat.PatientView.VMPatientConverters;
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

        private NotesConverter notesConverter = new NotesConverter();

        //private StudentConverter studentConverter = new StudentConverter();



        public ReportsConverter ReportsConverter
        {
            get { return reportsConverter; }
        }

        public NotesConverter NotesConverter
        {
            get { return notesConverter; }
        }

        //public StudentConverter StudentConverter
        //{
        //    get { return studentConverter; }
        //}
    }
}
