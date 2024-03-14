namespace PetClinic.Model
{
    public class OpeningHour
    {
        public int Id { get; set; } 
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        public string DayOfWeek { get; set; } 
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
