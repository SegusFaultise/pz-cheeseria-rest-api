using MongoDB.Driver;
using MongoDB.Bson;
using Xunit;
using System.Text.Json;

public class LocalTestingMongoConnectionTests
{
    private readonly IMongoDatabase _database;

    public LocalTestingMongoConnectionTests()
    {
        var client = new MongoClient("mongodb://root:example@127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&authSource=admin&appName=mongosh+2.3.2");
        _database = client.GetDatabase("pz-cheeseria-database");
    }

    [Fact]
    public void TestMongoConnection_CanInsertAndRetrieveData()
    {
        var collection = _database.GetCollection<BsonDocument>("test-collection");

        collection.DeleteMany(FilterDefinition<BsonDocument>.Empty);

        var document = new BsonDocument { { "name", "test_item" }, { "value", 42 } };

        collection.InsertOne(document);

        var filter = Builders<BsonDocument>.Filter.Eq("name", "test_item");
        var retrievedDocument = collection.Find(filter).FirstOrDefault();

        Assert.NotNull(retrievedDocument);
        Assert.Equal("test_item", retrievedDocument["name"]);
        Assert.Equal(42, retrievedDocument["value"].AsInt32);
    }
}
