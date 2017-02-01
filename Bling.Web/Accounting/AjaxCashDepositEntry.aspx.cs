using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Accounting;
using Bling.Domain.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AjaxCashDepositEntry : BasePage, IAjaxView
    {
        private AjaxCashDepositEntryPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load(Request.Form["inputdate"]);
                        break;
                    
                    case "add":
                        CashDeposit cd = new CashDeposit
                        {
                            AppNum = Request.Form["loanNo"],
                            Branch = Request.Form["branchNo"] == String.Empty ? null : Request.Form["branchNo"],
                            AccountNo = Request.Form["accountNo"],
                            DollarAmount = Convert.ToDecimal(Request.Form["amount"]),
                            BankAcct = Request.Form["bankAccount"],
                            InputDate = Convert.ToDateTime(Request.Form["depositDate"])
                        };
                        m_Presenter.Save(cd);
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
            m_Presenter = new AjaxCashDepositEntryPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}