using EventsAPI.Models;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class AndCreatingAnEvent : EventsControllerTestBase {
        [Test]
        public async Task WhenAnEventIsGivenItShouldBeSaved() {
            var name = "World Domination Plotting";
            var description = "Figuring out how to take over the world, " +
                "so everyone will wear cat ears and there will be glitter everywhere";
            var date = new DateTime(2023, 3, 3);
            var type = EventType.Hybrid;
            var newEvent = new Event {
                Name = name,
                Description = description,
                Date = date,
                EventType = type
            };

            this.Controller.CreateEvent(newEvent);
            var allEvents = await this.Controller.GetEvents();
            var retrievedEvent = allEvents?.FirstOrDefault(x => x.Name == name);
            Assert.AreEqual(4, allEvents?.Count);
            Assert.NotNull(retrievedEvent?.Id);
            Assert.AreEqual(name, retrievedEvent?.Name);
            Assert.AreEqual(description, retrievedEvent?.Description);
            Assert.AreEqual(date, retrievedEvent?.Date);
            Assert.AreEqual(type, retrievedEvent?.EventType);
        }
    }
}
