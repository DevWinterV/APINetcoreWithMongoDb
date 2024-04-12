using APINetcoreWithSqLServer.@abstract;
using APINetcoreWithSqLServer.Data;
using APINetcoreWithSqLServer.Entities;
using APINetcoreWithSqLServer.Request;
using APINetcoreWithSqLServer.Response;
using AutoMapper;
using Contract;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.EntityFrameworkCore;

namespace APINetcoreWithSqLServer.Repo
{
    public class AccountRepository : IAccountRepo
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appContext;
        private readonly IPublishEndpoint _publishEndpoint;


        public AccountRepository(IMapper mapper, AppDbContext appContext, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _appContext = appContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> CreateAsync(AccountRequest request)
        {
            try
            {
                var accountAdd = _mapper.Map<Account>(request);
                _appContext.Accounts.Add(accountAdd);
                await _appContext.SaveChangesAsync();
                await _publishEndpoint.Publish(new AccountCreateEvent
                {
                    Id = accountAdd.Id,
                    AccountId = accountAdd.AccountId,
                    UserId = accountAdd.UserId,
                    Username = accountAdd.Username,
                    Password = accountAdd.Password,
                    Status = accountAdd.Status
                });
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(AccountRequest request)
        {
            try
            {
                var account = await _appContext.Accounts.FindAsync(request.Id);
                if (account == null)
                {
                    return false;

                }
                 _appContext.Accounts.Remove(account);
                 await _appContext.SaveChangesAsync();
                await _publishEndpoint.Publish(new AccountDeleteEvent
                {
                    Id = request.Id,

                });
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<AccountResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetById(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountResponse>> GetByKeyWord(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(AccountRequest request)
        {
            try
            {
                _appContext.Accounts.Update(_mapper.Map<Account>(request));
                await _appContext.SaveChangesAsync();
                await _publishEndpoint.Publish(new AccountUpdateEvent
                {
                    Id = request.Id,
                    AccountId = request.AccountId,
                    UserId = request.UserId,
                    Username = request.Username,
                    Password = request.Password,
                    Status = request.Status
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
