using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Site;
using System.Collections.Generic;
using ECom.Messages;
using ECom.Site.Core;
using ECom.Site.Areas.Admin.Models;

namespace ECom.Site.Tests
{
    [TestClass]
    public class EventComparerClass
    {
        private readonly static List<EventViewModel> testList;

        static EventComparerClass()
        {
            testList = new List<EventViewModel>() 
                {
                    new EventViewModel() {EventName = "ccc", EventDate = new DateTime(2013, 1, 23)},
                    new EventViewModel() {EventName = "aaa", EventDate = new DateTime(2013, 1, 21)},
                    new EventViewModel() {EventName = "bbb", EventDate = new DateTime(2013, 1, 22)}
                };
        }
        
        [TestMethod]
        public void sort_using_default_settings()
        {
            var comparer = new EventComparer();
            DateTime date1 = new DateTime(2013, 1, 21);
            DateTime date2 = new DateTime(2013, 1, 22);
            DateTime date3 = new DateTime(2013, 1, 23);

            testList.Sort(comparer);

            Assert.AreEqual(date1.ToShortDateString(), testList[0].EventDate.ToShortDateString());
            Assert.AreEqual(date2.ToShortDateString(), testList[1].EventDate.ToShortDateString());
            Assert.AreEqual(date3.ToShortDateString(), testList[2].EventDate.ToShortDateString());
        }

        [TestMethod]
        public void sort_by_date_asc()
        {
            var comparer = new EventComparer(SortFieldEnum.DATE, SortOrderEnum.ASC);
            DateTime date1 = new DateTime(2013, 1, 21);
            DateTime date2 = new DateTime(2013, 1, 22);
            DateTime date3 = new DateTime(2013, 1, 23);

            testList.Sort(comparer);

            Assert.AreEqual(date1.ToShortDateString(), testList[0].EventDate.ToShortDateString());
            Assert.AreEqual(date2.ToShortDateString(), testList[1].EventDate.ToShortDateString());
            Assert.AreEqual(date3.ToShortDateString(), testList[2].EventDate.ToShortDateString());
        }

        [TestMethod]
        public void sort_by_date_desc()
        {
            var comparer = new EventComparer(SortFieldEnum.DATE, SortOrderEnum.DESC);
            DateTime date1 = new DateTime(2013, 1, 21);
            DateTime date2 = new DateTime(2013, 1, 22);
            DateTime date3 = new DateTime(2013, 1, 23);

            testList.Sort(comparer);

            Assert.AreEqual(date3.ToShortDateString(), testList[0].EventDate.ToShortDateString());
            Assert.AreEqual(date2.ToShortDateString(), testList[1].EventDate.ToShortDateString());
            Assert.AreEqual(date1.ToShortDateString(), testList[2].EventDate.ToShortDateString());
        }

        [TestMethod]
        public void sort_by_name_asc()
        {
            var comparer = new EventComparer(SortFieldEnum.NAME, SortOrderEnum.ASC);

            testList.Sort(comparer);

            Assert.AreEqual("aaa", testList[0].EventName);
            Assert.AreEqual("bbb", testList[1].EventName);
            Assert.AreEqual("ccc", testList[2].EventName);
        }

        [TestMethod]
        public void sort_by_name_desc()
        {
            var comparer = new EventComparer(SortFieldEnum.NAME, SortOrderEnum.DESC);

            testList.Sort(comparer);

            Assert.AreEqual("ccc", testList[0].EventName);
            Assert.AreEqual("bbb", testList[1].EventName);
            Assert.AreEqual("aaa", testList[2].EventName);
        }
    }
}
