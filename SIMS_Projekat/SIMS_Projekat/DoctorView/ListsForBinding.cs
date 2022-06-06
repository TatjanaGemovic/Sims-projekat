using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static SIMS_Projekat.DoctorView.Examinations;
using static SIMS_Projekat.DoctorView.PatientExaminationList;
using static SIMS_Projekat.DoctorView.ReportPage;
using static SIMS_Projekat.DoctorView.Scheduling;
using static SIMS_Projekat.DoctorView.ViewModel.RequestsViewModel;

namespace SIMS_Projekat.DoctorView
{
    public class ListsForBinding
    {
        public BindingList<AppointmentInformation> CreatePatientList(Doctor doctor, string selectedDate)
        {
            BindingList<AppointmentInformation> appointmentInformations = new BindingList<AppointmentInformation>();

            foreach (Model.Appointment appointment in App.appointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
                DateTime date = appointment.beginningDate;
                string dateTime = date.ToString("MM/dd/yyyy HH:mm");
                DateTime date2 = appointment.endDate;
                string dateTime3 = date2.ToString("MM/dd/yyyy HH:mm");

                String[] datePart = dateTime.Split(" ");
                string time = datePart[1]; 
                int day = App.dateTimeFormater.FormatDate(dateTime);
                int day2 = App.dateTimeFormater.FormatDate(selectedDate);

                String type;
                if (appointment.operation == false)
                    type = "Pregled";
                else
                    type = "Operacija";

                if (day == day2)
                    appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                              time, dateTime3, type));
            }
            return appointmentInformations;
        }

        public ObservableCollection<RequestsViewModel.Request2> CreateRequestsList(Doctor doctor)
        {
            ObservableCollection<Request2> requests = new ObservableCollection<Request2>();
            if (App.freeDayRequestRepository.GetAll() != null)
            {
                foreach (Model.FreeDayRequest r in App.freeDayRequestRepository.GetAll())
                {
                    if (r.doctor.Equals(doctor))
                    { 
                        string time = App.dateTimeFormater.FormatTime(r);
                        string status;

                        if (r.status.ToString().Equals("Waiting"))
                            status = "Na cekanju";
                        else if (r.status.ToString().Equals("Accepted"))
                            status = "Prihvacen";
                        else
                            status = "Odbijen";

                        requests.Add(new Request2(time, status));
                    }
                }
            }
            return requests;
        }  

        public BindingList<Appointment2> CreateExaminationsList(Doctor doctor)
        {
            BindingList<Appointment2> AppointmentList = new BindingList<Appointment2>();

            if (App.appointmentRepo.GetAllAppointments() != null)
            {
                foreach (Appointment app in App.appointmentRepo.GetAllAppointments())
                {
                    if (app.doctor.Equals(doctor))
                    {
                        string date = app.beginningDate.ToString();
                        String[] datePart = date.Split(" ");
                        String[] timePart = datePart[1].Split(":");
                        String[] datePart2 = datePart[0].Split("/");
                        string time = datePart2[1] + "." + datePart2[0] + "." + " - " + timePart[0] + ":" + timePart[1];

                        AppointmentList.Add(new Appointment2(app.appointmentID, datePart[0], app.patient.FirstName + " " + app.patient.LastName, time));
                    }
                }
            }
            return AppointmentList;
        }

        public BindingList<PatientExaminationList.FinishedAppointment2> CreateExaminationListForPatient(Patient patient)
        {
            BindingList<FinishedAppointment2>  patientFinishedAppointmentList = new BindingList<FinishedAppointment2>();
            if (App.finishedAppointmentRepo.GetAllAppointments() != null)
            {
                foreach (FinishedAppointment app in App.finishedAppointmentRepo.GetAllAppointments())
                {
                    if (app.patient.ID.Equals(patient.ID))
                    {
                        string date = app.beginningDate.ToString();
                        String[] datePart = date.Split(" ");
                        String[] timePart = datePart[1].Split(":");
                        String[] datePart2 = datePart[0].Split("/");
                        string time = datePart2[1] + "." + " Maj" + "   " + timePart[0] + ":" + timePart[1];

                        patientFinishedAppointmentList.Add(new FinishedAppointment2(app.finishedAppointmentID, datePart2[1] + "." + " Maj", app.doctor.FirstName + " " + app.doctor.LastName, time));
                    }
                }
            }
            return patientFinishedAppointmentList;
        }

        public BindingList<string> CreateMedicineListForPatient(Patient patient)
        {
            BindingList<String>  medicines = new BindingList<String>();
            foreach (Medicine m in App.medicineController.GetMedicine())
            {
                bool temp = true;
                foreach (Allergen a in patient.Allergens)
                {
                    List<string> komponente = m.MedicineComponents;
                    foreach (String k in komponente)
                    {
                        if (a.Name.Equals(k))
                            temp = false; //ne moze ovaj lek
                    }
                }
                if (temp)
                    medicines.Add(m.MedicineName);
            }
            return medicines;
        }

        public int SelectMonth(string selectedMonth)
        {
            int month;
            if (selectedMonth.Equals("Januar"))
                month = 1;
            else if (selectedMonth.Equals("Februar"))
                month = 2;
            else if (selectedMonth.Equals("Mart"))
                month = 3;
            else if (selectedMonth.Equals("April"))
                month = 4;
            else if (selectedMonth.Equals("Maj"))
                month = 5;
            else if (selectedMonth.Equals("Jun"))
                month = 6;
            else if (selectedMonth.Equals("Jul"))
                month = 7;
            else if (selectedMonth.Equals("Avgust"))
                month = 8;
            else if (selectedMonth.Equals("Septembar"))
                month = 9;
            else if (selectedMonth.Equals("Oktobar"))
                month = 10;
            else if (selectedMonth.Equals("Novembar"))
                month = 11;
            else
                month = 12;

            return month;
        }

        public List<PdfData> CreateListForPdf(string selectedMonth, Doctor doctor)
        {
            List<PdfData> data = new List<PdfData>();

            int month = SelectMonth(selectedMonth);

            foreach (Appointment a in App.appointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
                string date = a.beginningDate.ToString();
                String[] datePart = date.Split("/");
                int month2 = int.Parse(datePart[0]);
                int day = int.Parse(datePart[1]);
                if (month == month2)
                {
                    string op;
                    if (a.operation == true)
                        op = "Operacija";
                    else
                        op = "Pregled";

                    data.Add(new PdfData(day.ToString() + " " + selectedMonth, a.patient.FirstName + " " + a.patient.LastName, op));
                }
            }
            return data;
        }

        public List<string> CreateAppointmentTimeList()
        {
            List<string> timeList = new List<string> { "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30",
                                                        "09:45", "10:00", "10:15", "10:30" ,"10:45" ,"11:00" ,"11:15",
                                                        "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00",
                                                        "13:15", "13:30","13:45", "14:00", "14:15", "14:30" ,"14:45" ,
                                                        "15:00" ,"15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45"};
            return timeList;
        }
    }
}
