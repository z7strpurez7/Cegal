namespace PetClinic.Model
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaximumNumberOfVets { get; set; }
        
        public ICollection<OpeningHour> OpeningHours { get; set; }
        public ICollection<Vet> Vets { get; set; }
    }
}
