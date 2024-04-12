using APINetcoreWithSqLServer.@abstract;
using APINetcoreWithSqLServer.Request;
using APINetcoreWithSqLServer.Response;
using Microsoft.AspNetCore.Mvc;

namespace APINetcoreWithSqLServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepo _repo;

        public AccountController(ILogger<AccountController> logger, IAccountRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpPost(Name = "CreateAccount")]
        public async Task<bool> CreateAccount(AccountRequest request)
        {
            return await _repo.CreateAsync(request);
        }
        [HttpPut(Name = "UpdateAccount")]
        public async Task<bool> UpdateAccount(AccountRequest request)
        {
            return await _repo.UpdateAsync(request);
        }
        [HttpDelete(Name = "DeleteAccount")]
        public async Task<bool> DeleteAccount(AccountRequest request)
        {
            return await _repo.DeleteAsync(request);
        }
    }
}
