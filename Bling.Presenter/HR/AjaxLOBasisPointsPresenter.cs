using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;
using System.IO;

namespace Bling.Presenter.HR
{
    public class AjaxLOBasisPointsPresenter : Presenter
    {
        private IAjaxView m_View;
        private ILOBasisPointsDao m_Dao;
        private IByteLOBasisPointsDao m_ByteDao;

        public AjaxLOBasisPointsPresenter(IAjaxView view) :
            this(view, new LOBasisPointsDao(DMDDataSession()), new ByteLOBasisPointsDao(DMDDataSession()))
        {
        }

        public AjaxLOBasisPointsPresenter(IAjaxView view, ILOBasisPointsDao dao, IByteLOBasisPointsDao byteDao)
        {
            m_View = view;
            m_Dao = dao;
            m_ByteDao = byteDao;
        }

        public void Save(BasisPoints bp)
        {
            m_Dao.Save(bp);
        }

        public void SaveByte(ByteBasisPoints bp)
        {
            m_ByteDao.Save(bp);
        }

        public void GetBasisPointsByLO(string empId)
        {
            IList<BasisPoints> bp = m_Dao.GetAllByLO(empId);
            BuildTable(bp);
        }

        public void BuildTable(IList<BasisPoints> bp)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table class='t1'>");
            table.AppendFormat("<thead>");

            table.AppendFormat(
                    "<tr class='yellow'>" +
                    "   <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>" +
                    "   <td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>" +
                    "   <td>{10}</td><td>{11}</td><td>{12}</td><td>{13}</td><td>{14}</td><td>{15}</td><td>{16}</td><td>{17}</td><td></td>" +
                    "</tr>",
                    "Branch", "Loan Officer", "Effective Date", "Weekly Pay", "Inside Sales Rep", "Base", "Brokered Loans", "Minimum",
                    "Maximum", "Tier 1", "Tier 2", "Tier 3", "Tier 4", "Tier 5", "Tier 6", "Override", "Manager",
                    "Added By"
                    );
            table.Append("</thead>");
            table.Append("<tbody>");

            foreach (var b in bp)
            {
                table.AppendFormat(
                    "<tr>" +
                    "   <td>{0}</td><td>{1}</td><td>{2}</td>" +
                    //"   <td>{4}</td><td>{5}</td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='Weekly_{16}' {3} /></td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='InsideSalesRep_{16}' {4} /></td>" +
                    "   <td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>" +
                    "   <td>{10}</td><td>{11}</td><td>{12}</td><td>{17}</td><td>{18}</td><td>{13}</td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='Manager_{16}' {14} /></td>" +
                    "   <td>{15}</td>" +
                    "   <td><img id='{16}' class='delete-bp' alt='Delete' src='/Images/Trash.gif' /></td>" +
                    "</tr>",
                    b.Broker.DBA,
                    b.LoanOfficer.FullName,
                    b.EffectiveDate.ToShortDateString(),
                    //b.Weekly ? "Yes" : "No", b.InsideSalesRep ? "Yes" : "No",
                    b.Weekly ? "checked='checked'" : "",
                    b.InsideSalesRep ? "checked='checked'" : "",
                    b.BaseCommission.ToString("0.0000"), b.BrokeredLoans.ToString("0.0000"), b.Minimum == 0 ? "" : b.Minimum.ToString("0.0000"),
                    b.Maximum == 0 ? "" : b.Maximum.ToString("0.00"),
                    b.Tier1.ToString("0.0000"), b.Tier2.ToString("0.0000"), b.Tier3.ToString("0.0000"), b.Tier4.ToString("0.0000"),
                    b.BranchOverride.ToString("0.0000"),
                    //b.Manager ? "Yes" : "No",
                    b.Manager ? "checked='checked'" : "",
                    b.CreatedBy.FullName,
                    b.Id, b.Tier5.ToString("0.0000"), b.Tier6.ToString("0.0000")

                    )
                ;
            }
            table.Append("<tbody>");
            table.Append("<table>");
            m_View.ResponseText = table.ToString();
        }

        public void GetByteBasisPointsByLO(string empId, string branchNo)
        {
            IList<ByteBasisPoints> bp = m_ByteDao.GetAllByLO(empId, branchNo);
            BuildByteTable(bp);
        }

        public void BuildByteTable(IList<ByteBasisPoints> bp)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table class='t1'>");
            table.AppendFormat("<thead>");

            table.AppendFormat(
                    "<tr class='yellow'>" +
                    "   <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>" +
                    "   <td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>" +
                    "   <td>{10}</td><td>{11}</td><td>{12}</td><td>{13}</td><td>{14}</td><td>{15}</td><td>{16}</td><td>{17}</td><td></td>" +
                    "</tr>",
                    "Branch", "Loan Officer", "Effective Date", "Weekly Pay", "Inside Sales Rep", "Base", "Brokered Loans", "Minimum",
                    "Maximum", "Tier 1", "Tier 2", "Tier 3", "Tier 4", "Tier 5", "Tier 6", "Override", "Manager",
                    "Added By"
                    );
            table.Append("</thead>");
            table.Append("<tbody>");
            
            foreach (var b in bp)
            {
                table.AppendFormat(
                    "<tr>" +
                    "   <td>{0}</td><td>{1}</td><td>{2}</td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='Weekly_{16}' {3} /></td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='InsideSalesRep_{16}' {4} /></td>" +
                    "   <td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>" +
                    "   <td>{10}</td><td>{11}</td><td>{12}</td><td>{17}</td><td>{18}</td><td>{13}</td>" +
                    "   <td><input type='checkbox' class='cbUpdate' id='Manager_{16}' {14} /></td>" + 
                    "   <td>{15}</td>" +
                    "   <td><img id='{16}' class='delete-bp' alt='Delete' src='/Images/Trash.gif' /></td>" +
                    "</tr>",
                    b.BranchName,
                    b.EmployeeName,
                    b.EffectiveDate.ToShortDateString(),
                    b.Weekly ? "checked='checked'" : "",
                    b.InsideSalesRep ? "checked='checked'" : "",
                    b.BaseCommission.ToString("0.0000"), b.BrokeredLoans.ToString("0.0000"), b.Minimum == 0 ? "" : b.Minimum.ToString("0.0000"),
                    b.Maximum == 0 ? "" : b.Maximum.ToString("0.00"),
                    b.Tier1.ToString("0.0000"), b.Tier2.ToString("0.0000"), b.Tier3.ToString("0.0000"), b.Tier4.ToString("0.0000"),
                    b.BranchOverride.ToString("0.0000"), 
                    b.Manager ? "checked='checked'" : "",
                    b.CreatedBy,
                    b.Id, b.Tier5.ToString("0.0000"), b.Tier6.ToString("0.0000")

                    )
                ;
            }
            table.Append("<tbody>");
            table.Append("<table>");
            m_View.ResponseText = table.ToString();
        }

        public void RemoveBasisPoints(int id)
        {
            m_Dao.RemoveBasisPoints(id);
        }

        public void RemoveByteBasisPoints(int id)
        {
            m_ByteDao.RemoveBasisPoints(id);
        }

        public void GenerateBasisPointReport(string reportName, string pdfName, string branchNo, string insideSales)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@branchNo", branchNo)
               .AddParameter("@insideSalesRep", insideSales)
               .ViewReport();
        }

        public void UpdateBasisPoint(int id, string item, string newValue)
        {
            m_Dao.UpdateBasisPoint(id, item, newValue);
        }

        public void UpdateByteBasisPoint(int id, string item, string newValue)
        {
            m_ByteDao.UpdateBasisPoint(id, item, newValue);
        }

        public void GenerateLoBpCurrentReport(string reportName, string pdfName, string empID, string insideSales)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            new Crystal(reportName)
                .ConnectToDataDepot()
                .SetDestinationToPDFAndRename(reportName, pdfName)
                .AddParameter("@empID", empID)
                .AddParameter("@insideSalesRep", insideSales)
                .ViewReport();
        }

        public void GenerateLoBpHistoryReport(string reportName, string pdfName, string empID, string insideSales)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            new Crystal(reportName)
                .ConnectToDataDepot()
                .SetDestinationToPDFAndRename(reportName, pdfName)
                .AddParameter("@empID", empID)
                .AddParameter("@insideSalesRep", insideSales)
                .ViewReport();
        }
    }


}
