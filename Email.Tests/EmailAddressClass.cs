using System;
using ECom.Messages;
using Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECom.Utility.Tests
{
	[TestClass]
	public class EmailAddressClass
	{
		[TestClass]
		public class Ctor
		{
			[TestMethod]
			public void must_accept_valid_email_address()
			{
				var address = new EmailAddress("valid@email.com");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentException))]
			public void must_not_accept_address_without_sobaka()
			{
				var address = new EmailAddress("valid.email.com");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentException))]
			public void must_not_accept_address_without_domain()
			{
				var address = new EmailAddress("valid@email");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentException))]
			public void must_not_accept_address_without_name()
			{
				var address = new EmailAddress("@email.com");
			}
		}
	}
}
