using APINetcoreWithMongoDb.Data;
using APINetcoreWithMongoDb.Entities;
using APINetcoreWithMongoDb.Request;
using APINetcoreWithMongoDb.Response;
using MongoDB.Bson.Serialization.Attributes;

namespace APINetcoreWithMongoDb.@abstract
{
    // Định nghĩa interface IRepository
    public interface IRepository<Request, Response> where Response : DTOResponse where Request : DTORequest
    {
        Task<bool> CreateAsync(Request request);
        Task<bool> UpdateAsync(Request request);
        Task<bool> DeleteAsync(Request request);
        Task<List<Response>> GetAll();
        Task<Response> GetById(object Id);
        Task<List<Response>> GetByKeyWord(string keyword);
    }

}
