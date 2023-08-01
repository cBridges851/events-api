using EventsAPI.Migration;
using EventsAPI.Models;
using FluentMigrator.Runner;
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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
rawConfig.SetNamingStrategy(new PostgresNamingStrategy());

var serviceProvider = builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(AddEventTable).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope()) {
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

rawConfig.DataBaseIntegration(x => {
    x.ConnectionString = connectionString;
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
