namespace PZCheeseriaRestApi.Services.Settings
{
    /// <summary>
    /// Represents the settings required to connect to a MongoDB database.
    /// </summary>
    public class MongoDBSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to connect to the MongoDB instance.
        /// </summary>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the database in MongoDB.
        /// </summary>
        public required string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the collection within the database.
        /// </summary>
        public required string CollectionName { get; set; }
    }
}
