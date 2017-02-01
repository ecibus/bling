using System;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.Processing;
using NHibernate;

namespace Bling.Repository.Processing
{
    public interface IOrderAppraisalDao
    {
        void SaveInDT(OrderAppraisal orderAppraisal);
        string GetFolderPathInPoint(string loanNumber);
    }

    public class OrderAppraisalDao : AbstractDao<OrderAppraisal, string>, IOrderAppraisalDao
    {
        public OrderAppraisalDao(ISession session)
            : base(session)
        {            
        }

        public void SaveInDT(OrderAppraisal orderAppraisal)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateOrderAppraisal";
                    cmd.Parameters.AddWithValue("@LoanNumber", orderAppraisal.LoanNumber);  
                    cmd.Parameters.AddWithValue("@Appraiser", orderAppraisal.Appraiser);
                    cmd.Parameters.AddWithValue("@TicketNo", orderAppraisal.TicketNo);
                    cmd.Parameters.AddWithValue("@OrderedBy", orderAppraisal.OrderedBy);
                    cmd.Parameters.AddWithValue("@OrderedDate", orderAppraisal.OrderedDate);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GetFolderPathInPoint(string loanNumber)
        {
            string folderpath = "";

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetFolderPathInPoint";
                    cmd.Parameters.AddWithValue("@LoanNumber", loanNumber);

                    folderpath = (string) cmd.ExecuteScalar();
                }
            }

            return folderpath;
        }

    }
}
