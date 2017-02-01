using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.LOS;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.LOS;

namespace Bling.Tests.Repository.LOS
{
    [TestFixture, Category("Database")]
    public class HMDAChangesDaoTests
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
        public void Should_be_able_to_get_hmda_changes_by_loan_number()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            List<HMDAChanges> datas = new HMDAChangesDao(session).FindByLoanNumber("Test");

            Console.WriteLine(datas.Count);
        }
    }
}
