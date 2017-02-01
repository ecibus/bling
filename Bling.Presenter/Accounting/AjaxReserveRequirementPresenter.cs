using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;
using Bling.Domain.Extension;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using Oracle;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using System.Net;

namespace Bling.Presenter.Accounting
{
    public class AjaxReserveRequirementPresenter : Presenter
    {
        private IAjaxView m_View;
        private IReserveRequirementDao m_Dao;

        public AjaxReserveRequirementPresenter(IAjaxView view)
            : this(view, new ReserveRequirementDao(DMDDataSession()))
        {
        }

        public AjaxReserveRequirementPresenter(IAjaxView view, IReserveRequirementDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Save(ReserveRequirement rr)
        {
            m_Dao.Save(rr);
            m_View.ResponseText = String.Format("{{ Id : '{0}' }}", rr.Id);
        }

        public void Update(string id, string costCenter, string reserveMinimum, string fixedReserve, string recipient)
        {
            //var rr = m_Dao.GetById(id.ToInteger());

            //rr.CostCenter = costCenter;
            //rr.ReserveMinimum = reserveMinimum.ToDecimal();
            //rr.FixedReserve = fixedReserve.ToDecimal();
            //m_Dao.Save(rr);
            try
            {
                m_Dao.Update(id, costCenter, reserveMinimum == string.Empty ? "null" : reserveMinimum, 
                    fixedReserve == string.Empty ? "null" : fixedReserve, recipient);
                m_View.ResponseText = String.Format("{{ Id : '{0}' }}", id);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("duplicate"))
                {
                    throw new ApplicationException(String.Format("Cost Center {0} already exist in Reserve Requirement table.  Please try again.", costCenter));
                }
                else
                {
                    throw ex;
                }
            }
        }

        public void Load()
        {
            IList<ReserveRequirement> list = m_Dao.GetAll().OrderBy(x => x.CostCenter).ToList();

            StringBuilder tr = new StringBuilder();
            int counter = 1;

            foreach (var l in list)
            {
                tr.AppendFormat("<tr><td>{7}.</td><td><input id='chkSend_{5}' class='sendEmail' type='checkbox' /></td><td id='cc_{5}'>{0}</td><td class='number dollarAmount' id='rm_{5}'>{1}</td><td class='number dollarAmount' id='fr_{5}'>{2}</td><td id='r_{5}'>{6}</td><td>{3}</td><td id='upd_{5}'>{4}</td></tr>",
                    l.CostCenter, 
                    l.ReserveMinimum.HasValue ? l.ReserveMinimum.Value.ToString("#,##0") : "",
                    l.FixedReserve.HasValue ? l.FixedReserve.Value.ToString("#,##0") : "",
                    String.Format("<a href='#' class='del' id='del_{0}'>Delete</a> ", l.Id.ToString()),
                    String.Format("<a href='#' class='edit' id='edit_{0}'>Edit</a> ", l.Id.ToString()),
                    l.Id,
                    l.Recipient,
                    counter++
                    );
            }

            m_View.ResponseText = tr.ToString();
        }

        public void Delete(int id)
        {
            var rr = m_Dao.GetById(id);
            m_Dao.Delete(rr);
        }

        public void RefreshPLRecapData(string month, string year)
        {
            m_Dao.RefreshPLRecap("all", month, year);
            m_View.ResponseText = String.Format("{{ Id : '0' }}");

        }

        public void RefreshAMBData(string counter)
        {
            if (counter.ToInteger() == 9)
            {
                BulkCopy();
                m_Dao.ConsolidateGL();
                int i = Convert.ToInt32(counter);
                i = 0;
                m_View.ResponseText = String.Format("{{ 'NextSql' : '{0}' }}", i);
            }
            else
            {
                var data = m_Dao.ReadGL(GetSQLStatementForAMB(counter));
                string tableName = counter.ToInteger() == 9 ? "xGEM_AMB_LTRAN" : "xGEM_AMB_JRN";

                m_Dao.InsertData(tableName, data);

                int i = Convert.ToInt32(counter) + 1;
                if (i == 10)
                {
                    m_Dao.ConsolidateGL();
                    i = 0;
                }
                m_View.ResponseText = String.Format("{{ 'NextSql' : '{0}' }}", i);
            }

            
        }

        public void ConsolidateGL()
        {
            m_Dao.TruncateTable("dbo.xGEM_PandLRecapGL");
            m_Dao.ConsolidateGL();
            m_View.ResponseText = String.Format("{{ 'Success' : '0' }}");
        }

        public void ClearAMBData()
        {
            m_Dao.TruncateTable("dbo.xGEM_AMB_JRN");
            m_Dao.TruncateTable("dbo.xGEM_AMB_LTRAN");
            m_Dao.TruncateTable("dbo.xGEM_PandLRecapGL");

            m_View.ResponseText = String.Format("{{ 'Success' : '0' }}");
        }

        private string GetSQLStatementForAMB(string counter)
        {
            string sql = "";
            
            switch (counter)
            {
                case "1":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where crgl like '40%' ";
                    break;

                case "2":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where drgl like '40%' ";
                    break;

                case "3":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where crgl like '50%' ";
                    break;

                case "4":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where drgl like '50%' ";
                    break;

                case "5":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where crgl like '51%' ";
                    break;

                case "6":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where drgl like '51%' ";
                    break;

                case "7":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where drgl like '20005%' ";
                    break;

                case "8":
                    sql = "select doctype, jrnseqnumb, drgl, crgl, postdate, glnumb, gldesc, invamt, amt, prndesc, ambflag, amblxcode from jrn where crgl like '20005%' ";
                    break;

                case "9":
                    sql = "select l.jrnseqnumb, l.ltranseqnumb, l.doctype, l.rectype, l.loannumb, l.acctnumb, l.amount, l.ldate, l.amblxcode, l.time_stamp, l.last_user, b.loanofficer from ltran l left join bloan b on l.loannumb = b.loannumb where l.time_stamp >= TO_DATE('01/APR/2015','dd/mon/yyyy') ";
                    break;

                default:
                    sql = "";
                    break;
            }

            return sql;
        }

        public void EMailReport(string ids, string month, string year, string pdfName, string reportName, string senderEmail, string senderFullname, string emailtome)
        {
            try
            {
                var list = m_Dao.GetByIds(ids).OrderBy(x => x.CostCenter);

                string host = ConfigurationManager.AppSettings["gemhost"];

                SmtpClient client = new SmtpClient(host);
                client.Timeout = 900000; //15 minutes

                NetworkCredential netCre =
                    new NetworkCredential("eibus", "Cordero01");
                client.Credentials = netCre;

                Crystal crystal = new Crystal(reportName)
                   .ConnectToDataDepot()
                   .SetDestinationToPDFAndRename(reportName, pdfName);

                foreach (var rr in list)
                {
                    GenerateReport(crystal, rr.CostCenter.Substring(1), month, year, pdfName, reportName);
                    SendReport(client, pdfName, rr.Recipient, senderEmail, senderFullname, rr.CostCenter.Substring(1), emailtome);
                    System.Threading.Thread.Sleep(3000);
                }

                crystal.Dispose();

                m_View.ResponseText = String.Format("{{ Id : '0' }}");
            }
            catch (Exception ex)
            {
                throw ex;
                //m_logger.DebugFormat("Exception: {0}", ex.Message);
                //m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        private void SendReport(SmtpClient client, string pdfName, string recipient, string senderEmail, string senderFullname, string branch, string emailtome)
        {
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("cbergam@gemcorp.com", "Chris Bergam");
            if (!String.IsNullOrEmpty(recipient))
            {
                //recipient.Split(new char[';']);
                //mm.To.Add(recipient);
                if (emailtome == "1")
                {
                    mm.To.Add(senderEmail);
                    //mm.To.Add("cbergam@gemcorp.com");

                }
                else
                {
                    mm.To.Add(recipient);
                    if (senderEmail.ToLower() != "cbergam@gemcorp.com")
                    {
                        mm.CC.Add("cbergam@gemcorp.com");
                    }
                    
                }
                //mm.To.Add(new MailAddress(recipient));
            }
            if (senderEmail.ToLower() != "eibus@gemcorp.com")
            {
                mm.CC.Add(new MailAddress(senderEmail));
            }
            mm.Bcc.Add("eibus@gemcorp.com");
            mm.Subject = "P & L Recap Report for Branch " + branch;
            //mm.Body = "Your branch Profit and Loss Statement is now available for viewing on the web-based reporting page.  Attached is a recap of your P&L showing key financial data.  If you are a nonproducing branch manager, you may request a P&L draw by signing, dating, and specifying the draw amount on the attachment.  Return the request to Chris Bergam, Controller.";
            mm.Body = "Your branch Profit and Loss Statement is now available for viewing on the web-based reporting page.  Attached is a recap of your P&L showing key financial data.  If you are a nonproducing branch manager, you may request a P&L draw by signing, dating, and specifying the draw amount on the attachment.  Return the request to Chris Bergam, Controller.  If you have questions regarding your Profit & Loss Statement, please contact Luis Rangel at (661) 283-1258.";
            mm.Attachments.Add(new Attachment(pdfName));

            client.Send(mm);
            mm.Dispose();

        }

        private void GenerateReport(Crystal crystal, string branch, string month, string year, string pdfName, string reportName)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            //crystal.ClearParameter();

            crystal
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@branch", branch)
               .AddParameter("@m1", month)
               .AddParameter("@y1", year)
               .ViewReport(true);

            //Crystal crystal = new Crystal(reportName)
            //   .ConnectToDataDepot()
            //   .SetDestinationToPDFAndRename(reportName, pdfName)
            //   .AddParameter("@branch", branch)
            //   .AddParameter("@m1", month)
            //   .AddParameter("@y1", year)
            //   .ViewReport(true);

            //crystal.Dispose();

            //int recCount = crystal.NumberOfRecordsSelected;

        }

        private void BulkCopy()
        {
            string oraCmd = "select CAST(l.jrnseqnumb AS varchar(2000)), CAST(l.ltranseqnumb AS varchar(2000)), CAST(l.doctype AS varchar(2000)), CAST(l.rectype AS varchar(2000)), CAST(l.loannumb AS varchar(2000)), CAST(l.acctnumb AS varchar(2000)), " +
                            "CAST(l.amount AS varchar(2000)), l.ldate , " +
                                "CAST(l.amblxcode AS varchar(2000)), l.time_stamp, CAST(l.last_user AS varchar(2000)), CAST(b.loanofficer AS varchar(2000)) from ltran l left join bloan b on l.loannumb = b.loannumb where l.time_stamp >= TO_DATE('01/JAN/2015','dd/mon/yyyy')  ";
            DataTable dt = new DataTable();

            using (OracleConnection oCon = new OracleConnection(ConfigurationManager.AppSettings["AMBConnectionString"]))
            {
                oCon.Open();

                using (OracleCommand oCmd = oCon.CreateCommand())
                {
                    oCmd.CommandType = CommandType.Text;
                    oCmd.CommandText = oraCmd;
                    OracleDataReader oDr = oCmd.ExecuteReader();
                    dt.Load(oDr);
                }
            }
            m_Dao.PerformBulkOperation(dt);
        }
    }
}
