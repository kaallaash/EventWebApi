using Microsoft.EntityFrameworkCore;
using EventWebAPI.DataAccess;
using EventWebAPI.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("PostgreSqlConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(connection);
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddTransient<IDataAccessProvider, DataAccessProvider>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();