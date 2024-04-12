using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Request;
using AutoMapper;
using Contract;
using MassTransit;
using MassTransit.Testing;

namespace APINetcoreWithMongoDb.Fuatures.Account
{
    public class AccountCreatedConsumer : IConsumer<AccountCreateEvent>
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public AccountCreatedConsumer(IAccountRepo accountRepo, IMapper mapper) { 
            _accountRepo = accountRepo;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AccountCreateEvent> context)
        {
            try
            {
                await _accountRepo.CreateAsync(_mapper.Map<AccountRequest>(context.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
