using System;
using Bling.Presenter;
using log4net;

namespace Bling.Web
{
    public partial class Main : BasePage, IMainView
    {
        private const string SESSION_ALLOWED_SCRIPT = "AllowedScript";
        private const string SESSION_APPLICATION_LIST = "ApplicationList{0}";
        private MainPresenter m_presenter;

        private int m_parentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_parentId = Request.QueryString["a"] != null ? Convert.ToInt32(Request.QueryString["a"]) : 0;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }        

        protected override void OnInit(EventArgs e)
        {
            m_logger = LogManager.GetLogger(typeof(Main));
            m_presenter = new MainPresenter(this);
            base.OnInit(e);
        }

        #region IMainView Members
        public string AllowedApplicationScript
        {
            set { Session[SESSION_ALLOWED_SCRIPT] = value; }
            get
            {
                if (Session[SESSION_ALLOWED_SCRIPT] == null)
                    m_presenter.CreateScriptForAllowedApplication();

                return Session[SESSION_ALLOWED_SCRIPT].ToString();
            }
        }

        public string ApplicationList
        {
            set { Session[String.Format(SESSION_APPLICATION_LIST, m_parentId)] = value; }
            get
            {
                string sessionName = String.Format(SESSION_APPLICATION_LIST, m_parentId);

                if (Session[sessionName] == null)
                    m_presenter.GetApplicationByParent(m_parentId);

                return Session[sessionName].ToString();
            }
        }
        #endregion
    }
}
