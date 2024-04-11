using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Data;
using APINetcoreWithMongoDb.Entities;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.Driver;

namespace APINetcoreWithMongoDb.Repo
{
    public class AccountRepository : IAccountRepo
    {
        private readonly IMongoCollection<Account> _account;
        private IMapper _mapper;
        public AccountRepository(MongoDbService mongoDbService, IMapper mapper)
        {
            _account = mongoDbService.MongoDatabase?.GetCollection<Account>("account");
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(AccountRequest request)
        {
            try
            {
                var account = _mapper.Map<Account>(request);
                await _account.InsertOneAsync(account);
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây
                return false;
            }
        }

        public async Task<bool> DeleteAsync(AccountRequest request)
        {
            try
            {
                var filter = Builders<Account>.Filter.Eq(x => x.Id, request.Id);
                var result = await _account.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây
                return false;
            }
        }

        public async Task<List<AccountResponse>> GetAll()
        {
            var listAccount = await _account.Find(_ => true).ToListAsync();
            return _mapper.Map<List<AccountResponse>>(listAccount);
        }

        public async Task<AccountResponse> GetById(object Id)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Id, Id);
            var account = await _account.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<List<AccountResponse>> GetByKeyWord(string keyword)
        {
            var filter = Builders<Account>.Filter.Regex(x => x.Username, new MongoDB.Bson.BsonRegularExpression(keyword));
            var listAccount = await _account.Find(filter).ToListAsync();
            return _mapper.Map<List<AccountResponse>>(listAccount);
        }

        public async Task<bool> UpdateAsync(AccountRequest request)
        {
            try
            {
                var filter = Builders<Account>.Filter.Eq(x => x.Id, request.Id);
                var update = Builders<Account>.Update
                    .Set(x => x.UserId, request.UserId)
                    .Set(x => x.Username, request.Username)
                    .Set(x => x.Password, request.Password)
                    .Set(x => x.Status, request.Status)
                    .Set(x => x.AccountId, request.AccountId);

                var result = await _account.UpdateOneAsync(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây
                return false;
            }
        }
    }
}
