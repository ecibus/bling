using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class ReportUserDaoTests
    {
        private string m_EmployId;
        private ISession m_Session;
        private IReportUserDao m_Dao;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new ReportUserDao(m_Session);
            m_EmployId = "b<4";
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_report_user_by_id()
        {            
            Assert.That(m_Dao.GetById("aar").FirstName.ToLower(), Is.EqualTo("nancy"));
        }

        [Test]
        public void Should_be_able_to_get_all_funder()
        {
            Assert.That(m_Dao.GetAllFunder().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_add_funder_in_report()
        {            
            m_Dao.AddFunder(m_EmployId);

            Assert.That(m_Dao.GetById(m_EmployId), Is.Not.Null);
        }

        [Test]
        public void Should_be_able_to_remove_funder_in_report()
        {
            m_Dao.AddFunder(m_EmployId);
            m_Dao.RemoveUserAsFunder(m_EmployId);
            Assert.That(m_Dao.GetById(m_EmployId), Is.Null);
        }

        [Test]
        public void Should_be_able_to_get_all_underwriter()
        {
            Assert.That(m_Dao.GetAllUnderwriter().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_add_underwriter_in_report()
        {
            m_Dao.AddUnderwriter(m_EmployId);

            Assert.That(m_Dao.GetById(m_EmployId), Is.Not.Null);
        }

        [Test]
        public void Should_be_able_to_remove_underwriter_in_report()
        {
            m_Dao.AddUnderwriter(m_EmployId);
            m_Dao.RemoveUserAsUnderwriter(m_EmployId);
            Assert.That(m_Dao.GetById(m_EmployId), Is.Null);
        }

        [Test]
        public void Should_be_able_to_make_user_both_an_underwriter_and_funder()
        {
            m_Dao.AddFunder(m_EmployId);
            m_Dao.AddUnderwriter(m_EmployId);

            ReportUser ru = m_Dao.GetById(m_EmployId);
            Assert.That(ru.IsFunder, Is.True);
            Assert.That(ru.IsUnderwriter, Is.True);

            m_Dao.RemoveUserAsUnderwriter(m_EmployId);
            m_Dao.RemoveUserAsFunder(m_EmployId);
            Assert.That(m_Dao.GetById(m_EmployId), Is.Null);
        }

        [Test]
        public void Should_not_be_able_to_delete_funder_when_user_is_underwriter()
        {
            m_Dao.AddFunder(m_EmployId);
            m_Dao.AddUnderwriter(m_EmployId);
                        
            m_Dao.RemoveUserAsUnderwriter(m_EmployId);            
            Assert.That(m_Dao.GetById(m_EmployId), Is.Not.Null);
                        
            m_Dao.RemoveUserAsFunder(m_EmployId);
            Assert.That(m_Dao.GetById(m_EmployId), Is.Null);
        }

        [Test]
        public void Should_not_be_able_to_delete_underwriter_when_user_is_funder()
        {
            m_Dao.AddFunder(m_EmployId);
            m_Dao.AddUnderwriter(m_EmployId);

            m_Dao.RemoveUserAsFunder(m_EmployId);
            Assert.That(m_Dao.GetById(m_EmployId), Is.Not.Null);
            
            m_Dao.RemoveUserAsUnderwriter(m_EmployId);            
            Assert.That(m_Dao.GetById(m_EmployId), Is.Null);
        }
    }
}
