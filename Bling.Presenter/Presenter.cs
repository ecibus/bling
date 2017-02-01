using System;
using log4net;
using NHibernate;

namespace Bling.Presenter
{
    public class Presenter
    {
        public ILog m_logger;
        
        public static ISession DMDDataSession()
        {
            return StaticSessionManager.DMDDataSessionFactory.GetCurrentSession();
        }        

        public static ISession MWDataStoreSession()
        {
            return StaticSessionManager.MWDataStoreSessionFactory.GetCurrentSession();
        }

        public static ISession GEMAppSession()
        {            
            return StaticSessionManager.GEMAppSessionFactory.GetCurrentSession();
        }

        public static ISession GEMSql01Session()
        {
            return StaticSessionManager.GEMSql01SessionFactory.GetCurrentSession();
        }

        protected string LogError(Exception ex)
        {            
            m_logger.Error(ex.Message);
            m_logger.Error(ex.Source);
            m_logger.Error(ex.StackTrace);
            return ex.Message;
        }
    }
}
