using EventsAPI.Controllers;
using EventsAPI.Models;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class AndGettingEvents {

        private List<Event> Events = new List<Event>();
        private EventsController? Controller { get; set; }

        [SetUp]
        public void Setup() {
            this.Events = new List<Event> {
               new Event {
                    Id = Guid.NewGuid(),
                    Date = new DateTime(2023, 4, 5),
                    Name = "Cat Lovers Anonymous Meetup",
                    Description = "A meeting for people who love cats to talk about cats, obviously.",
                    EventType = EventType.Online
               },
               new Event {
                    Id = Guid.NewGuid(),
                    Date = new DateTime(2023, 7, 7),
                    Name = "Annual Badger Throwing Contest",
                    Description = "A competition that happens every year to see who is the best badger thrower. No badgers are harmed in this event.",
                    EventType = EventType.InPerson
               },
               new Event {
                    Id = Guid.NewGuid(),
                    Date= new DateTime(2023, 5, 1),
                    Name = "Extreme Squirrel Juggling",
                    Description = "A gathering of people who find that juggling balls are overrated, so choose to juggle squirrels instead.",
                    EventType= EventType.InPerson
               }
            };
            this.Controller = new EventsController(this.Events);
        }

        [Test]
        public void WhenNoIdIsPassedThenItShouldReturnAllEvents() {
            var returnedEvents = this.Controller?.GetEvents();
            Assert.IsNotNull(returnedEvents);
            Assert.AreEqual(3, returnedEvents?.Count);
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
