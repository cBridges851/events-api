using EventsAPI.Controllers;
using EventsAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class EventsControllerTestBase {
        protected List<Event> Events = new List<Event>();

        protected IDistributedCache? Cache { get; private set; }
        protected ISession? Session { get; set; }
        protected EventsController Controller { get; set; }

        protected EventsControllerTestBase() {
            this.Controller = new EventsController(Substitute.For<ISession>(), Substitute.For<IDistributedCache>());
        }

        [SetUp]
        public void BaseSetup() {
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
            this.Session.CreateCriteria<Event>().List<Event>().Returns(this.Events);
            this.Session.Get<Event>(Arg.Any<Guid>()).Returns((arg) => this.Events.FirstOrDefault(x => x.Id == arg.ArgAt<Guid>(0)));
            this.Session.When(x => x.Save(Arg.Any<Event>())).Do(arg => this.Events.Add(arg.ArgAt<Event>(0)));
            this.Session.When(x => x.Update(Arg.Any<Event>())).Do(arg => {
                var updatedEvent = arg.ArgAt<Event>(0);
                var existingEvent = this.Events.FirstOrDefault(x => x.Id == updatedEvent.Id);

                if (existingEvent is null) {
                    throw new StaleObjectStateException("", "");
                }

                this.Events.Remove(existingEvent);
                this.Events.Add(updatedEvent);
            });
            this.Session.When(x => x.Delete(Arg.Any<Event>())).Do(arg => this.Events.Remove(arg.ArgAt<Event>(0)));
            this.Cache = Substitute.For<IDistributedCache>();
            this.Controller = new EventsController(this.Session, this.Cache);
        }
    }
}
