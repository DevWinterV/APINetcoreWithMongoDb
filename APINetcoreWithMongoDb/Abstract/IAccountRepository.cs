using APINetcoreWithMongoDb.Data;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using MongoDB.Driver;

namespace APINetcoreWithMongoDb.@abstract
{
    // Định nghĩa interface IAccountRepo kế thừa từ IRepository
    public interface IAccountRepo : IRepository<AccountRequest, AccountResponse>
    {

    }
}
