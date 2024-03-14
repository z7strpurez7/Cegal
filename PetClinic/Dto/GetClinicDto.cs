namespace PetClinic.Dto
{
    public class GetClinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<OperatingHourDto> OperatingHours { get; set; }
    }
}
