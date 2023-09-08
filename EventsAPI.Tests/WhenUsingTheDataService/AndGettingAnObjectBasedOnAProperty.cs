using EventsAPI.Models;
using NSubstitute;
using NUnit.Framework;

namespace EventsAPI.Tests.WhenUsingTheDataService {
    public class AndGettingAnObjectBasedOnAProperty : DataServiceTestBase {
        [Ignore("Return to this, struggling with mocking the GetRecordAsync extension method")]
        [Test]
        public void ThenItShouldReturnAnObjectThatMatchesTheProperty() {
            var returnedEvent = this.DataService.Get<Event>(x => x.Id, this.Values.First().Id);
            Assert.AreEqual(this.Values.First(), returnedEvent);
        }

        [Ignore("Return to this, struggling with mocking the GetRecordAsync extension method")]
        [Test]
        public void ThenItShouldReturnNullIfNoObjectMatches() {
        }
    }
}
