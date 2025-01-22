using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Middleware;
using FinalAssignmentBE.Repositories;
using FinalAssignmentBE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<FinalAssignmentDbContext>(options =>
    {
        // Fetch the connection string from configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Validate if the connection string exists
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not defined.");
        }

        // Configure Entity Framework Core with Npgsql provider
        options.UseNpgsql(connectionString, npgsqlOptions =>
        {
            // Optional: Enable logging of sensitive data during development
            npgsqlOptions.EnableRetryOnFailure(); // Enables retry for transient failures
        });
    }
);


// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();



builder.Services.AddHttpClient();

var app = builder.Build();

// Configure middleware
var errorHandlingMiddleware = new ErrorHandlingMiddleware();

// Use the middleware
app.Use(async (context, next) => await errorHandlingMiddleware.InvokeAsync(context, next));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"); });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();