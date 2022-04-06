using Microsoft.EntityFrameworkCore;
using EventWebAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddTransient<IDataAccessProvider, DataAccessProvider>();
builder.Services.AddControllers();

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

app.MapControllers();

app.Run();