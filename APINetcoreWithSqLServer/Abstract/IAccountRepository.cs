using APINetcoreWithSqLServer.Data;
using APINetcoreWithSqLServer.Request;
using APINetcoreWithSqLServer.Response;

namespace APINetcoreWithSqLServer.@abstract
{
    // Định nghĩa interface IAccountRepo kế thừa từ IRepository
    public interface IAccountRepo : IRepository<AccountRequest, AccountResponse>
    {

    }
}
