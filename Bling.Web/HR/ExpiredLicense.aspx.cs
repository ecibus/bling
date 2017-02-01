using System;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class ExpiredLicense : BasePage, IExpiredLicenseView
    {
        private ExpiredLicensePresenter m_Presenter;
        protected string m_List;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                txtDeadline.Text = DateTime.Now.ToShortDateString();
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new ExpiredLicensePresenter(this);
            base.OnInit(e);
        }

        public string List
        {
            set { m_List = value; }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            m_Presenter.SendMail();
            InfoMessage = "Done.";
        }

        public string DeadLine
        {
            get { return txtDeadline.Text; }
        }

        public string AttachedFile
        {
            get { return Server.MapPath("Report/EmployeeChangeNotice.pdf"); }
        }

    }
}
