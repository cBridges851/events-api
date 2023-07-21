using EventsAPI.Controllers;
using EventsAPI.Models;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class EventsControllerTestBase {
        protected List<Event> Events = new List<Event>();
        protected EventsController? Controller { get; set; }
        protected ISession Session { get; set; }

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
            this.Session = Substitute.For<ISession>();
            this.Controller = new EventsController(this.Events, this.Session);
        }
    }
}
