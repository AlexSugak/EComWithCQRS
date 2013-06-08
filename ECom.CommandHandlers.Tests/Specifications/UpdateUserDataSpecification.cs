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
    public class UpdateUserDataSpecification : CommandSpecificationTest<UpdateUserData>
    {
        [TestInitialize]
        public override void SetUp()
        {
            StaticsInitializer.Dummy();
            base.SetUp();
        }

        [TestMethod]
        public void update_user_name()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";
            var newUserName = "New name";

            Assert(new CommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new UpdateUserData(userId, newUserName, photoUrl, 2),
                ExpectEvents = 
                { 
                    new UserDataUpdated(TimeProvider.Now, 2, userId, newUserName, photoUrl)
                }
            });
        }

        [TestMethod]
        public void update_user_photo()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";
            var newPhotoUrl = "New photo";

            Assert(new CommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new UpdateUserData(userId, userName, newPhotoUrl, 2),
                ExpectEvents = 
                { 
                    new UserDataUpdated(TimeProvider.Now, 2, userId, userName, newPhotoUrl)
                }
            });
        }

        [TestMethod]
        public void update_user_name_and_photo()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";
            var newUserName = "new name";
            var newPhotoUrl = "New photo";

            Assert(new CommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new UpdateUserData(userId, newUserName, newPhotoUrl, 2),
                ExpectEvents = 
                { 
                    new UserDataUpdated(TimeProvider.Now, 2, userId, newUserName, newPhotoUrl)
                }
            });
        }

        [TestMethod]
        public void update_user_data_no_changes()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new CommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new UpdateUserData(userId, userName, photoUrl, 2),
                ExpectEvents = {}
            });
        }

        [TestMethod]
        public void update_user_data_null_userId()
        {
            var userId = new UserId("123");
            var userName = "name";
            var email = new EmailAddress("mail@test.com");
            var photoUrl = "kjh";

            Assert(new FailingCommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, userId, userName, email, photoUrl)
                },
                When = new UpdateUserData(null, userName, photoUrl, 2),
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

            Assert(new FailingCommandSpecification<UpdateUserData>()
            {
                Given = 
                {
                    new UserCreated(TimeProvider.Now, 1, new UserId("another@test.com"), userName, email, photoUrl)
                },
                When = new UpdateUserData(userId, userName, photoUrl, 2),
                ExpectException = new AggregateRootNotFoundException(typeof(UserAggregate), userId)
            });
        }
    }
}
