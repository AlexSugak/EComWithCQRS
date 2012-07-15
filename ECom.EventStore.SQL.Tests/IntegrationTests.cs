using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Messages;
using ECom.Bus;

namespace ECom.EventStore.SQL.Tests
{
    //[TestClass]
    //public class IntegrationTests
    //{
    //    private Guid _aggregateId = Guid.NewGuid();
    //    private EventStore _eventStore;

    //    private string _connectionString;

    //    [TestInitialize]
    //    public void Setup()
    //    {
    //        _connectionString = @"Data Source=.\;Initial Catalog=ECom.EventStore;Persist Security Info=True;Integrated Security=SSPI";
    //        _eventStore = new EventStore(_connectionString, new FakePublisher());
    //    }

    //    [TestMethod, TestCategory("Integration"), Ignore]
    //    public void TestEvent_Save()
    //    {
    //        var testEvent = new TestEvent(_aggregateId, 0, "test data");
    //        var testEvent2 = new TestEvent(_aggregateId, 1, "test data 2");
    //        _eventStore.SaveAggregateEvents(testEvent.AggregateId, "test aggregate", new[] { testEvent, testEvent2 }, 0);

    //        var events = _eventStore.GetEventsForAggregate(testEvent.AggregateId).ToArray();
    //        Assert.AreEqual(2, events.Count());

    //        Assert.AreEqual(0, events[0].Version);
    //        Assert.AreEqual("test data", ((TestEvent)events[0]).TestData);

    //        Assert.AreEqual(1, events[1].Version);
    //        Assert.AreEqual("test data 2", ((TestEvent)events[1]).TestData);
    //    }
    //}

    //[Serializable]
    //sealed class TestEvent : Event
    //{
    //    public readonly string TestData;
    //    private FakeIdentity _id;

    //    private TestEvent()
    //    {
    //    }

    //    public TestEvent(Guid aggregateId, int version, string testData)
    //    {
    //        _id = new FakeIdentity(aggregateId);
    //        Version = version;
    //        TestData = testData;
    //    }

    //    public override IIdentity AggregateId
    //    {
    //        get { return _id; }
    //    }
    //}

    //class FakeIdentity : GuidIdentity
    //{
    //    public FakeIdentity(Guid id)
    //        : base(id)
    //    {
    //    }

    //    public override string GetTag()
    //    {
    //        return "fakeId";
    //    }
    //}

    //class FakePublisher : IEventPublisher
    //{
    //    public void Publish<T>(T @event) where T : Event
    //    {
    //        //nothing to do
    //    }
    //}
}
