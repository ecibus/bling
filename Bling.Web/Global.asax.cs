using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net;
using Bling.Domain;

namespace Bling.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger(typeof(Global));
            try
            {
                GEMUser user = Session["CurrentUser"] as GEMUser;
                logger.DebugFormat("Web Session end for {0}", user.UserInfo.FullName);            
            }
            catch (Exception ex)
            {                
                logger.ErrorFormat("Exception on Session End {0}", ex.Message);
            }            
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}