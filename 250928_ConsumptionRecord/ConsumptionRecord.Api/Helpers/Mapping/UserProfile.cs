using AutoMapper;
using ConsumptionRecord.Data.Dto;
using ConsumptionRecord.Data.Entities;

namespace ConsumptionRecord.Api.Helpers.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoDto>().ReverseMap();
    }
}
