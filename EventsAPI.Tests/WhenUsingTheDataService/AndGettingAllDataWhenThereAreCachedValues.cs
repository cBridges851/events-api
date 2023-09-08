using EventsAPI.Extensions;
using EventsAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System.Text;

namespace EventsAPI.Tests.WhenUsingTheDataService {
    public class AndGettingAllDataWhenThereAreCachedValues : DataServiceTestBase {
        private List<Event> CachedValues { get; set; }

        [SetUp]
        public void Setup() {
            this.CachedValues = new List<Event> {
                new Event { 
                    Id = Guid.NewGuid(),
                    Name = "HI I'M A CACHED EVENT!",
                    Description = "This event is obviously cached, idiot",
                    Date = DateTime.UtcNow,
                    EventType = EventType.Online
                }
            };
            this.Cache = Substitute.For<IDistributedCache>();
            this.Cache.GetRecordAsync<List<Event>>(Arg.Any<string>()).Returns(this.CachedValues);
        }
        [Ignore("Return to this, struggling with mocking the GetRecordAsync extension method")]
        [Test]
        public async Task ThenItShouldReturnAllTheValuesFromTheCache() {
            var recordKey = $"Events_{DateTime.UtcNow.ToString("yyyyMMdd_hhmm")}";
            var result = await this.DataService.GetAll();
            Assert.AreEqual(this.CachedValues, result);
            Assert.AreNotEqual(this.Values, result);
        }
    }
}
