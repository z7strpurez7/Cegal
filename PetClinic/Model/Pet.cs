namespace PetClinic.Model
{
    public class Pet
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int BirthYear { get; set; }
        public string Breed { get; set; }
        public string MedicalHistory { get; set; }
    }
}
