using AutoMapper;
using PetClinic.Controllers;
using PetClinic.Dto;
using PetClinic.Model;

namespace PetClinic.Mapper
{
    public class ClinicAutoMapperProfile : Profile
    {
        public ClinicAutoMapperProfile()
        {
         
            CreateMap<PostClinicDto, Clinic>()
           .ForMember(dest => dest.OpeningHours, opt => opt.Ignore());

            CreateMap<BaseClinicDto, Clinic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OpeningHours, opt => opt.Ignore());


         
            CreateMap<Clinic, GetClinicDto>()
             .ForMember(dest => dest.StreetAdress, opt => opt.MapFrom(src => src.StreetAdress))
              // Mapping and formatting OperatingHours
              .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom(src => src.OpeningHours
                .Select(oh => new GetClinicOperatingHoursDto
                {
                    DayOfWeek = oh.DayOfWeek, 
                    OpeningTime = oh.OpeningTime.ToString("dddd HH:mm"),
                    ClosingTime = oh.ClosingTime.ToString("dddd HH:mm")
                })));

            
            CreateMap<OpeningHour, OperatingHourDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                .ForMember(dest => dest.OpeningTime, opt => opt.MapFrom(src => src.OpeningTime))
                .ForMember(dest => dest.ClosingTime, opt => opt.MapFrom(src => src.ClosingTime));

            CreateMap<OperatingHourDto, OpeningHour>();
        }
    }
}
