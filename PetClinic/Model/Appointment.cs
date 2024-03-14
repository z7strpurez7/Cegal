namespace PetClinic.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int VetId { get; set; }
        public Pet Pet { get; set; }
        public Schedule Schedule { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }


    }
}
