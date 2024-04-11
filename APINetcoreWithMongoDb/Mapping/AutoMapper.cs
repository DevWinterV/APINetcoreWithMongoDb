using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Entities;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using AutoMapper;

namespace APINetcoreWithMongoDb.Mapping
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
