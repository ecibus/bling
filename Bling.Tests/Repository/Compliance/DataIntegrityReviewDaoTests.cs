using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Domain.Compliance;
using Bling.Repository.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class DataIntegrityReviewDaoTests
    {
        [Test]
        public void Save_ShouldSaveTheObject()
        {
            ////Arrange
            //ISession session = StaticSessionManager.OpenSessionForDMDData();
            //DataIntegrityReview review = new DataIntegrityReview { ActorId = "ActorId", CreatedOn = DateTime.Now, FileId = "FileId", Notes = "Notes" };
            //IDataIntegrityReviewDao dao = new DataIntegrityReviewDao(session);

            ////Act
            //ITransaction t = session.BeginTransaction();
            //var o = dao.Save(review);
            //t.Commit();

            ////Assert
            //Assert.That(o.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public void GetByFileId_ShouldReturnAnObject()
        {
            ////Arrange
            //ISession session = StaticSessionManager.OpenSessionForDMDData();
            //IDataIntegrityReviewDao dao = new DataIntegrityReviewDao(session);

            ////Act
            //var review = dao.GetByFileId("fileid");

            ////Assert
            //Assert.That(review.Id, Is.Not.EqualTo(0));
                
        }
    }
}
