using System;
using Bling.Presenter;
using Bling.Presenter.Processing;
using Bling.Domain.Processing;

namespace Bling.Web.Processing
{
    public partial class AjaxAppraiserSelector : BasePage, IAjaxView
    {
        public string ResponseText { set; get; }

        private AjaxAppraiserSelectorPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "loadloan":
                        m_Presenter.LoadLoan(Request["LoanNumber"]);
                        break;

                    case "getappraiser":
                        m_Presenter.GetAppraiser(Request["LoanNumber"], Request["LoanType"]);
                        break;

                    case "addselectedappraiser":                        
                        SaveSelectedAppraiser();
                        if (Request["TicketNo"].ToString() != String.Empty)
                        {
                            //SaveAppraiserInDataTrac();
                            SaveAppraiserInPoint();
                        }
                        ResponseText = "Added";
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

        private void SaveSelectedAppraiser()
        {
            m_Presenter.AddSelectedAppraiser(Request["LoanNumber"], Request["AppraiserId"], CurrentUser.EmployId);            
        }

        private void SaveAppraiserInDataTrac()
        {
            OrderAppraisal oa = new OrderAppraisal { 
               Appraiser = Request["AppraiserId"], 
               LoanNumber = Request["LoanNumber"],
               TicketNo = Request["TicketNo"],
               OrderedBy = CurrentUser.EmployId, 
               OrderedDate = DateTime.Now.ToShortDateString() };
            m_Presenter.SaveInDataTrac(oa);
        }

        private void SaveAppraiserInPoint()
        {
            m_Presenter.SaveInPoint(Request["LoanNumber"].ToString(), Request["AppraiserId"].ToString());
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxAppraiserSelectorPresenter(this);
            base.OnInit(e);
        }
    }
}
