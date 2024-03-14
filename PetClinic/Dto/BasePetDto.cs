using PetClinic.Model;

namespace PetClinic.Dto
{
    public class BasePetDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string MedicalHistory { get; set; }

    }
}
