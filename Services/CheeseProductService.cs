using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services.Settings;

namespace PZCheeseriaRestApi.Services
{
    /// <summary>
    /// Service for managing cheese products in the MongoDB database.
    /// </summary>
    public class CheeseProductService
    {
        private readonly IMongoCollection<CheeseProductModel> _cheese_collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheeseProductService"/> class.
        /// </summary>
        /// <param name="mongodb_Settings">MongoDB settings containing database and collection names.</param>
        /// <param name="mongo_client">MongoDB client for database interactions.</param>
        public CheeseProductService(IOptions<MongoDBSettings> mongodb_Settings, IMongoClient mongo_client)
        {
            // Get the database from the MongoDB client using the specified database name
            var mongo_database = mongo_client.GetDatabase(mongodb_Settings.Value.DatabaseName);
            // Get the collection of cheese products from the database
            _cheese_collection = mongo_database.GetCollection<CheeseProductModel>(mongodb_Settings.Value.CollectionName);
        }

        /// <summary>
        /// Retrieves all cheese products from the collection.
        /// </summary>
        /// <returns>A list of cheese product models.</returns>
        public virtual async Task<List<CheeseProductModel>> GetAllAsync() =>
            await _cheese_collection.Find(_ => true).ToListAsync();

        /// <summary>
        /// Retrieves a cheese product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cheese product.</param>
        /// <returns>The cheese product model if found; otherwise, null.</returns>
        public async Task<CheeseProductModel> GetByIdAsync(string id) =>
            await _cheese_collection.Find(x => x._id == id).FirstOrDefaultAsync();

        /// <summary>
        /// Retrieves a cheese product by its name.
        /// </summary>
        /// <param name="cheese_product_name">The name of the cheese product.</param>
        /// <returns>The cheese product model if found; otherwise, null.</returns>
        public async Task<CheeseProductModel> GetByCheeseProductNameAsync(string cheese_product_name) =>
            await _cheese_collection.Find(x => x.cheese_product_name == cheese_product_name).FirstOrDefaultAsync();

        /// <summary>
        /// Creates a new cheese product in the collection.
        /// </summary>
        /// <param name="cheese_model">The cheese product model to create.</param>
        public virtual async Task CreateAsync(CheeseProductModel cheese_model) =>
            await _cheese_collection.InsertOneAsync(cheese_model);

        /// <summary>
        /// Updates an existing cheese product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cheese product to update.</param>
        /// <param name="updated_cheese">The updated cheese product model.</param>
        public virtual async Task UpdateAsync(string id, CheeseProductModel updated_cheese) =>
            await _cheese_collection.ReplaceOneAsync(x => x._id == id, updated_cheese);

        /// <summary>
        /// Deletes a cheese product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cheese product to delete.</param>
        /// <returns>True if the cheese product was successfully deleted; otherwise, false.</returns>
        public virtual async Task<bool> DeleteAsync(string id)
        {
            var result = await _cheese_collection.DeleteOneAsync(x => x._id == id);
            return result.DeletedCount > 0; // Return true if a product was deleted
        }
    }
}

