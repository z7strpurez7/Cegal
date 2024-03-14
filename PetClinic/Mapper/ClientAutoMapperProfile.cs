using AutoMapper;
using PetClinic.Common;
using PetClinic.Dto;
using PetClinic.Model;

namespace PetClinic.Mapper
{
    public class ClientAutoMapperProfile : Profile
    {
        public ClientAutoMapperProfile()
        {
            CreateMap<DateTimeOffset, DateTime>()
                .ConstructUsing((src, ctx) => src.UtcDateTime);
            CreateMap<DateTime, DateTimeOffset>()
                .ConstructUsing((src, ctx) => src.GetDateTimeOffset());

            CreateMap<PostClientDto, Client>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex));

            CreateMap<BaseClientDto, Client>()
             .ForMember(dest => dest.BirthDate, opt => opt.Ignore()) 
             .ForMember(dest => dest.Sex, opt => opt.Ignore());
            //TODO: Add mapping for pets and appointments

            CreateMap<Pet, GetClientPetsDto>()
            .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.PetBreed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory))
            .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments));


            CreateMap<Client, GetClientDto>()
                .ForMember(
                    dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.GetDateTimeOffset()))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => $"{src.StreetAddress}, {src.ZipCode} {src.City}"));
        }
    }
}
