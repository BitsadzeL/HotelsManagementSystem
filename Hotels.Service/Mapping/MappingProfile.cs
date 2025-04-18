﻿using AutoMapper;
using Hotels.Models.Dtos.Bookings;
using Hotels.Models.Dtos.Guests;
using Hotels.Models.Dtos.Hotel;
using Hotels.Models.Dtos.Identity;
using Hotels.Models.Dtos.Manager;
using Hotels.Models.Dtos.Managers;
using Hotels.Models.Dtos.Reservations;
using Hotels.Models.Dtos.Rooms;
using Hotels.Models.Entities;
using Microsoft.AspNetCore.Identity;

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





            CreateMap<Manager, ManagerGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.HotelId, options => options.MapFrom(src => src.HotelId));

            CreateMap<ManagerAddingDto, Manager>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.HotelId, options => options.MapFrom(src => src.HotelId));

            CreateMap<ManagerUpdatingDto, Manager>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.HotelId, options => options.MapFrom(src => src.HotelId));





            CreateMap<Room, RoomGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsFree, options => options.MapFrom(src => src.IsFree))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dest => dest.Reservations, options => options.MapFrom(src => src.Reservations));


            CreateMap<RoomAddingDto, Room>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsFree, options => options.MapFrom(src => src.IsFree))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dest => dest.HotelId, options => options.MapFrom(src => src.HotelId));

            CreateMap<RoomUpdatingDto,Room>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsFree, options => options.MapFrom(src => src.IsFree))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dest => dest.HotelId, options => options.MapFrom(src => src.HotelId));









            CreateMap<Guest, GuestGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Bookings, options => options.MapFrom(src => src.Bookings));


            CreateMap<GuestAddingDto,Guest>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber));


            CreateMap<GuestUpdatingDto,Guest>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, options => options.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IdNumber, options => options.MapFrom(src => src.IdNumber))
                .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber));




            CreateMap<Reservation,ReservationGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CheckIn, options => options.MapFrom(src => src.CheckIn))
                .ForMember(dest => dest.CheckOut, options => options.MapFrom(src => src.CheckOut))
                .ForMember(dest => dest.RoomId, options => options.MapFrom(src => src.RoomId));

            CreateMap<ReservationAddingDto, Reservation>()
                .ForMember(dest => dest.CheckIn, options => options.MapFrom(src => src.CheckIn))
                .ForMember(dest => dest.CheckOut, options => options.MapFrom(src => src.CheckOut))
                .ForMember(dest => dest.RoomId, options => options.MapFrom(src => src.RoomId));

            CreateMap<ReservationUpdatingDto, Reservation>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CheckIn, options => options.MapFrom(src => src.CheckIn))
                .ForMember(dest => dest.CheckOut, options => options.MapFrom(src => src.CheckOut))
                .ForMember(dest => dest.RoomId, options => options.MapFrom(src => src.RoomId));





            CreateMap<Booking,BookingGettingDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.GuestId, options => options.MapFrom(src => src.GuestId))
                .ForMember(dest => dest.ReservationId, options => options.MapFrom(src => src.ReservationId));


            CreateMap<BookingAddingDto, Booking>()
                .ForMember(dest => dest.GuestId, options => options.MapFrom(src => src.GuestId))
                .ForMember(dest => dest.ReservationId, options => options.MapFrom(src => src.ReservationId));

            CreateMap<BookingUpdatingDto,Booking>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.GuestId, options => options.MapFrom(src => src.GuestId))
                .ForMember(dest => dest.ReservationId, options => options.MapFrom(src => src.ReservationId));









            CreateMap<GuestRegistrationDto, IdentityUser<int>>()
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, options => options.MapFrom(src => src.Email.ToUpper()));

            CreateMap<ManagerRegistrationDto, IdentityUser<int>>()
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, options => options.MapFrom(src => src.Email.ToUpper()));

            CreateMap<AdminRegistrationDto, IdentityUser<int>>()
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, options => options.MapFrom(src => src.Email.ToUpper()));






        }
    }
}
