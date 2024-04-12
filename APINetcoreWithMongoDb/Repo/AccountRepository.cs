using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Data;
using APINetcoreWithMongoDb.Entities;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using AutoMapper;
using MongoDB.Driver;
using System.Net.WebSockets;

namespace APINetcoreWithMongoDb.Repo
{
    public class AccountRepository : IAccountRepo
    {
        private IMapper _mapper;
        private IMongoCollection<Account> _accoutnCollection;

        public AccountRepository( IMapper mapper, MongoDbService dbService)
        {
            _mapper = mapper;
            _accoutnCollection = dbService.MongoDatabase?.GetCollection<Account>("account");
        }

        public async Task<bool> CreateAsync(AccountRequest request)
        {
            try
            {
                var accountAdd = _mapper.Map<Account>(request);
                await _accoutnCollection.InsertOneAsync(accountAdd);
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(AccountRequest request)
        {
            try
            {
                var checkAccount = Builders<Account>.Filter.Eq(y => y.Id, request.Id);  
                if(checkAccount == null)
                {
                    return false;
                }
                await _accoutnCollection.DeleteOneAsync(checkAccount);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<AccountResponse>> GetAll()
        {
            var listAccount = await _accoutnCollection.Find(_ => true).ToListAsync();
            return listAccount.Select(x => _mapper.Map<AccountResponse>(x)).ToList();
        }

        public async  Task<AccountResponse> GetById(object Id)
        {
            var filter = Builders<Account>.Filter.Eq(y => y.Id, Id);
            var account = await _accoutnCollection.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<List<AccountResponse>> GetByKeyWord(string keyword)
        {
            var filter = Builders<Account>.Filter.Regex(x => x.Username, new MongoDB.Bson.BsonRegularExpression(keyword));
            var listAccount = await _accoutnCollection.Find(filter).ToListAsync();
            return listAccount.Select(x => _mapper.Map<AccountResponse>(x)).ToList();
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
                var result = await _accoutnCollection.UpdateOneAsync(filter, update);
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
