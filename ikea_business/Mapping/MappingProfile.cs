using AutoMapper;
using ikea_business.DTO;
using ikea_data.Models;

namespace ikea_business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductInput, Product>();
            CreateMap<NewArrivalInput, NewArrival>();
            CreateMap<UserInput, User>()
                .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
                .ForMember(dst => dst.PasswordSalt, opt => opt.Ignore());
        }
    }
}