using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PZCheeseriaRestApi.Controllers;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services;
using PZCheeseriaRestApi.Services.Settings;
using Xunit;


public class CheeseProductServiceTests
{
    private readonly CheeseProductController _controller;
    private readonly CheeseProductService _service;
    private readonly IMongoCollection<CheeseProductModel> _collection;

    public CheeseProductServiceTests()
    {
        var mongoDbSettings = new MongoDBSettings
        {
            ConnectionString = "mongodb://root:example@127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&authSource=admin&appName=mongosh+2.3.2",
            DatabaseName = "pz-cheeseria-database",
            CollectionName = "cheeseProductsTest"
        };

        var options = Options.Create(mongoDbSettings);
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.DatabaseName);

        _collection = database.GetCollection<CheeseProductModel>(mongoDbSettings.CollectionName);
        _service = new CheeseProductService(options, client);
        _controller = new CheeseProductController(_service);

        SeedDatabase();
    }

    internal void SeedDatabase()
    {
        _collection.DeleteMany(_ => true); 

        var cheeseProducts = new List<CheeseProductModel>
        {
            new CheeseProductModel
            {
                _id = "67134fefc40949aab6fe6911",
                cheese_product_name = "Swiss",
                cheese_product_price_per_kilo = 11.5,
                cheese_product_color = "Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/Swiss.jpg",
                cheese_product_origin = "Swiss",
                cheese_product_stock = 30,
                created_at = "10/19/2024 2:00:36 AM",
                updated_at = "10/19/2024 2:00:36 AM",
            },
            new CheeseProductModel
            {
                _id = "67134fefc40949aab6fe6912",
                cheese_product_name = "Gouda",
                cheese_product_price_per_kilo = 15.5,
                cheese_product_color = "Mild Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/Gouda.jpg",
                cheese_product_origin = "Netherlands",
                cheese_product_stock = 20,
                created_at = "10/19/2024 2:00:37 AM",
                updated_at = "10/19/2024 2:00:37 AM",
            },
        };

        _collection.InsertMany(cheeseProducts);
    }

    [Fact]
    public async Task DeleteCheeseProduct_ReturnsBadRequest_WhenProductDoesNotExist()
    {
        var id = "67134fefc40949aab6f00000";

        var result = await _service.DeleteAsync(id);
        var badRequestResult = Assert.IsType<bool>(result);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteCheeseProduct_ReturnsOk_WhenProductExists()
    {
        var id = "67134fefc40949aab6fe6911";
        var result = await _service.DeleteAsync(id);
        var okResult = Assert.IsType<bool>(result);
        var deletedProduct = await _service.GetByIdAsync(id);

        Assert.True(result);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task GetCheeseProduct_ReturnsOk_WhenProductExists()
    {
        var id = "67134fefc40949aab6fe6911";

        var result = await _service.GetByIdAsync(id);

        Assert.NotNull(result);

        var okResult = Assert.IsType<CheeseProductModel>(result);
        var cheeseProduct = Assert.IsType<CheeseProductModel>(okResult);

        Assert.Equal(id, cheeseProduct._id);
        Assert.Equal("Swiss", cheeseProduct.cheese_product_name);
    }

    [Fact]
    public async Task GetCheeseProductByName_ReturnsOk_WhenProductExists()
    {
        var cheese_product_name = "Swiss";
        var result = await _service.GetByCheeseProductNameAsync(cheese_product_name);

        Assert.NotNull(result);

        var okResult = Assert.IsType<CheeseProductModel>(result);
        var cheeseProduct = Assert.IsType<CheeseProductModel>(okResult);

        Assert.Equal(cheese_product_name, cheeseProduct.cheese_product_name);
    }
}

