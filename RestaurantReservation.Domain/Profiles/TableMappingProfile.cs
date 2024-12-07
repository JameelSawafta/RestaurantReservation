using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.Domain.Profiles;

public class TableMappingProfile : Profile
{
    public TableMappingProfile()
    {
        CreateMap<Table, TableDto>();
        CreateMap<CreateTableDto, Table>();
        CreateMap<UpdateTableDto, Table>();
    }
}