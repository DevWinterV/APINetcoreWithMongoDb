using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Entities;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using AutoMapper;
using Contract;

namespace APINetcoreWithMongoDb.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<Account, AccountRequest>().ReverseMap();
            //
            CreateMap<AccountCreateEvent, AccountRequest>().ReverseMap();
            CreateMap<AccountDeleteEvent, AccountRequest>().ReverseMap();
            CreateMap<AccountUpdateEvent, AccountRequest>().ReverseMap();

        }
    }
}
