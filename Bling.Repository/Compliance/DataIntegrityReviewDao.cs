using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;

namespace Bling.Repository.Compliance
{
    public interface IDataIntegrityReviewDao : IDao<DataIntegrityReview, int>
    {
        DataIntegrityReview GetByFileId(string fileId);
    }
    
    public class DataIntegrityReviewDao : AbstractDao<DataIntegrityReview, int>, IDataIntegrityReviewDao
    {
        public DataIntegrityReviewDao(ISession session) :
            base(session)
        {
        }
        public DataIntegrityReview GetByFileId(string fileId)
        {
            return m_session
                .CreateQuery("from DataIntegrityReview where FileId = :fileId")
                .SetString("fileId", fileId)
                .UniqueResult<DataIntegrityReview>();
        }
    }
}
