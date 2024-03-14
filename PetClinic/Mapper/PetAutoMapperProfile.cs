using AutoMapper;
using PetClinic.Dto;
using PetClinic.Model;

namespace PetClinic.Mapper
{
    public class PetAutoMapperProfile : Profile
    {
        public PetAutoMapperProfile()
        {
            CreateMap<PostPetDto, Pet>();
            CreateMap<BasePetDto, Pet>()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.BirthYear, opt => opt.Ignore());

            CreateMap<Pet, GetPetDto>()
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.PetBreed, opt => opt.MapFrom(src => src.Breed))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory));
               /* .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Appointments.Select(a => new GetPetsAppointmentsDto
                {
                    AppointmentStartTime = a.StartTime.ToString(),
                    AppointmentEndTime = a.EndTime.ToString(),
                    AppointmentClinicName = a.Clinic.Name,
                    AppointmentVetName = a.Vet.Name
                })));  */
        }
    }
}
