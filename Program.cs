using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PZCheeseriaRestApi.Services;
using PZCheeseriaRestApi.Services.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings from app settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Register MongoDB client as a singleton service
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    // Retrieve MongoDB settings
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    // Create and return a new MongoClient instance using the connection string
    return new MongoClient(settings.ConnectionString);
});

// Register CheeseService to interact with the cheese collection
builder.Services.AddSingleton<CheeseProductService>();

// Add controllers and endpoints to the service container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    // Define Swagger document with version and description
    c.SwaggerDoc(
    "v1",
    new OpenApiInfo
    {
        Version = "v1.0",
        Title = "pz-cheeseria-rest-api",
        Description = @"The backend API for PZ Cheeseria, a cheese store MVP. 
                        The API provides CRUD operations for managing the store's cheese selection."
    });

    // Include XML comments for Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Configure CORS (Cross-Origin Resource Sharing) policies
builder.Services.AddCors(options =>
{
    // CORS policy for production environment
    options.AddPolicy("ProductionPolicy", builder =>
    {
        builder.WithOrigins("https://segusfaultise.github.io/pz-cheeseria-web-app/")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
    });

    // CORS policy for development environment
    options.AddPolicy("DevelopmentPolicy", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware based on the environment
if (app.Environment.IsDevelopment())
{
    app.UseCors("ProductionPolicy");
    app.UseSwagger(); // Enable Swagger UI in development
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseCors("DevelopmentPolicy");
    app.UseExceptionHandler("/Home/Error"); // Handle exceptions in production
    app.UseHsts(); // Use HTTP Strict Transport Security
}

// Middleware to redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes

// Start the application
app.Run();