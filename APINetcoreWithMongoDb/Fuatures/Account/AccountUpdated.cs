using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Request;
using AutoMapper;
using Contract;
using MassTransit;

namespace APINetcoreWithMongoDb.Fuatures.Account
{
    public class AccountUpdatedConsumer : IConsumer<AccountUpdateEvent>
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public AccountUpdatedConsumer(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AccountUpdateEvent> context)
        {
            await _accountRepo.UpdateAsync(_mapper.Map<AccountRequest>(context.Message));
        }
    }
}
