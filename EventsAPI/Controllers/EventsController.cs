using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NHibernate;
using Microsoft.Extensions.Caching.Distributed;
using EventsAPI.Extensions;

namespace EventsAPI.Controllers {
    [ApiController]
    [Route("Events")]
    public class EventsController : ControllerBase {
        private NHibernate.ISession session;
        private IDistributedCache cache;
        private List<Event> events;

        public EventsController(NHibernate.ISession session, IDistributedCache cache) {
            this.session = session;
            this.cache = cache;
            this.events = new List<Event>();
        }

        [HttpGet]
        public async Task<List<Event>> GetEvents() {
            var recordKey = $"Events_{DateTime.UtcNow.ToString("yyyyMMdd_hhmm")}";
            var cachedEvents = await cache.GetRecordAsync<List<Event>>(recordKey);

            if (cachedEvents is null) { 
                this.events = this.session.CreateCriteria<Event>().List<Event>().ToList();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Loaded from database!");
                await cache.SetRecordAsync(recordKey, this.events);
                return this.events;
            }

            this.events = cachedEvents;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Loaded from cache!");

            return this.events;
        }

        [HttpGet("{id}")]
        public Event? GetEvent(Guid id) {
            this.events = this.session.CreateCriteria<Event>().List<Event>().ToList();
            return this.events.FirstOrDefault(x => x.Id == id); 
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