using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services.Settings;

namespace PZCheeseriaRestApi.Services
{
    public class CheeseProductService
    {
        private readonly IMongoCollection<CheeseProductModel> _cheese_collection;

        public CheeseProductService(IOptions<MongoDBSettings> mongodb_Settings, IMongoClient mongo_client)
        {
            var mongo_database = mongo_client.GetDatabase(mongodb_Settings.Value.DatabaseName);
            _cheese_collection = mongo_database.GetCollection<CheeseProductModel>(mongodb_Settings.Value.CollectionName);
        }

        public async Task<List<CheeseProductModel>> GetAllAsync() =>
            await _cheese_collection.Find(_ => true).ToListAsync();

        public async Task<CheeseProductModel?> GetByIdAsync(string id) =>
            await _cheese_collection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CheeseProductModel cheese_model) =>
            await _cheese_collection.InsertOneAsync(cheese_model);

        public async Task UpdateAsync(string id, CheeseProductModel updated_cheese) =>
            await _cheese_collection.ReplaceOneAsync(x => x.id == id, updated_cheese);

        public async Task DeleteAsync(string id) =>
            await _cheese_collection.DeleteOneAsync(x => x.id == id);
    }
}
