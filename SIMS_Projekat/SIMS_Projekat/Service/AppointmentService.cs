using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public List<string> GetAvailableAppointmentsForPatient(Patient p, DateTime dt, String licence)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (dt.Date == appointment.beginningDate.Date) //za dan
                {
                    if (!appointment.operation)     // samo ako nije operacija
                    {
                        if (CheckRoomOccupancy(appointment.beginningDate)) //1.5. 2022. 15:30
                        {
                            listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));            // 15:30
                        }
                        else
                        {
                           
                            Doctor doctor = App.accountService.GetDoctorAccountByLicenceNumber(licence) as Doctor;
                            if (!CheckIfDoctorIsAvailable(doctor, appointment.beginningDate))
                            {
                                listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                            }
                            else
                            {
                                //ako pacijent bas taj ima termin kod drugog dr, a ne svog, ne moze u to vreme opet da zakaze
                                if (!CheckIfPatientIsAvailable(p, appointment.beginningDate))
                                {
                                    listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                                }
                            }
                            
                           
                        }
                    }
                }

            }
            return listOfTakenAppointmentTime;
        }

        public bool CheckRoomOccupancy(DateTime dt) 
        {
            ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);
            //int numberOfAvailableRooms = rooms.Count;
            //Room availableRoom = new Room();

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    if (dt == appointment.beginningDate)
                    {
                        //numberOfAvailableRooms--;
                        rooms.Remove(appointment.room);
                    }
                }              
            }

            if (rooms.Count == 0) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckIfDoctorIsAvailable(Doctor doctor, DateTime dt)
        {
            
            foreach (Appointment appointment in GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
         
                if (dt == appointment.beginningDate)
                {
                    
                    return false;           //doktor je zauzet, znaci termin se dodaje u listu zauzetih termina
                    
                }
                
            }
            return true;       

        }
        public bool CheckIfPatientIsAvailable(Patient patient, DateTime dt)
        {

            foreach (Appointment appointment in GetAppointmentByPatientID(patient.ID))
            {
                
                if (dt == appointment.beginningDate)
                {
                   
                   return false;           //pacijent je zauzet, znaci termin se dodaje u listu zauzetih termina
                    
                }
                
            }
            return true;

        }

        public Room GetAvailableRoom(DateTime start)
        {
            ObservableCollection<Room> rooms = App.roomController.GetRoomsByType(RoomType.examRoom);

            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    if (start == appointment.beginningDate)
                    {
                        rooms.Remove(appointment.room);
                    }
                }
            }
            return rooms[0];
        }

        public Appointment CreateRandomAppointment(Patient patient)
        {
            var random = new Random();
            Doctor doctor;
            DateTime currentDate;
            List<DateTime> dateList;
            int dateIndex;
            List<string> timeList;
            int timeIndex;
            List<Doctor> doctorList;
            int doctorIndex;
            DateTime startDate;
            TimeSpan timeStart;

            do
            {
                currentDate = DateTime.Now;
                dateList = new List<DateTime> { currentDate.AddDays(1), currentDate.AddDays(2), currentDate.AddDays(3) };
                dateIndex = random.Next(dateList.Count);

                timeList = createAppointmentTime();
                timeIndex = random.Next(timeList.Count);

                if (patient.doctorLicenceNumber == "")
                {
                    doctorList = App.accountService.GetAllDoctorAccounts();
                    doctorIndex = random.Next(doctorList.Count);
                    doctor = doctorList[doctorIndex];
                }
                else
                {
                    doctor = App.accountService.GetDoctorAccountByLicenceNumber(patient.doctorLicenceNumber) as Doctor;
                }

                startDate = dateList[dateIndex].Date;
                timeStart = TimeSpan.Parse(timeList[timeIndex]);
                startDate = startDate.Add(timeStart);
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
                room = room
            };
            return appointment;
            
        }

        public List<string> createAppointmentTime()
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