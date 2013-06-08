using ECom.Domain;
using ECom.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.CommandHandlers.Tests.Specifications
{
    [TestClass]
    public class CreateUserSpecification : CommandSpecificationTest<CreateUser>
    {
        [TestInitialize]
        public override void SetUp()
        {
            StaticsInitializer.Dummy();
            base.SetUp();
        }

        [TestMethod]
        public void create_user()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new CommandSpecification<CreateUser>()
            {
                When = new CreateUser(userId, userName, email, photoUrl),
                ExpectEvents = 
                { 
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                }
            });
        }

        [TestMethod]
        public void create_user_duplicate_id()
        {
            var userId = new UserId("123");

            Assert(new FailingCommandSpecification<CreateUser>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, "name1", new EmailAddress("mail1@test.com"), null) 
                },
                When = new CreateUser(userId, "name2", new EmailAddress("mail2@test.com"), null),
                ExpectException = new DuplicateEntityException()
            });
        }

        [TestMethod]
        public void create_user_null_id()
        {
            Assert(new FailingCommandSpecification<CreateUser>()
            {
                When = new CreateUser(null, "name2", new EmailAddress("mail2@test.com"), null),
                ExpectException = new ArgumentNullException("userId")
            });
        }

        [TestMethod]
        public void create_user_null_email()
        {
            Assert(new FailingCommandSpecification<CreateUser>()
            {
                When = new CreateUser(new UserId("mail@test.com"), "", null, null),
                ExpectException = new ArgumentNullException("email")
            });
        }

    }
}
