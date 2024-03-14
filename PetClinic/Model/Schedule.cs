namespace PetClinic.Model
{
    public class Schedule
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public int AppointmentId { get; set; } 
        public Vet Vet { get; set; }
        public Appointment Appointment { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; }

    }
}
