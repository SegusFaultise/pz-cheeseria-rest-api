using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PZCheeseriaRestApi.Services;
using PZCheeseriaRestApi.Services.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Register CheeseService to interact with the cheese collection
builder.Services.AddSingleton<CheeseProductService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
    "v1",
    new OpenApiInfo
    {
        Version = "v1.0",
        Title = "pz-cheeseria-rest-api",
        Description = @"The backend API for PZ Cheeseria, a cheese store MVP. 
                        The API provides CRUD operations for managing the store's cheese selection."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    //c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(
options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins(
            "https://localhost:7281")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseExceptionHandler("/Home/Error");
//app.UseHsts();

app.UseCors(
builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
