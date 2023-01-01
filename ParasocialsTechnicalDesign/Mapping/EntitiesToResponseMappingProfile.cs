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
        CreateMap<Discount,DiscountDTO>().ForMember(x => x.Group, opt => opt.MapFrom(src => src.Group.GroupName));
        CreateMap<Employee,EmployeeDTO>().ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.Title));
        CreateMap<Group,GroupDTO>();
        CreateMap<Inventory, InventoryDTO>();
        CreateMap<Position, PositionDTO>();
        CreateMap<Premise, PremiseDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<RefundTicket, RefundTicketDTO>();
        CreateMap<Reservation, ReservationDTO>().ForMember(x => x.Premise, opt => opt.MapFrom(src => src.Premise.Location));
        CreateMap<Shift, ShiftDTO>();
        CreateMap<Order, OrderDTO>();
        CreateMap<Tax, TaxDTO>();
        CreateMap<Tip, TipDTO>().ForMember(x => x.Receiver, opt => opt.MapFrom(src => src.Receiver.Id));    
    }
}
