using EventsAPI.Controllers;
using EventsAPI.Models;
using EventsAPI.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq.Expressions;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class EventsControllerTestBase {
        protected List<Event> Events = new List<Event>();
        protected IDataService<Event> DataService { get; private set; }
        protected EventsController Controller { get; set; }

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

            this.DataService = Substitute.For<IDataService<Event>>();
            this.DataService.GetAll().Returns(this.Events);
            this.DataService.Get<Event>(Arg.Any<Expression<Func<Event, object>>>(), Arg.Any<object>()).Returns((arg) => this.Events.FirstOrDefault(
                x => x.Id == arg.ArgAt<Guid>(1))
            );
            this.DataService.Delete(Arg.Any<Guid>()).Returns(arg => {
                var eventToDelete = this.Events.FirstOrDefault(x => x.Id == arg.ArgAt<Guid>(0));
                if (eventToDelete is null) {
                    return false;
                }

                this.Events.Remove(eventToDelete);
                return true;
            });

            this.DataService.When(x => x.Create(Arg.Any<Event>())).Do(arg => this.Events.Add(arg.ArgAt<Event>(0)));
            this.DataService.Update(Arg.Any<Event>()).Returns(arg => { 
                var eventToUpdate = this.Events.FirstOrDefault(x => x.Id == arg.ArgAt<Event>(0).Id);

                if (eventToUpdate is null) { return false; }

                this.Events.Remove(eventToUpdate);
                this.Events.Add(arg.ArgAt<Event>(0));
                return true;
            });
            this.Controller = new EventsController(this.DataService);
        }
    }
}
