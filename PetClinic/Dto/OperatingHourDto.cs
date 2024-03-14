using PetClinic.Model;

namespace PetClinic.Dto
{
    public class OperatingHourDto
    {
        public string DayOfWeek { get; set; } 
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }

    public class GetClinicOperatingHoursDto
    {
        public string DayOfWeek { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
    }
}
