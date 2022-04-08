using Microsoft.EntityFrameworkCore;
using EventWebAPI.DataAccess;
using EventWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("PostgreSqlConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(connection);
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddTransient<IDataAccessProvider, DataAccessProvider>();
builder.Services.AddSingleton<IEventAPIMapperService, EventAPIMapperService>();
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