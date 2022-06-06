using SIMS_Projekat.DoctorView;
using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Projekat.Service
{
    public class AppointmentService
    {
        public AppointmentRepository appointmentRepository;
        public Appointment SetAppointment(Appointment appointment)
        {
            return appointmentRepository.SetAppointment(appointment);

        }

        public Model.Appointment DeleteAppointment(Model.Appointment appointment)
        {
            return appointmentRepository.DeleteAppointment(appointment);
        }

        public Model.Appointment AddAppointment(Model.Appointment appointment)
        {
            return appointmentRepository.AddAppointment(appointment);
        }

        public Model.Appointment GetAppointmentByID(int appointmentID)
        {
            return appointmentRepository.GetAppointmentByID(appointmentID);
        }

        public List<Appointment> GetAppointmentByPatientID(string patientID)
        {
            return appointmentRepository.GetAppointmentByPatientID(patientID);
        }

        public List<Appointment> GetAppointmentByDoctorLicenceNumber(string licenceNumber)
        {
            return appointmentRepository.GetAppointmentByDoctorLicenceNumber(licenceNumber);
        }

        public List<Appointment> GetAllAppointments()
        {
            return appointmentRepository.GetAllAppointments();
        }

        public bool CheckRoomOccupancy(DateTime dt)
        {
            List<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);

            foreach (Appointment app in GetAllAppointments())
            {
                if (!app.operation && dt == app.beginningDate)
                {
                    rooms.Remove(app.room);
                }
            }
            return rooms.Count == 0;
        }

        public bool CheckIfDoctorIsAvailable(Doctor doctor, DateTime date)
        {
            foreach (Appointment appointment in App.appointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
                if (date == appointment.beginningDate)
                {
                    return false;           //doktor je zauzet, znaci termin se dodaje u listu zauzetih termina
                }
            }
            return true;
        }
        public bool CheckIfPatientIsAvailable(Patient patient, DateTime dt)
        {
            foreach (Appointment appointment in App.appointmentController.GetAppointmentByPatientID(patient.ID))
            {
                if (dt == appointment.beginningDate)
                {
                    return false;           //pacijent je zauzet, znaci termin se dodaje u listu zauzetih termina
                }
            }
            return true;
        }

        public List<string> GetTakenAppointmentsForPatient(Patient p, DateTime dt, String licence)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            Doctor doctor = App.accountService.GetDoctorAccountByLicenceNumber(licence) as Doctor;

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (dt.Date == appointment.beginningDate.Date && !appointment.operation) //za dan
                {
                    if (CheckRoomOccupancy(appointment.beginningDate) ||
                        !CheckIfDoctorIsAvailable(doctor, appointment.beginningDate) ||
                        !CheckIfPatientIsAvailable(p, appointment.beginningDate)) //1.5. 2022. 15:30
                    {
                        listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));            // 15:30
                    }
                }
            }
            return listOfTakenAppointmentTime;
        }

        public List<Appointment> GetAppointmentsByRoomIdAndDate(string roomID, DateTime date)
        {
            List<Appointment> appointmentList = appointmentRepository.GetAllAppointments().Where(
                appointment => appointment.roomID == roomID
                && appointment.beginningDate.DayOfYear == date.DayOfYear).ToList();

            return appointmentList;
        }

        public Room GetAvailableRoom(DateTime start)
        {
            List<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    continue;
                }
                if (start == appointment.beginningDate)
                {
                    rooms.Remove(appointment.room);
                }
            }
            return rooms[0];
        }

        public Appointment CreateRandomAppointment(Patient patient)
        {
            DateTime startDate;
            Doctor doctor;

            do
            {
                startDate = CreateRandomDateAndTime();
                if (patient.doctorLicenceNumber == "")
                {
                    doctor = GetRandomDoctor();
                }
                else
                {
                    doctor = App.accountService.GetDoctorAccountByLicenceNumber(patient.doctorLicenceNumber) as Doctor;
                }

            }
            while (CheckRoomOccupancy(startDate) || !CheckIfDoctorIsAvailable(doctor, startDate) || !CheckIfPatientIsAvailable(patient, startDate));

            Room room = GetAvailableRoom(startDate);
            Appointment appointment = new Appointment()
            {
                beginningDate = startDate,
                doctor = doctor,
                patient = patient,
                endDate = startDate.AddMinutes(15),
                operation = false,
                room = room,
                isDelayedByPatient = false,
                isScheduledByPatient = true
            };
            return appointment;

        }

        private DateTime CreateRandomDateAndTime()
        {
            var random = new Random();

            DateTime currentDate = DateTime.Now;
            List<DateTime> dateList = new List<DateTime> { currentDate.AddDays(1), currentDate.AddDays(2), currentDate.AddDays(3) };
            int dateIndex = random.Next(dateList.Count);    // 10.5.2022.  12:00 AM

            List<string> timeList = CreateAppointmentTime();
            int timeIndex = random.Next(timeList.Count);

            DateTime startDate = dateList[dateIndex].Date;
            TimeSpan timeStart = TimeSpan.Parse(timeList[timeIndex]);

            return startDate.Add(timeStart);
        }

        private Doctor GetRandomDoctor()
        {
            var random = new Random();

            List<Doctor> doctorList = App.accountService.GetAllDoctorAccounts();
            int doctorIndex = random.Next(doctorList.Count);
            Doctor doctor = doctorList[doctorIndex];
            return doctor;
        }

        public List<string> CreateAppointmentTime()
        {
            List<string> timeList = new List<string> { "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30",
                                                        "09:45", "10:00", "10:15", "10:30" ,"10:45" ,"11:00" ,"11:15",
                                                        "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00",
                                                        "13:15", "13:30","13:45", "14:00", "14:15", "14:30" ,"14:45" ,
                                                        "15:00" ,"15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45"};
            return timeList;
        }

        public bool CheckForScheduledAppointments(Patient patient, DateTime beginningOfMonth)
        {
            int numberOfAppointments = CheckForFollowingAppointments(patient, beginningOfMonth);

            if(numberOfAppointments < 0)
                return false;

            int numberOfTakenAppointments = CheckForFinishedAppointments(patient, beginningOfMonth);
            if (numberOfTakenAppointments < 0)
                return false;

            if(numberOfAppointments + numberOfTakenAppointments >= 3)
                return false;

            return true;        // nije zakazao 3
        }
        public int CheckForFollowingAppointments(Patient patient, DateTime beginningOfMonth)
        {
            int numberOfAppointments = 0;
            foreach (Appointment appoint in GetAppointmentByPatientID(patient.ID))
            {
                if (appoint != null && appoint.isScheduledByPatient && beginningOfMonth <= appoint.beginningDate)
                {
                    numberOfAppointments++;
                    if (numberOfAppointments >= 3)
                        return -1;       //zakazao je vec 3    
                }
            }
            return numberOfAppointments;
        }
        public int CheckForFinishedAppointments(Patient patient, DateTime beginningOfMonth)
        {
            int numberOfAppointments = 0;
            foreach (FinishedAppointment appoint in App.finishedAppointmentService.GetAppointmentByPatientID(patient.ID))
            { 
                if (appoint != null && appoint.isScheduledByPatient && beginningOfMonth <= appoint.beginningDate)
                {
                    numberOfAppointments++;
                    if (numberOfAppointments >= 3)
                        return -1;      //prosla 3 termina vec
                }
            }
            return numberOfAppointments;
        }

        public List<Appointment> GetAppointmentsForTimeSpan(Doctor doctor, DateTime startDateTime, DateTime endDateTime)
        {
            List<Appointment> appointments = appointmentRepository.GetAllAppointments();
            return appointments.Where(appointment => appointment.beginningDate > startDateTime && 
                appointment.beginningDate < endDateTime && appointment.doctor.ID == doctor.ID).ToList();
        }

        public List<Appointment> GetAllAppointmentForToday()
        {
            List<Appointment> appointments = appointmentRepository.GetAllAppointments();
            return appointments.Where(appointment => appointment.beginningDate.Date == DateTime.Today).ToList();
        }
    }
}