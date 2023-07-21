using EventsAPI.Models;
using NUnit.Framework;
using System.Net;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class AndDeletingAnEvent : EventsControllerTestBase {
        // Events should not contain event with a given id, and the length should have decreased
        // If event with id not found, the length of the events list should stay the same and endpoint should send 404
        [Test]
        public void WhenTheEventExistsThenItShouldDeleteSuccessfully() {
            var existingEvent = this.Events.First();
            var result = this.Controller?.DeleteEvent(existingEvent.Id);
            var allEvents = this.Controller?.GetEvents();

            Assert.AreEqual(result?.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(2, allEvents?.Count);
            var containsEvent = this.Events.Any(x => x.Id == existingEvent.Id);
            Assert.IsFalse(containsEvent);
        }

        [Test]
        public void WhenTheEventDoesNotExistThenItShouldReturnNotFound() {
            var nonExistentEventId = Guid.NewGuid();
            var result = this.Controller?.DeleteEvent(nonExistentEventId);
            var allEvents = this.Controller?.GetEvents();
            Assert.AreEqual(HttpStatusCode.NotFound, result?.StatusCode);
            Assert.AreEqual(3, allEvents?.Count);
            var containsEvent = this.Events.Any(x => x.Id == nonExistentEventId);
            Assert.IsFalse(containsEvent);
        }
    }
}
