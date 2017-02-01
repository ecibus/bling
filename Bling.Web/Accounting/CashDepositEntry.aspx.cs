using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Accounting;
using Bling.Domain.Accounting;

namespace Bling.Web.Accounting
{
    public partial class CashDepositEntry : BasePage, ICashDepositEntryView
    {
        private CashDepositEntryPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DepositDate = DateTime.Now.ToShortDateString();
            }

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new CashDepositEntryPresenter(this);
            m_Presenter.Load();
            base.OnInit(e);
        }

        //public IList<CashDepositAccount> Account { get; set; }


        public string Account { get; set; }
        public string DepositDate { get; set; }
    }
}