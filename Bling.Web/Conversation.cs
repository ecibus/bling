using System;
using System.Web;
using Bling.Presenter;
using log4net;
using NHibernate;
using NHibernate.Context;

namespace Bling.Web
{
    public class Conversation : IHttpModule
    {
        const string DMDDATASESSION = "nhibernate.session.dmddata";
        const string MWDATASTORESESSION = "nhibernate.session.mwdatastore";
        const string GEMAPPSESSION = "nhibernate.session.gemapp";
        const string PCLAPPSESSION = "nhibernate.session.pcldata";

        const string ENDFLAG = "nhibernate.session.end";
        static ILog m_logger = LogManager.GetLogger("Conversation");

        public void Dispose()
        {            
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(BeginRequest);
            context.PostRequestHandlerExecute += new EventHandler(EndRequest);
        }

        //public static void EndConversation()
        //{
        //    HttpContext.Current.Items[ENDFLAG] = true;
        //}

        public void BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            { 
                ISession DMDDataSession = (ISession)HttpContext.Current.Session[DMDDATASESSION];
                if (DMDDataSession == null)
                {
                    m_logger.Debug("Opening New Session for DMDData");
                    DMDDataSession = StaticSessionManager.OpenSessionForDMDData();
                    DMDDataSession.FlushMode = FlushMode.Never;
                }
                CurrentSessionContext.Bind(DMDDataSession);
                DMDDataSession.BeginTransaction();

                ISession MWDataStoreSession = (ISession)HttpContext.Current.Session[MWDATASTORESESSION];
                if (MWDataStoreSession == null)
                {
                    m_logger.Debug("Opening New Session for MWDataStore");
                    MWDataStoreSession = StaticSessionManager.OpenSessionForMWDataStore();
                    MWDataStoreSession.FlushMode = FlushMode.Never;
                }
                CurrentSessionContext.Bind(MWDataStoreSession);
                MWDataStoreSession.BeginTransaction();

                ISession GEMAppSession = (ISession)HttpContext.Current.Session[GEMAPPSESSION];
                if (GEMAppSession == null)
                {
                    m_logger.Debug("Opening New Session for GemApp");
                    GEMAppSession = StaticSessionManager.OpenSessionForGEMApp();
                    GEMAppSession.FlushMode = FlushMode.Never;
                }
                CurrentSessionContext.Bind(GEMAppSession);
                GEMAppSession.BeginTransaction();
                
                m_logger.Debug("Starting Conversation");
            }
        }

        public void EndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                ISession DMDDataSession = CurrentSessionContext.Unbind(StaticSessionManager.DMDDataSessionFactory);
                ISession MWDataStoreSession = CurrentSessionContext.Unbind(StaticSessionManager.MWDataStoreSessionFactory);
                ISession GEMAppSession = CurrentSessionContext.Unbind(StaticSessionManager.GEMAppSessionFactory);

                if (HttpContext.Current.Items[ENDFLAG] != null)
                {
                    DMDDataSession.Flush();
                    DMDDataSession.Transaction.Commit();
                    DMDDataSession.Close();
                    HttpContext.Current.Session[DMDDATASESSION] = null;

                    MWDataStoreSession.Flush();
                    MWDataStoreSession.Transaction.Commit();
                    MWDataStoreSession.Close();
                    HttpContext.Current.Session[MWDATASTORESESSION] = null;

                    GEMAppSession.Flush();
                    GEMAppSession.Transaction.Commit();
                    GEMAppSession.Close();
                    HttpContext.Current.Session[GEMAPPSESSION] = null;

                    m_logger.Debug("Ending Conversation");
                }
                else
                {
                    DMDDataSession.Transaction.Commit();
                    HttpContext.Current.Session[DMDDATASESSION] = DMDDataSession;
                    
                    MWDataStoreSession.Transaction.Commit();
                    HttpContext.Current.Session[MWDATASTORESESSION] = MWDataStoreSession;

                    GEMAppSession.Transaction.Commit();
                    HttpContext.Current.Session[GEMAPPSESSION] = GEMAppSession;

                    m_logger.Debug("Pausing Conversation");
                }
            }
        }

    }
}
