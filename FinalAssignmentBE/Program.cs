using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Middleware;
using FinalAssignmentBE.Repositories;
using FinalAssignmentBE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // ✅ Allow Next.js Frontend
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // ✅ Important if using authentication cookies
        });
});
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
builder.Services.AddScoped<IGameAttemptRepository, GameAttemptRepository>();
builder.Services.AddScoped<IGameAttemptService, GameAttemptService>();
builder.Services.AddScoped<IGameRuleRepository, GameRuleRepository>();
builder.Services.AddScoped<IGameRuleService, GameRuleService>();
builder.Services.AddScoped<IGameQuestionRepository, GameQuestionRepository>();

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

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();