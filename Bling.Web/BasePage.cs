using System;
using System.Web.UI;
using Bling.Presenter;
using Bling.Domain;
using log4net;
using Bling.Web;
using Bling.Domain.Extension; 

namespace Bling.Web
{
    public class BasePage : Page, IBasePageView
    {
        private GEMUser m_CurrentUser;
        private BasePagePresenter m_presenter;        
        private string m_title = "GEMCentral";
        private string m_menu = "Home";
        protected string m_subTitle;
        protected static ILog m_logger;

        protected string ErrorMessage
        {
            get { return ((Bling)Master).ErrorMessage; }
            set { ((Bling)Master).ErrorMessage = value; }
        }

        protected string InfoMessage
        {
            get { return ((Bling)Master).Message; }
            set { ((Bling)Master).Message = value; }
        }

        public GEMUser CurrentUser
        {
            get { return Session["CurrentUser"] as GEMUser; }
            set { Session["CurrentUser"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            m_logger = LogManager.GetLogger(typeof(BasePage));
            m_presenter = new BasePagePresenter(this);

            int applicationId =  GetApplicationId();

            if (m_presenter.NotAllowed(applicationId))
                Response.Redirect("\\NoAccess.aspx");

            SetMenuAndTitle(applicationId);
            
            m_logger.DebugFormat("'{0}' access '{1}'.", CurrentUser.UserInfo.FullName, m_subTitle ?? m_title );

            base.OnInit(e);
        }

        protected void LogError(Exception ex)
        {
            ErrorMessage = ex.Message;
            m_logger.Error(ex.Message);
            m_logger.Error(ex.Source);
            m_logger.Error(ex.StackTrace);
        }

        public GEMUser GEMUser
        {
            set { m_CurrentUser = value; Session["CurrentUser"] = value; }
        }

        public string Menu
        {
            set { m_menu = value; }
        }

        public string PageTitle
        {
            set { m_title = value; }
        }

        private int GetApplicationId()
        {
            int applicationId = Request.QueryString["a"] != null ? Convert.ToInt32(Request.QueryString["a"]) : 0;

            if (applicationId == 0)
            {
                string pagename = Request.ServerVariables["SCRIPT_NAME"];
                applicationId = m_presenter.GetApplicationIdByLink(pagename);
            }
            return applicationId;
        }

        private void SetMenuAndTitle(int applicationId)
        {
            if (Session["Menu" + applicationId] != null)
            {
                string[] menuTitle = Session["Menu" + applicationId].ToString().Split('~');
                m_title = menuTitle[1];
                m_menu = menuTitle[0];
                m_logger.DebugFormat("Reading menu from the session for Application Id: {0}", applicationId);
            }
            else
            {
                m_presenter.BuildMenu(applicationId);
                Session["Menu" + applicationId] = String.Format("{0}~{1}", m_menu, m_title);
            }

            if (Page.Master != null)
            {
                ((Bling)Master).m_menu = m_menu;
                ((Bling)Master).DisplayName = CurrentUser.UserInfo.FirstName.Capitalize();
                Title = m_title;
            }
        }
    }
}
