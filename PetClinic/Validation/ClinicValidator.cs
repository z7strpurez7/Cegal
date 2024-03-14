using PetClinic.Dto;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PetClinic.Validation
{
    public class ClinicValidator
    {
        public List<string> ValidatePostClinic(PostClinicDto clinicDto)
        {
            var errors = new List<string>();


      

            if (clinicDto.MaximumNumberOfVets < 1)
            {
                errors.Add("There must be minimum 1 vet");
            }

            errors.AddRange(ValidatePutClinic(clinicDto));
            /*    if (clinicDto.MaximumNumberOfVets < clinicDto.NumberOfActiveVets || clinicDto.NumberOfActiveVets < 1)
                {
                    errors.Add("Invalid vet numbers. There must be at least 1 active vet and not more than the maximum number.");
                }*/
            return errors;
        }

        public List<string> ValidatePutClinic(BaseClinicDto clinicDto)
        {

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(clinicDto.Name))
            {
                errors.Add("Clinic name is required.");
            }
            else if (clinicDto.Name.Length > 200)
            {
                errors.Add("Clinic name cannot be more than 200 characters.");
            }



            if (string.IsNullOrEmpty(clinicDto.StreetAdress))
            {
                errors.Add("Street address is required");
            }

            if (
                !string.IsNullOrEmpty(clinicDto.StreetAdress)
                && clinicDto.StreetAdress.Length > 200
            )
            {
                errors.Add("Street address is too long");
            }

            if (
                !string.IsNullOrEmpty(clinicDto.StreetAdress)
                && clinicDto.StreetAdress.Split(" ").Length < 2
            )
            {
                errors.Add("Please Specify Street address and house number");
            }

            if (string.IsNullOrEmpty(clinicDto.City))
            {
                errors.Add("City is required");
            }

            if (!string.IsNullOrEmpty(clinicDto.City) && clinicDto.City.Length > 100)
            {
                errors.Add("City name is too long");
            }

            if (string.IsNullOrEmpty(clinicDto.ZipCode))
            {
                errors.Add("Zip code is required");
            }
            if (!string.IsNullOrEmpty(clinicDto.ZipCode) && clinicDto.ZipCode.Length != 4)
            {
                errors.Add("Zip code must be 4 digits");
            }
            if (string.IsNullOrEmpty(clinicDto.PhoneNumber))
            {
                errors.Add("Phone number is required");
            }
            if (!string.IsNullOrEmpty(clinicDto.PhoneNumber) && clinicDto.PhoneNumber.Length != 8)
            {
                errors.Add("Phone number must be 8 digits");
            }
            if (string.IsNullOrEmpty(clinicDto.Email))
            {
                errors.Add("Email is required");
            }
            if (!string.IsNullOrEmpty(clinicDto.Email) && !IsValidEmail(clinicDto.Email))
            {
                errors.Add("Email is not valid");
            }

            if (clinicDto.OperatingHours != null && !ValidateOperatingHours(clinicDto.OperatingHours))
            {
                errors.Add("Opening hour should be before closing hour");
            }

            return errors;
        }

        public static bool ValidateOperatingHours(ICollection<OperatingHourDto> operatingHours)
        {
            foreach (var hour in operatingHours)
            {
                TimeSpan openingTime = hour.OpeningTime.TimeOfDay;
                TimeSpan closingTime = hour.ClosingTime.TimeOfDay;
                if (openingTime >= closingTime)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(
                    email,
                    @"(@)(.+)$",
                    DomainMapper,
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(200)
                );

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250)
                );
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
