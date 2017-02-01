using System;
using Bling.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class UserInfoTests
    {
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_create_fullname()
        {
            UserInfo user = new UserInfo { FirstName = "Firstname", LastName = "Lastname" };
            Assert.That(user.FullName, Is.EqualTo("Firstname Lastname"));
        }
    }
}
