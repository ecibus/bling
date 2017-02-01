using System;
using Bling.Domain;
using Bling.Domain.Extension;
using Bling.Repository;

namespace Bling.Presenter
{
    public interface IBasePageView
    {
        string Menu { set; }
        string PageTitle { set; }
        GEMUser CurrentUser { set; get; }
    }

    public class BasePagePresenter : Presenter
    {
        IBasePageView m_view;
        IGEMUserDao m_userDao;
        IGEMApplicationDao m_appDao;
  
        public BasePagePresenter(IBasePageView view) : 
            this(view, new GEMUserDao(DMDDataSession()), new GEMApplicationDao(DMDDataSession()))
        {            
        }

        public BasePagePresenter(IBasePageView view, IGEMUserDao userDao, IGEMApplicationDao appDao)
        {
            m_view = view;
            m_userDao = userDao;
            m_appDao = appDao;

            if (m_view.CurrentUser == null)
                m_view.CurrentUser = m_userDao.GetCurrentUser();
        }

        public void BuildMenu(int id)
        {
            string menu = "Home";
            string title = "GEMCentral";

            if (id != 0)
            {
                GEMApplication app = m_appDao.GetApplicationById(id);
                title = menu = app.ApplicationName.RemoveHTMLTag();

                while (app.Parent != 0)
                {
                    app = m_appDao.GetApplicationById(app.Parent);
                    menu = String.Format("{0} &#149; {1}", app.GetApplicationAsALink(), menu);
                }

                menu = String.Format("<a href='/Main.aspx'>Home</a> &#149; {0}", menu);
            }

            m_view.PageTitle = title;
            m_view.Menu = menu;
        }     
   
        public int GetApplicationIdByLink(string link)
        {
            if (link.ToLower().Contains("main.aspx"))
                return 0;

            GEMApplication app = m_appDao.GetApplicationByLink(link);
            if (app != null)
                return app.Id;

            return 0;
        }

        public bool NotAllowed(int applicationId)
        {
            if (applicationId == 0)
                return false ;

            GEMUser user = m_view.CurrentUser;
            //GEMUser user = m_userDao.GetCurrentUser();

            if (user == null)
                return true;

            
            foreach (GEMGroup group in user.Groups)
            {
                foreach (GEMApplication app in group.Applications)
                {
                    if (app.Id == applicationId)
                        return false;
                }
            }

            return true;
        }
    }
}
