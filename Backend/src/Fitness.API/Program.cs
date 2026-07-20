using Fitness.API.ExceptionHandlers;
using Fitness.API.Extensions;
using Fitness.Application;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDatabase(builder.Configuration);

// Redis
builder.Services.AddRedis(builder.Configuration);

// Dependency Injection
builder.Services.AddApplicationServices();

// JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// MVC
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
builder.Services.AddFluentValidationAutoValidation();

// Build
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();