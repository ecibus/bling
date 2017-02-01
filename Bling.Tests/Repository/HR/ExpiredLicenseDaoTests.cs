using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using Bling.Domain.HR;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public sealed class ExpiredLicenseDaoTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_all_expired_license()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IExpiredLicenseDao dao = new ExpiredLicenseDao(session);
            	
            //Act
            List<ExpiredLicense> list = dao.GetAllEmployee().ToList();

            //Assert
            //list.ForEach(x => Console.WriteLine("{0} {1} {2} {3} {4}", 
            //                      x.EmployeeName, x.EMail, x.Branch,
            //                      x.ExpirationDate.ToString("MM/dd/yyyy"),
            //                      x.HireDate.ToString("MM/dd/yyyy")));
                    
            var branches = from b in list
                         select b.Branch;
            
            foreach (var branch in branches.Distinct()) 
            {
                Console.WriteLine(branch);
                var br = from b in list 
                         where b.Branch == branch
                         select b;

                br.ToList().ForEach(x => Console.WriteLine(" - {0}", x.EmployeeId));
                
            }
                         
            
            Assert.That(list.Count, Is.GreaterThan(0));
        }
    }
}
