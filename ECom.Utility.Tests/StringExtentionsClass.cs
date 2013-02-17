using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECom.Utility;

namespace ECom.Utility.Tests
{
	[TestClass]
	public class StringExtentionsClass
	{
		[TestClass]
		public class WordifyMethod
		{
			[TestMethod]
			public void must_split_camel_case()
			{
				var testString = "SomeTestCamelCaseString";

				Assert.AreEqual("Some Test Camel Case String", testString.Wordify());
			}

			[TestMethod]
			public void must_not_split_acronyms()
			{
				var testString = "WTF";

				Assert.AreEqual("WTF", testString.Wordify());
			}
		}

        [TestClass]
        public class ReverseMethod
        {
            [TestMethod]
            public void reverse_not_empty_string()
            {
                var testString = "test";

                Assert.AreEqual("tset", testString.Reverse());
            }

            [TestMethod]
            public void reverse_empty_string()
            {
                var testString = string.Empty;

                Assert.AreEqual(string.Empty, testString.Reverse());
            }
        }
	}
}
