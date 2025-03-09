﻿using AutoMapper;
using Hotels.Models.Dtos.Hotel;
using Hotels.Models.Dtos.Manager;
using Hotels.Models.Entities;

namespace Hotels.Service.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelAddingDto, Hotel>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Rating, options => options.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, options => options.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.City));


            CreateMap<Manager, ManagerGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber));


            CreateMap<Hotel, HotelGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Rating, options => options.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, options => options.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.City))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Manager, options => options.MapFrom(src => src.Manager));

            CreateMap<HotelUpdatingDto, Hotel>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Rating, options => options.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title));


        }
    }
}
