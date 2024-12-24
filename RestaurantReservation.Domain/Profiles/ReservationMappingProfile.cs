using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Profiles;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Reservation, ReservationDto>(); 
    }
}