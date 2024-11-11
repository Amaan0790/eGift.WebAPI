using eGift.WebAPI.Middlewares;
using eGift.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

#region Configure Services

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Optional: Configure JSON options as needed
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use camelCase if needed
    });

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.CommandTimeout(120); // Set command timeout to 120 seconds
    }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#endregion

#region Configure

// Register your custom middleware here
app.UseMiddleware<DefaultAdminUserMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion