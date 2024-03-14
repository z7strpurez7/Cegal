namespace PetClinic.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }    
        public char Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
