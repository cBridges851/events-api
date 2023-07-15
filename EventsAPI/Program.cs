using EventsAPI.Models;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
