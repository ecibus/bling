using System;
using System.Collections.Generic;
using Bling.Domain.LOS;
using Bling.Presenter.LOS;
using log4net;

namespace Bling.Web.LOS
{
    public partial class HMDAChanges : BasePage, IHMDAChangesView
    {
        private HMDAChangesPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new HMDAChangesPresenter(this);
            m_logger = LogManager.GetLogger(typeof(HMDAChanges));
            base.OnInit(e);
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public List<string> AvailableYear
        {
            set
            {
                if (!Page.IsPostBack)
                    value.ForEach(year => ddlReportYear.Items.Add(year));
            }
        }

        public List<HMDAField> FieldName
        {
            set
            {
                if (!Page.IsPostBack)
                    value.ForEach(field => ddlFields.Items.Add(field.Name));
            }
        }
    }
}
