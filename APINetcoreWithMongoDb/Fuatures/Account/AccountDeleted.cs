using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Request;
using AutoMapper;
using Contract;
using MassTransit;

namespace APINetcoreWithMongoDb.Fuatures.Account
{
    public class AccountDeletedConsumer : IConsumer<AccountDeleteEvent>
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public AccountDeletedConsumer(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AccountDeleteEvent> context)
        {
            await _accountRepo.DeleteAsync(_mapper.Map<AccountRequest>(context.Message));
        }
    }
}
