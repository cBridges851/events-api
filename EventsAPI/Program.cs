using EventsAPI.Models;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton(_ = new List<Event> {
    new Event {
        Id = Guid.NewGuid(),
        Name = "Extreme Squirrel Juggling",
        Description = "Meep",
        Date = DateTime.UtcNow,
        EventType = EventType.Online
    }
});

builder.Services.AddHealthChecks();

// NHibernate
var rawConfig = new Configuration();
rawConfig.SetNamingStrategy(new PostgresNamingStrategy());
rawConfig.DataBaseIntegration(x => {
    x.ConnectionString = "Host=localhost;Database=postgres;User ID=postgres;Password=mX8TysRsj90Tc9eXeypK;Enlist=false;";
    x.Dialect<PostgreSQL83Dialect>();
    x.Driver<NpgsqlDriver>();
});
rawConfig.AddAssembly(Assembly.GetExecutingAssembly());
var sessionFactory = rawConfig.BuildSessionFactory();
builder.Services.AddSingleton(sessionFactory);
builder.Services.AddScoped(_ => sessionFactory.OpenSession());


var app = builder.Build();

app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
