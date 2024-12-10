using EmployeeManagement.RegisterServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Console.WriteLine(Directory.GetCurrentDirectory());


Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(
                           new ConfigurationBuilder()
                           .SetBasePath(
                                     Directory
                                     .GetCurrentDirectory()
                                     )
                           .AddJsonFile("appsettings.json")
                           .Build())
            .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureDatabase();
builder.Services.AddDepandancies();  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.AddMiddleware();

app.MapControllers();

app.Run();
