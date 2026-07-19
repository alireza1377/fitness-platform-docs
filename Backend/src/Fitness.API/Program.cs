using Fitness.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Build App
var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication
// app.UseAuthentication();
// app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();