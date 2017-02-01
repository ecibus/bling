using System;
using System.Collections.Generic;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Rhino.Mocks;
using Bling.Domain;
using Bling.Repository;
using Bling.Presenter;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Tests
{

    [TestFixture]
    public class GEMUserTests
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
        public void Should_be_able_to_create_an_object()
        {            
            Assert.That(new GEMUser(), Is.Not.Null);
        }
                
    }
}
