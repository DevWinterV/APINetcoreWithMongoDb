using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Driver;

namespace APINetcoreWithMongoDb.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongoDbConnections");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        
        public IMongoDatabase? MongoDatabase { get { return _mongoDatabase; } }
    }
}
