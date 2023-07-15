using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers {
    [ApiController]
    [Route("Events")]
    public class EventsController : ControllerBase {
        private readonly List<Event> events;
        public EventsController(List<Event> events) {
            this.events = events;
        }

        [HttpGet]
        public List<Event> GetEvents() {
            return this.events;
        }

        [HttpGet("{id}")]
        public Event? GetEvent(Guid id) {
            return this.events.FirstOrDefault(x => x.Id == id);
        }
    }
}