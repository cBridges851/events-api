using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage UpdateEvent([FromBody] Event updatedEvent) {
            if (this.dataService.Update(updatedEvent)) { 
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage DeleteEvent([FromBody] Guid id) {
            if (this.dataService.Delete(id)) {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}