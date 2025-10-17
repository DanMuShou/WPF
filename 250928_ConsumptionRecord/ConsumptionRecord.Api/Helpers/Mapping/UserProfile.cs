using AutoMapper;
using ConsumptionRecord.Data.Dto.Users;
using ConsumptionRecord.Data.Entities;

namespace ConsumptionRecord.Api.Helpers.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegisterDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();
        CreateMap<User, UserInfoDto>().ReverseMap();
    }
}
