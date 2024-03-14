using PetClinic.Dto;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace PetClinic.Validation
{
    public class PetValidator
    {
        public List<string> ValidatePostPet(PostPetDto petDto)
        {
            var errors = new List<string>();

            List<string> validPetTypes = new List<string>
             {"Cat",
              "Dog",
              "Fish",
              "Bird",
              "Reptile",
              "Insect"
             };

            if (string.IsNullOrEmpty(petDto.Type))
            {
                errors.Add("Type is required.");
            }
            if (!validPetTypes.Contains(petDto.Type))
            {
                errors.Add("Invalid pet type. Must be one of: " + string.Join(", ", validPetTypes));
            }

            var currentYear = DateTime.Now.Year;
            if (petDto.BirthYear < currentYear - 200 || petDto.BirthYear > currentYear)
            {
                errors.Add("Invalid birth year. A pet can maximum be 200 years old");
            }

            errors.AddRange(ValidatePutPet(petDto));
            return errors;
        }
        public List<string> ValidatePutPet(BasePetDto petDto)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(petDto.Name))
            {
                errors.Add("Pet name is required.");
            }
            else if (petDto.Name.Length > 100)
            {
                errors.Add("Pet name cannot be more than 100 characters.");
            }
            if (string.IsNullOrEmpty(petDto.Breed))
            {
                errors.Add("Breed is required.");
            }
            else if (petDto.Breed.Length > 100)
            {
                errors.Add("Breed cannot be more than 100 characters.");
            }
            if (!string.IsNullOrEmpty(petDto.MedicalHistory) && petDto.MedicalHistory.Length > 5000)
            {
                errors.Add("Medical history cannot be more than 5000 characters.");
            }

            return errors;
        }

    }
}
