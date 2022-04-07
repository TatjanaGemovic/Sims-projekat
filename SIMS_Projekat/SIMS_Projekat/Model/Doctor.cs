namespace SIMS_Projekat.Model
{
    public class Doctor : Account
    {
        public string LicenceNumber { get; set; }
        public Appointment[] Appointment { get; set; }
        public ScheduledOperation[] ScheduledOperation { get; set; }
    }
}