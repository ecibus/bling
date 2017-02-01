using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.HR;
using Bling.Domain;
using Bling.Domain.HR;

namespace Bling.Web.HR
{
    public partial class AjaxLOBasisPoints : BasePage, IAjaxView
    {
        private AjaxLOBasisPointsPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;
                switch (Request["Type"].ToString().ToLower())
                {
                    case "add":
                        BasisPoints bp = new BasisPoints
                        {
                            CreatedBy =  CurrentUser.UserInfo,
                            LoanOfficer = new UserInfo { EmployId = Request.Form["empId"] },
                            EffectiveDate = Convert.ToDateTime(Request.Form["effectiveDate"]),
                            InsideSalesRep = Convert.ToBoolean(Request.Form["insideSalesRep"]),
                            BaseCommission = Convert.ToDecimal(Request.Form["baseCommission"]),
                            Minimum = Convert.ToDecimal(Request.Form["minimum"]),
                            Maximum = Convert.ToDecimal(Request.Form["maximum"]),
                            Tier1 = Convert.ToDecimal(Request.Form["tier1"]),
                            Tier2 = Convert.ToDecimal(Request.Form["tier2"]),
                            Tier3 = Convert.ToDecimal(Request.Form["tier3"]),
                            Tier4 = Convert.ToDecimal(Request.Form["tier4"]),
                            Tier5 = Convert.ToDecimal(Request.Form["tier5"]),
                            Tier6 = Convert.ToDecimal(Request.Form["tier6"]),
                            BrokeredLoans = Convert.ToDecimal(Request.Form["brokeredloans"]),
                            BranchOverride = Convert.ToDecimal(Request.Form["branchOverride"]),
                            Manager = Convert.ToBoolean(Request.Form["Manager"]),
                            Weekly = Convert.ToBoolean(Request.Form["Weekly"]),
                            Broker = new Broker { Id = Request.Form["brokerid"] },
                            Enabled = true

                        };

                        m_Presenter.Save(bp);
                        break;

                    case "addbyte":
                        ByteBasisPoints bbp = new ByteBasisPoints
                        {
                            CreatedBy = CurrentUser.UserInfo.EmployId,
                            EmployeeId = Request.Form["empId"] ,
                            EffectiveDate = Convert.ToDateTime(Request.Form["effectiveDate"]),
                            InsideSalesRep = Convert.ToBoolean(Request.Form["insideSalesRep"]),
                            BaseCommission = Convert.ToDecimal(Request.Form["baseCommission"]),
                            Minimum = Convert.ToDecimal(Request.Form["minimum"]),
                            Maximum = Convert.ToDecimal(Request.Form["maximum"]),
                            Tier1 = Convert.ToDecimal(Request.Form["tier1"]),
                            Tier2 = Convert.ToDecimal(Request.Form["tier2"]),
                            Tier3 = Convert.ToDecimal(Request.Form["tier3"]),
                            Tier4 = Convert.ToDecimal(Request.Form["tier4"]),
                            Tier5 = Convert.ToDecimal(Request.Form["tier5"]),
                            Tier6 = Convert.ToDecimal(Request.Form["tier6"]),
                            BrokeredLoans = Convert.ToDecimal(Request.Form["brokeredloans"]),
                            BranchOverride = Convert.ToDecimal(Request.Form["branchOverride"]),
                            Manager = Convert.ToBoolean(Request.Form["Manager"]),
                            Weekly = Convert.ToBoolean(Request.Form["Weekly"]),
                            BrokerId = Request.Form["brokerid"],
                            Enabled = true

                        };

                        m_Presenter.SaveByte(bbp);
                        break;

                    case "getlobasispoints":
                        m_Presenter.GetBasisPointsByLO(Request.Form["empId"]);
                        break;

                    case "getbytelobasispoints":
                        m_Presenter.GetByteBasisPointsByLO(Request.Form["empId"], Request.Form["branchno"]);
                        break;

                    case "removelobasispoints":
                        m_Presenter.RemoveBasisPoints(Convert.ToInt32(Request.Form["bpid"]));
                        break;

                    case "removebytelobasispoints":
                        m_Presenter.RemoveByteBasisPoints(Convert.ToInt32(Request.Form["bpid"]));
                        break;

                    case "viewlobasispointreport":
                        string branchNo = Request.Form["branchNo"];
                        string report = Server.MapPath("Report/BranchBPISR.rpt");            
                        string pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "BasisPoints"));
                        string insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateBasisPointReport(report, pdfName, branchNo, insideSales);
                        break;

                    case "viewlobasispointhistory":
                        branchNo = Request.Form["branchNo"];
                        report = Server.MapPath("Report/BranchBPHistory.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "BasisPointsHistory"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateBasisPointReport(report, pdfName, branchNo, insideSales);
                        break;

                    case "viewbytelobasispointhistory":
                        branchNo = Request.Form["branchNo"];
                        report = Server.MapPath("Report/ByteBranchBPHistory.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "ByteBasisPointsHistory"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateBasisPointReport(report, pdfName, branchNo, insideSales);
                        break;

                    case "viewlobasispointcurrent":
                        branchNo = Request.Form["branchNo"];
                        report = Server.MapPath("Report/BranchBPCurrent.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "BasisPointsCurrent"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateBasisPointReport(report, pdfName, branchNo, insideSales);
                        break;

                    case "viewbytelobasispointcurrent":
                        branchNo = Request.Form["branchNo"];
                        report = Server.MapPath("Report/ByteBranchBPCurrent.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "ByteBasisPointsCurrent"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateBasisPointReport(report, pdfName, branchNo, insideSales);
                        break;

                    case "updatelobasispoint" :
                        int bpId = Convert.ToInt32(Request.Form["bpid"]);
                        string item = Request.Form["item"].ToLower();
                        string newValue = Request.Form["newValue"];
                        m_Presenter.UpdateBasisPoint(bpId, item, newValue);
                        break;

                    case "updatebytelobasispoint":
                        int bpId2 = Convert.ToInt32(Request.Form["bpid"]);
                        string item2 = Request.Form["item"].ToLower();
                        string newValue2 = Request.Form["newValue"];
                        m_Presenter.UpdateByteBasisPoint(bpId2, item2, newValue2);
                        break;

                    case "viewlobpcurrent" :
                        string empID = Request.Form["empID"];
                        report = Server.MapPath("Report/LoBpCurrent.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "LoBpCurrent"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateLoBpCurrentReport(report, pdfName, empID, insideSales);
                        break;

                    case "viewbytelobpcurrent":
                        empID = Request.Form["empID"];
                        report = Server.MapPath("Report/ByteLoBpCurrent.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "ByteLoBpCurrent"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateLoBpCurrentReport(report, pdfName, empID, insideSales);
                        break;

                    case "viewlobphistory":
                        empID = Request.Form["empID"];
                        report = Server.MapPath("Report/LoBpHistory.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "LoBpHistory"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateLoBpCurrentReport(report, pdfName, empID, insideSales);
                        break;

                    case "viewbytelobphistory":
                        empID = Request.Form["empID"];
                        report = Server.MapPath("Report/ByteLoBpHistory.rpt");
                        pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "ByteLoBpHistory"));
                        insideSales = Request.Form["insideSales"];
                        m_Presenter.GenerateLoBpCurrentReport(report, pdfName, empID, insideSales);
                        break;
                    
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxLOBasisPointsPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { get; set; }
    }
}