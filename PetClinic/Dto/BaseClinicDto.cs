namespace PetClinic.Dto
{
    public class BaseClinicDto
    {
        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int MaximumNumberOfVets { get; set; }
        public ICollection<OperatingHourDto> OperatingHours { get; set; }
    }
}
