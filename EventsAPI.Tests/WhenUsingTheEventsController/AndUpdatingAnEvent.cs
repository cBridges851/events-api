using EventsAPI.Models;
using NUnit.Framework;
using System.Net;

namespace EventsAPI.Tests.WhenUsingTheEventsController {
    public class AndUpdatingAnEvent : EventsControllerTestBase {
        private Event eventToUpdate = new Event();

        [SetUp]
        public void SetUp() {
            this.eventToUpdate = new Event {
                Id = this.Events.First().Id
            };
        }

        [Test]
        public void WhenTheEventExistsThenItShouldReturnThatTheEventHasBeenSuccessfullyUpdated() {
            var result = this.Controller?.UpdateEvent(this.eventToUpdate);
            Assert.AreEqual(result?.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void WhenTheEventDoesNotExistThenItShouldReturnThatItWasNotFound() {
            var nonExistentEvent = new Event {
               Id = Guid.NewGuid()
            };

            var result = this.Controller?.UpdateEvent(nonExistentEvent);
            Assert.AreEqual(result?.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public void WhenTheNameHasBeenUpdatedThenItShouldSaveThisChange() {
            var newName = "This Event Has Been Renamed!";           
            
            this.eventToUpdate.Name = newName;
            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(newName, updatedEvent?.Name);
            Assert.AreEqual(this.eventToUpdate.Description, updatedEvent?.Description);
            Assert.AreEqual(this.eventToUpdate.Date, updatedEvent?.Date);
            Assert.AreEqual(this.eventToUpdate.EventType, updatedEvent?.EventType);
        }

        [Test]
        public void WhenTheDescriptionHasBeenUpdatedThenItShouldSaveThisChange() {
            var newDescription = "This Event's Description Has Been Updated!";

            this.eventToUpdate.Description = newDescription;
            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(this.eventToUpdate.Name, updatedEvent?.Name);
            Assert.AreEqual(newDescription, updatedEvent?.Description);
            Assert.AreEqual(this.eventToUpdate.Date, updatedEvent?.Date);
            Assert.AreEqual(this.eventToUpdate.EventType, updatedEvent?.EventType);
        }

        [Test]
        public void WhenTheDateHasBeenUpdatedThenItShouldSaveThisChange() {
            var newDate = new DateTime(2066, 6, 6);

            this.eventToUpdate.Date = newDate;
            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(this.eventToUpdate.Name, updatedEvent?.Name);
            Assert.AreEqual(this.eventToUpdate.Description, updatedEvent?.Description);
            Assert.AreEqual(newDate, updatedEvent?.Date);
            Assert.AreEqual(this.eventToUpdate.EventType, updatedEvent?.EventType);
        }

        [Test]
        public void WhenTheTypeHasBeenUpdatedThenItShouldSaveThisChange() {
            var newType = EventType.Hybrid;

            this.eventToUpdate.EventType = newType;
            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(this.eventToUpdate.Name, updatedEvent?.Name);
            Assert.AreEqual(this.eventToUpdate.Description, updatedEvent?.Description);
            Assert.AreEqual(this.eventToUpdate.Date, updatedEvent?.Date);
            Assert.AreEqual(newType, updatedEvent?.EventType);
        }

        [Test]
        public void WhenMultipleFieldsHaveBeenUpdatedThenItShouldSaveAllOfThem() {
            var newName = "THIS IS A BRAND NEW NAME!";
            var newDate = new DateTime(2024, 9, 30);
            this.eventToUpdate.Name = newName;
            this.eventToUpdate.Date = newDate;
            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(newName, updatedEvent?.Name);
            Assert.AreEqual(this.eventToUpdate.Description, updatedEvent?.Description);
            Assert.AreEqual(newDate, updatedEvent?.Date);
            Assert.AreEqual(this.eventToUpdate.EventType, updatedEvent?.EventType);
        }

        [Test]
        public void WhenAllFieldsHaveBeenUpdatedThenItShouldSaveAllOfThem() {
            var newName = "THIS IS A BRAND NEW NAME!";
            var newDescription = "THIS IS A BRAND NEW DESCRIPTION!";
            var newDate = new DateTime(2025, 10, 31);
            var newEventType = EventType.InPerson;

            this.eventToUpdate.Name = newName;
            this.eventToUpdate.Description = newDescription;
            this.eventToUpdate.Date = newDate;
            this.eventToUpdate.EventType = newEventType;

            this.Controller?.UpdateEvent(this.eventToUpdate);
            var updatedEvent = this.Controller?.GetEvent(this.eventToUpdate.Id);
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual(newName, updatedEvent?.Name);
            Assert.AreEqual(newDescription, updatedEvent?.Description);
            Assert.AreEqual(newDate, updatedEvent?.Date);
            Assert.AreEqual(newEventType, updatedEvent?.EventType);
        }

    }
}
