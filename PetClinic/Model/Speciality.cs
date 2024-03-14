namespace PetClinic.Model
{
    public class Speciality
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public Vet Vet { get; set; }
        public string SpecialityName { get; set; }
    }
}
