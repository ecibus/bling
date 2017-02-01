using System;
using System.Collections.Generic;
using Bling.Presenter.LOS;
using log4net;

namespace Bling.Web.LOS
{
    public partial class HMDA : BasePage, IHMDAView
    {
        private HMDAPresenter m_presenter;
        protected string m_CurrentMonthMessage;
        protected string m_HmdaLink;
        protected string m_APRAndDenialLink;
        protected string m_ValidationMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<string> AvailableYear
        {
            set
            {
                if (!Page.IsPostBack)
                    value.ForEach(year => ddlReportYear.Items.Add(year));
            }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new HMDAPresenter(this);
            m_logger = LogManager.GetLogger(typeof(HMDA));
            base.OnInit(e);
        }

        public string CurrentMonthMessage
        {
            set { m_CurrentMonthMessage = value; }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string current_date = String.Format("{0:MM_dd_yyyy_HH_mm_ss}", DateTime.Now);

            var hmdas = m_presenter.GenerateHMDA(String.Format("{0}\\{1}.csv", Server.MapPath("HMDAData"), current_date));

            if (chkAPRDenial.Checked)
                m_presenter.GenerateAPRAndDenialWorkbook(Server.MapPath("APRAndDenial"), 
                    String.Format("{0}.xls", current_date), 
                    hmdas);

            m_ValidationMessage = global::Bling.Domain.LOS.HMDA.ValidateDataAgainstRule(hmdas);
        }

        public string Year
        {
            get { return ddlReportYear.SelectedItem.Text; }
        }

        public bool IncludeCurrentMonth
        {
            get { return chkIncludeMonth.Checked; }
        }

        public string HMDALink
        {
            set { m_HmdaLink = value; }
        }

        public string APRAndDenialLink
        {
            set { m_APRAndDenialLink = value; }
        }

    }
}
