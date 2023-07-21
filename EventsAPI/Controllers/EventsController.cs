using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NHibernate;

namespace EventsAPI.Controllers {
    [ApiController]
    [Route("Events")]
    public class EventsController : ControllerBase {
        private NHibernate.ISession session;

        public EventsController(NHibernate.ISession session) {
            this.session = session;
        }

        [HttpGet]
        public List<Event> GetEvents() {
            return this.session.CreateCriteria<Event>().List<Event>().ToList();
        }

        [HttpGet("{id}")]
        public Event? GetEvent(Guid id) {
            return this.session.Get<Event>(id); 
        }

        [HttpPut]
        [Route("Create")]
        public void CreateEvent([FromBody] Event newEvent) {
            this.session.Save(newEvent);
            this.session.Flush();
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage UpdateEvent([FromBody] Event updatedEvent) {
            try {
                this.session.Update(updatedEvent);
                this.session.Flush();
            } catch (StaleObjectStateException) {
                return new HttpResponseMessage {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage DeleteEvent([FromBody] Guid id) {
            var eventToDelete = this.session.Get<Event>(id);

            if (eventToDelete is null) {
                return new HttpResponseMessage {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            this.session.Delete(eventToDelete);
            this.session.Flush();

            return new HttpResponseMessage { 
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}