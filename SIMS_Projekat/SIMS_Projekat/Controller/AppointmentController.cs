using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
   public class AppointmentController
   {
      public Model.Appointment SetAppointment(Model.Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public Model.Appointment DeleteAppointment(Model.Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public Model.Appointment AddAppointment(Model.Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public Model.Appointment GetAppointmentByID(int appointmentID)
      {
         throw new NotImplementedException();
      }
      
      public List<Appointment> GetAppointmentByPatientID(string patientID)
      {
         throw new NotImplementedException();
      }
      
      public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
      {
         throw new NotImplementedException();
      }
      
      public AppointmentService appointmentService;
   
   }
}