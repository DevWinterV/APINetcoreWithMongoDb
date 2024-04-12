using APINetcoreWithSqLServer.@abstract;
using APINetcoreWithSqLServer.Entities;
using APINetcoreWithSqLServer.Request;
using APINetcoreWithSqLServer.Response;
using AutoMapper;

namespace APINetcoreWithSqLServer.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<Account, AccountRequest>().ReverseMap();

        }
    }
}
