using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECom.Utility.Tests
{
    [TestClass]
    public class ArgumentTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedNull_ShouldThrowException()
        {
            Argument.ExpectNotNull(null, "someParam");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedEmptyString_ShouldThrowException()
        {
			Argument.ExpectNotNullOrWhiteSpace(string.Empty, "someParam");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedNullString_ShouldThrowException()
        {
			Argument.ExpectNotNullOrWhiteSpace(null, "someParam");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedWhiteSpaceString_ShouldThrowException()
        {
			Argument.ExpectNotNullOrWhiteSpace("  ", "someParam");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedNullExpression_ShouldThrowException()
        {
            string someParam = null;
			Argument.ExpectNotNull(() => someParam);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedEmptyStringExpression_ShouldThrowException()
        {
            var someParam = string.Empty;
			Argument.ExpectNotNullOrWhiteSpace(() => someParam);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedNullStringExpression_ShouldThrowException()
        {
            string someParam = null;
			Argument.ExpectNotNullOrWhiteSpace(() => someParam);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Argument_PassedWhiteSpaceStringExpression_ShouldThrowException()
        {
            var someParam = "   ";
			Argument.ExpectNotNullOrWhiteSpace(() => someParam);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Argument_PassedEmptyGuidStringExpression_ShouldThrowException()
        {
            var someParam = Guid.Empty;
			Argument.ExpectNotEmptyGuid(() => someParam);
        }

        [TestMethod]
        public void Argument_PassedNotEmptyStringExpression_ShouldNotThrowException()
        {
            var notEmptyString = "aa";
			Argument.ExpectNotNullOrWhiteSpace(() => notEmptyString);
        }

        [TestMethod]
        public void Argument_PassedNotNullObjectExpression_ShouldNotThrowException()
        {
            var notNullObject = new Object();
			Argument.ExpectNotNull(() => notNullObject);
        }

        [TestMethod]
        public void Argument_PassedNotEmptyGuidExpression_ShouldNotThrowException()
        {
            var notEmptyGuid = Guid.NewGuid();
			Argument.ExpectNotEmptyGuid(() => notEmptyGuid);
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Argument_PassedFalseCondition_ShouldThrowException()
		{
			Argument.Expect(() => 1 != 1, "someParam", "test message");
		}

		[TestMethod]
		public void Argument_PassedTrueCondition_ShouldNotThrowException()
		{
			Argument.Expect(() => 1 != 2, "someParam", "test message");
		}
    }
}
