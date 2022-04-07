namespace SIMS_Projekat.Model
{
    public class Doctor : Person
    {
        public string LicenceNumber { get; set; }
        public Appointment[] Appointment { get; set; }
        public ScheduledOperation[] ScheduledOperation { get; set; }

    }
}