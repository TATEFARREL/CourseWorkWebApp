using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<City, CityDto>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
            .ReverseMap();
        CreateMap<Attraction, AttractionDto>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
            .ReverseMap();
        CreateMap<Bus, BusDto>().ReverseMap();
        CreateMap<Tour, TourDto>().ReverseMap();
        CreateMap<TourApplication, TourApplicationDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Name))
            .ReverseMap();
        CreateMap<Voucher, VoucherDto>()
            .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.TourApplication.Tour.Name))
            .ForMember(dest => dest.TourStartDate, opt => opt.MapFrom(src => src.TourApplication.Tour.StartDate))
            .ReverseMap();
        CreateMap<TourAttractionDto, TourAttraction>()
                .ForMember(dest => dest.Tour, opt => opt.Ignore())
                .ForMember(dest => dest.Attraction, opt => opt.Ignore())
                .ReverseMap();
        CreateMap<Tour, TourDetailsDto>()
                .ForMember(dest => dest.Bus, opt => opt.Ignore())
                .ForMember(dest => dest.Attractions, opt => opt.Ignore());
        CreateMap<TourApplication, AdminTourApplicationDto>()
    .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
    .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Name))
    .ForMember(dest => dest.HasVoucher, opt => opt.MapFrom(src => src.Voucher != null));


    }
}