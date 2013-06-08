using ECom.Domain;
using ECom.Domain.Aggregates.User;
using ECom.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.CommandHandlers.Tests.Specifications
{
    [TestClass]
    public class ChangeUserEmailSpecification : CommandSpecificationTest<ChangeUserEmail>
    {
        [TestInitialize]
        public override void SetUp()
        {
            StaticsInitializer.Dummy();
            base.SetUp();
        }

        [TestMethod]
        public void change_user_email()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";
            var newEmail = new EmailAddress("new@text.com");

            Assert(new CommandSpecification<ChangeUserEmail>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new ChangeUserEmail(userId, newEmail, 2),
                ExpectEvents = 
                { 
                    new UserEmailChanged(TimeProvider.Now, 2, userId, newEmail)
                }
            });
        }

        [TestMethod]
        public void change_user_email_no_changes()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new CommandSpecification<ChangeUserEmail>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new ChangeUserEmail(userId, email, 2),
                ExpectEvents = {}
            });
        }

        [TestMethod]
        public void change_user_email_null_email()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new FailingCommandSpecification<ChangeUserEmail>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new ChangeUserEmail(userId, null, 2),
                ExpectException = new ArgumentNullException("email")
            });
        }

        [TestMethod]
        public void change_user_email_null_userId()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new FailingCommandSpecification<ChangeUserEmail>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new ChangeUserEmail(null, email, 2),
                ExpectException = new ArgumentNullException("userId")
            });
        }

        [TestMethod]
        public void update_user_data_non_existing_user()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new FailingCommandSpecification<ChangeUserEmail>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, new UserId("another@test.com"), userName, email, photoUrl)
                },
                When = new ChangeUserEmail(userId, email, 2),
                ExpectException = new AggregateRootNotFoundException(typeof(UserAggregate), userId)
            });
        }
    }
}
