using APINetcoreWithMongoDb.@abstract;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using Microsoft.AspNetCore.Mvc;

namespace APINetcoreWithMongoDb.Controllers
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
        [HttpGet(Name = "GetAllAccount")]
        public async Task<List<AccountResponse>> GetAllAccount()
        {
            return await _repo.GetAll();
        }


        [HttpGet("id/{Id}", Name = "GetByID")]
        public async Task<AccountResponse> GetByID(int Id)
        {
            return await _repo.GetById(Id);
        }

        [HttpGet("keyword/{keyword}", Name = "GetByKeyWord")]
        public async Task<List<AccountResponse>> GetByKeyWord(string keyword)
        {
            return await _repo.GetByKeyWord(keyword);
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
