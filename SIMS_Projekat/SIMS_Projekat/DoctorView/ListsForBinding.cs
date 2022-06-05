﻿using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                DateTime dt = appointment.beginningDate;
                string dateTime = dt.ToString("MM/dd/yyyy HH:mm");
                DateTime dt2 = appointment.endDate;
                string dateTime3 = dt2.ToString("MM/dd/yyyy HH:mm");

                String[] datePart = dateTime.Split(" ");
                string time = datePart[1]; //vreme
                int dan = FormatDate(dateTime);
                int dan2 = FormatDate(selectedDate);

                String type;
                if (appointment.operation == false)
                    type = "Pregled";
                else
                    type = "Operacija";

                if (dan == dan2)
                    appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                              time, dateTime3, type));
            }
            return appointmentInformations;
        }

        public int FormatDate(string selectedDate)
        {
            String[] datePart2 = selectedDate.Split(" ");
            String date2 = datePart2[0];
            String[] deoDatuma2 = date2.Split("/");
            return int.Parse(deoDatuma2[1]);
        }

        public ObservableCollection<RequestsViewModel.Request2> CreateRequestsList(Doctor doctor)
        {
            ObservableCollection<Request2> requests = new ObservableCollection<Request2>();
            if (App.freeDayRequestRepository.GetRequests() != null)
            {
                foreach (Model.FreeDayRequest r in App.freeDayRequestRepository.GetRequests())
                {
                    if (r.doctor.Equals(doctor))
                    { 
                        string time = FormatTime(r);
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

        public string FormatTime(Model.FreeDayRequest r)
        {
            string from1 = r.from.ToString();
            string until1 = r.until.ToString();
            String[] parts = from1.Split(" ");
            String[] parts2 = until1.Split(" ");
            return parts[0] + "  -  " + parts2[0];
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
                        date = datePart[0];
                        string time = datePart[1];

                        AppointmentList.Add(new Appointment2(app.appointmentID, date, app.patient.FirstName + " " + app.patient.LastName, time));
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
                        date = datePart[0];
                        string time = datePart[1];

                        patientFinishedAppointmentList.Add(new FinishedAppointment2(app.finishedAppointmentID, date, app.doctor.FirstName + " " + app.doctor.LastName, time));
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
                string datum = a.beginningDate.ToString();
                String[] deoDatuma = datum.Split("/");
                int mesec = int.Parse(deoDatuma[0]);
                int dan = int.Parse(deoDatuma[1]);
                if (month == mesec)
                {
                    string op;
                    if (a.operation == true)
                        op = "Operacija";
                    else
                        op = "Pregled";

                    data.Add(new PdfData(dan.ToString() + " " + selectedMonth, a.patient.FirstName + " " + a.patient.LastName, op));
                }
            }

            return data;
        }
    }
}
