using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Model;

namespace CustomerService.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<Customer, CustomerCreateDto>();

        }
    }
}
