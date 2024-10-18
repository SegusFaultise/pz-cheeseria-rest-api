using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services.Settings;

namespace PZCheeseriaRestApi.Services
{
    public class CheeseService
    {
        private readonly IMongoCollection<CheeseModel> _cheese_collection;

        public CheeseService(IOptions<MongoDBSettings> mongodb_Settings, IMongoClient mongo_client)
        {
            var mongo_database = mongo_client.GetDatabase(mongodb_Settings.Value.database_name);
            _cheese_collection = mongo_database.GetCollection<CheeseModel>(mongodb_Settings.Value.collection_name);
        }

        public async Task<List<CheeseModel>> GetAllAsync() =>
            await _cheese_collection.Find(_ => true).ToListAsync();

        public async Task<CheeseModel?> GetByIdAsync(string id) =>
            await _cheese_collection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CheeseModel cheese_model) =>
            await _cheese_collection.InsertOneAsync(cheese_model);

        public async Task UpdateAsync(string id, CheeseModel updated_cheese) =>
            await _cheese_collection.ReplaceOneAsync(x => x.id == id, updated_cheese);

        public async Task DeleteAsync(string id) =>
            await _cheese_collection.DeleteOneAsync(x => x.id == id);
    }
}
