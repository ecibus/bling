using System;
using System.Web;
using Bling.Presenter;
using log4net;
using NHibernate;
using NHibernate.Context;

namespace Bling.Web
{
    public class SessionPerRequest : IHttpModule
    {
        static ILog m_logger = LogManager.GetLogger("Conversation");

        public void Dispose()
        {            
        }

        public void Init(HttpApplication context)
        {
            m_logger.Debug("Session Per Request");
            context.PreRequestHandlerExecute += new EventHandler(BeginRequest);
            context.PostRequestHandlerExecute += new EventHandler(EndRequest);
        }

        private void BeginRequest (object sender, EventArgs e)
        {
            ISession DMDDataSession = StaticSessionManager.OpenSessionForDMDData();
            DMDDataSession.BeginTransaction();
            CurrentSessionContext.Bind(DMDDataSession);

            ISession MWDataStoreSession = StaticSessionManager.OpenSessionForMWDataStore();
            MWDataStoreSession.BeginTransaction();
            CurrentSessionContext.Bind(MWDataStoreSession);

            ISession GEMAppSession = StaticSessionManager.OpenSessionForGEMApp();
            GEMAppSession.BeginTransaction();
            CurrentSessionContext.Bind(GEMAppSession);

            ISession GEMSql01Session = StaticSessionManager.OpenSessionForGEMSql01();
            GEMSql01Session.BeginTransaction();
            CurrentSessionContext.Bind(GEMSql01Session);
        }

        private void EndRequest(object sender, EventArgs e)
        {
            ISession DMDDataSession = CurrentSessionContext.Unbind(StaticSessionManager.DMDDataSessionFactory);
            ISession MWDataStoreSession = CurrentSessionContext.Unbind(StaticSessionManager.MWDataStoreSessionFactory);
            ISession GEMAppSession = CurrentSessionContext.Unbind(StaticSessionManager.GEMAppSessionFactory);
            ISession GEMSql01Session = CurrentSessionContext.Unbind(StaticSessionManager.GEMSql01SessionFactory);

            //m_logger.Debug("Closing Session");

            if (DMDDataSession != null)
            {
                try
                {
                    DMDDataSession.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    DMDDataSession.Transaction.Rollback();
                    m_logger.Error(ex.Message);
                }
                finally
                {
                    DMDDataSession.Close();
                }
            }

            if (MWDataStoreSession != null)
            {
                try
                {
                    MWDataStoreSession.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    MWDataStoreSession.Transaction.Rollback();
                    m_logger.Error(ex.Message);
                }
                finally
                {
                    MWDataStoreSession.Close();
                }
            }

            if (GEMAppSession != null)
            {
                try
                {
                    GEMAppSession.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    GEMAppSession.Transaction.Rollback();
                    m_logger.Error(ex.Message);
                }
                finally
                {
                    GEMAppSession.Close();
                }
            }

            if (GEMSql01Session != null)
            {
                try
                {
                    GEMSql01Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    GEMSql01Session.Transaction.Rollback();
                    m_logger.Error(ex.Message);
                }
                finally
                {
                    GEMSql01Session.Close();
                }
            }

        }
    }
}
