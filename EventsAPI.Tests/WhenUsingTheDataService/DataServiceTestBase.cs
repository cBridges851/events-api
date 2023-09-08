using EventsAPI.Models;
using EventsAPI.Services;
using Microsoft.Extensions.Caching.Distributed;
using NHibernate;
using NHibernate.Cache;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Tests.WhenUsingTheDataService {
    public class DataServiceTestBase {
        protected List<Event> Values = new List<Event>();
        protected ISession Session;
        protected IDistributedCache Cache;
        protected IDataService<Event> DataService;

        [SetUp]
        public void BaseSetUp() {
            this.Values = new List<Event> {
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
            this.Session.CreateCriteria<Event>().List<Event>().Returns(this.Values);
            this.Cache = Substitute.For<IDistributedCache>();
            this.DataService = new DataService<Event>(this.Session, this.Cache);
        }
    }
}
