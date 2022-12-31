using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Mapping;

public class EntitiesToResponseMappingProfile : Profile
{
    public EntitiesToResponseMappingProfile()
    {
        CreateMap<Company, CompanyDTO>();
        CreateMap<Customer, CustomerDTO>();
        CreateMap<Discount,DiscountDTO>();
        CreateMap<Employee,EmployeeDTO>();
        CreateMap<Group,GroupDTO>();
        CreateMap<Inventory, InventoryDTO>();
        CreateMap<Position, PositionDTO>();
        CreateMap<Premise, PremiseDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<RefundTicket, RefundTicketDTO>();
        CreateMap<Reservation, ReservationDTO>();
        CreateMap<Shift, ShiftDTO>();
        CreateMap<Order, OrderDTO>();
        CreateMap<Tax, TaxDTO>();
        CreateMap<Tip, TipDTO>();    
    }
}
