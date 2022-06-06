using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class ScheduleAppointmentService
    {
        public AppointmentRepository appointmentRepository;

        public ScheduleAppointmentService(AppointmentRepository appointmentRepo)
        {
            appointmentRepository = appointmentRepo;
        }

        public List<string> GetAvailableAppointmentsForDoctor(AppointmentServiceDTO dto)
        {
            List<string> listOfTakenAppointmentTime = new List<string>();
            foreach (Appointment appointment in App.appointmentController.GetAllAppointments())
            {
                int day1 = App.dateTimeFormater.ChangeDateFormat(appointment.beginningDate);
                int day2 = App.dateTimeFormater.ChangeDateFormat2(dto.date);

                if (day2 == day1)
                {
                    if (CheckSelectedRoomOccupancy(appointment.beginningDate, dto.room) ||
                       !CheckIfPatientIsAvailable(dto.patient, appointment.beginningDate) ||
                       !CheckIfDoctorIsAvailable(dto.doctor, appointment.beginningDate))
                    {
                        listOfTakenAppointmentTime.Add(appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm"));
                    }
                }
            }
            return listOfTakenAppointmentTime;
        }
  
        public bool CheckSelectedRoomOccupancy(DateTime date, Room selectedRoom)
        {
            Room room = App.roomController.GetRoomByID(selectedRoom.RoomID);

            foreach (Appointment app in App.appointmentController.GetAllAppointments())
            {
                if (date == app.beginningDate)
                {
                    if (room == app.room)
                    {
                        return true;
                    }
                }
            }
            return false;
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
    }
}
