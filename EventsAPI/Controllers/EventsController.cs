using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NHibernate;

namespace EventsAPI.Controllers {
    [ApiController]
    [Route("Events")]
    public class EventsController : ControllerBase {
        private readonly List<Event> events;
        private NHibernate.ISession session;

        public EventsController(List<Event> events, NHibernate.ISession session) {
            this.events = events;
            this.session = session;
        }

        [HttpGet]
        public List<Event> GetEvents() {
            var events = this.session.CreateCriteria<Event>().List<Event>().ToList();
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

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage DeleteEvent([FromBody] Guid id) {
            var existingEvent = this.events.FirstOrDefault(x => x.Id == id);

            if (existingEvent is null) {
                return new HttpResponseMessage {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            this.events.Remove(existingEvent);

            return new HttpResponseMessage { 
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}