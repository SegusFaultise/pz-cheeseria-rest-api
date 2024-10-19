using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PZCheeseriaRestApi.Services;
using PZCheeseriaRestApi.Services.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable CORS with a policy that allows all origins, headers, and methods.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAzureApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddCors(
options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("https://www.google.com/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
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
