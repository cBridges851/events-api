using EventsAPI.Controllers;
using EventsAPI.Models;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class AndGettingEvents : EventsControllerTestBase {

        [Test]
        public async Task WhenNoIdIsPassedThenItShouldReturnAllEvents() {
            var returnedEvents = await this.Controller.GetEvents();
            Assert.IsNotNull(returnedEvents);
            Assert.AreEqual(3, returnedEvents.Count);
        }

        [Test]
        public void WhenAnIdIsPassedThenItShouldReturnTheCorrectEvent() {
            var expectedEvent = this.Events[1];
            var returnedEvent = this.Controller?.GetEvent(expectedEvent.Id);
            Assert.IsNotNull(returnedEvent);
            Assert.AreEqual(expectedEvent.Id, returnedEvent?.Id);
            Assert.AreEqual(expectedEvent.Name, returnedEvent?.Name);
            Assert.AreEqual(expectedEvent.Description, returnedEvent?.Description);
            Assert.AreEqual(expectedEvent.EventType, returnedEvent?.EventType);
            Assert.AreEqual(expectedEvent.Date, returnedEvent?.Date);
        }

        [Test]
        public void WhenAnIdIsPassedButTheEventDoesNotExistThenItShouldReturnNull() {
            var returnedEvent = this.Controller?.GetEvent(Guid.NewGuid());
            Assert.IsNull(returnedEvent);
        }
    }
}
