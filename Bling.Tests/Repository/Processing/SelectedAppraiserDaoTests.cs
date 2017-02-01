using System;
using Bling.Presenter;
using Bling.Repository.Processing;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;


namespace Bling.Tests.Repository.Processing
{
    [TestFixture, Category("Database")]
    public class SelectedAppraiserDaoTests
    {
        [Test, Ignore]
        public void GetLastAppraiserForBranch_371_Returns09O()
        {
            //Arrange
            SelectedAppraiserDao dao = new SelectedAppraiserDao(StaticSessionManager.OpenSessionForDMDData());

            //Act
            string appraiserId = dao.GetLastAppraiserForBranch("371");

            //Assert
            Assert.That(appraiserId, Is.EqualTo("09O"));
        }
    }
}
