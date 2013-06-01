using ECom.Messages;
using ECom.Site.Areas.Admin.Controllers;
using ECom.Site.Areas.Admin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ECom.Site.Tests
{
    [TestClass]
    public class EventViewerControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            // create the mock repository
            Mock<IEventStore> mock = new Mock<IEventStore>();

            var allEvents = new IEvent[]
                {
                    new UserCreated(new UserId("test1@eee.com")) {Version = 1, Date = DateTime.Now},
                    new UserCreated(new UserId("test2@eee.com")) {Version = 2, Date = DateTime.Now},
                    new UserCreated(new UserId("test3@eee.com")) {Version = 3, Date = DateTime.Now},
                    new UserCreated(new UserId("test4@eee.com")) {Version = 4, Date = DateTime.Now},
                    new UserCreated(new UserId("test4@eee.com")) {Version = 5, Date = DateTime.Now}
                };
            
            mock.Setup(m => m.GetEventsForAggregate(It.IsAny<string>())).Returns(allEvents.AsQueryable());

            // create  instance of a controller; set the page size
            EventViewerController controller = new EventViewerController(mock.Object);
            controller.PageSize = 3;

            // Action
            EventViewerViewModel result = (EventViewerViewModel)((ViewResultBase)controller.Index("1", null, 2)).Model;

            // Assert
            EventViewModel[] eventArray = result.Events.ToArray();
            Assert.IsTrue(eventArray.Length == 2);
            Assert.AreEqual(eventArray[0].EventVersion, 4);
            Assert.AreEqual(eventArray[1].EventVersion, 5);
        }

        [TestMethod]
        public void Can_Get_Details()
        {
            // Arrange
            // - create the mock repository
            Mock<IEventStore> mock = new Mock<IEventStore>();

            var allEvents = new IEvent[]
                {
                    new ProductAddedToOrder() {Id = new OrderId(777), Date = DateTime.Now, Version = 1},
                    new ProductAddedToOrder() {Id = new OrderId(777), Date = DateTime.Now, Version = 3},
                    new ProductAddedToOrder() {Id = new OrderId(777), Date = DateTime.Now, Version = 5}
                };

            mock.Setup(m => m.GetEventsForAggregate(It.IsAny<string>())).Returns(allEvents.AsQueryable());

            // create  instance of a controller
            EventViewerController controller = new EventViewerController(mock.Object);

            // Action: try to find event with Version = 3
            var result = ((ViewResultBase)controller.Details("777", 3)).Model as EventDetailsViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EventDetails);
        }
    }
}