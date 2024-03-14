namespace PetClinic.Model
{
    public class Vet
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        
        public string Name { get; set; }
        public string StreetAdress {  get; set; }
        public string City { get; set; }
        public string Zip {  get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Clinic Clinic { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
    }
}
