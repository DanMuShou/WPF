using AutoMapper;
using ConsumptionRecord.Data.Dto.Records;
using ConsumptionRecord.Data.Entities;

namespace ConsumptionRecord.Api.Helpers.Mapping;

public class WaitProfile : Profile
{
    public WaitProfile()
    {
        CreateMap<Wait, WaitInfoDto>().ReverseMap();
    }
}
