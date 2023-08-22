using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NHibernate;
using Microsoft.Extensions.Caching.Distributed;
using EventsAPI.Extensions;
using EventsAPI.Services;

namespace EventsAPI.Controllers {
    [ApiController]
    [Route("Events")]
    public class EventsController : ControllerBase {
        private List<Event> events;
        private IDataService<Event> dataService;

        public EventsController(IDataService<Event> dataService) {
            this.events = new List<Event>();
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<List<Event>> GetEvents() {
            this.events = await this.dataService.GetAll();
            return this.events;
        }

        [HttpGet("{id}")]
        public Event? GetEvent(Guid id) {
            return this.dataService.Get<Event>(x => x.Id, id);
        }

        [HttpPut]
        [Route("Create")]
        public void CreateEvent([FromBody] Event newEvent) {
            this.dataService.Create(newEvent);
            //this.session.Save(newEvent);
            //this.session.Flush();
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage UpdateEvent([FromBody] Event updatedEvent) {
            if (this.dataService.Update(updatedEvent)) { 
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
            //try {
            //    this.session.Update(updatedEvent);
            //    this.session.Flush();
            //} catch (StaleObjectStateException) {
            //    return new HttpResponseMessage {
            //        StatusCode = HttpStatusCode.NotFound
            //    };
            //}

            //return new HttpResponseMessage {
            //    StatusCode = HttpStatusCode.OK
            //};
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage DeleteEvent([FromBody] Guid id) {
            if (this.dataService.Delete(id)) {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);

            //var eventToDelete = this.session.Get<Event>(id);

            //if (eventToDelete is null) {
            //    return new HttpResponseMessage {
            //        StatusCode = HttpStatusCode.NotFound
            //    };
            //}

            //this.session.Delete(eventToDelete);
            //this.session.Flush();

            //return new HttpResponseMessage { 
            //    StatusCode = HttpStatusCode.OK
            //};
        }
    }
}