using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPut]
        [Route("Create")]
        public void CreateEvent([FromBody] Event newEvent) {
            this.events.Add(newEvent);
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage UpdateEvent([FromBody] Event updatedEvent) {
            var existingEvent = this.events.FirstOrDefault(x => x.Id == updatedEvent.Id);

            if (existingEvent is null) {
                return new HttpResponseMessage {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            this.events.Remove(existingEvent);
            this.events.Add(updatedEvent);
            return new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}