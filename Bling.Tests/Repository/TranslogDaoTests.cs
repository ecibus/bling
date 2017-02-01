using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Repository;
using NUnit.Framework.SyntaxHelpers;
using Bling.Domain;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class TranslogDaoTests
    {
        [Test]
        public void GetById_ShouldWork()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ITranslogDao dao = new TranslogDao(session);

            //Act
            var log = dao.GetById(172);

            //Assert
            Assert.That(log.NewValue, Is.EqualTo("360"));
        }

        [Test]
        public void Save_ShouldWork()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ITranslogDao dao = new TranslogDao(session);
            Translog translog = new Translog { FileId = "AAD6O", ActorId = "01BR", Field = "Branch Type", OldValue = "BRANCH", NewValue = "BROKER", ChangeDate = DateTime.Now};
            
            //Act
            var log = dao.Save(translog);

        }
    }
}
