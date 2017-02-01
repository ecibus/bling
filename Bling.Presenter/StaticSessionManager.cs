using System;
using NHibernate;
using NHibernate.Cfg;
using log4net;

namespace Bling.Presenter
{

    public class StaticSessionManager
    {
        public static readonly ISessionFactory DMDDataSessionFactory;
        public static readonly ISessionFactory MWDataStoreSessionFactory;
        public static readonly ISessionFactory GEMAppSessionFactory;
        public static readonly ISessionFactory GEMSql01SessionFactory;

        static StaticSessionManager()
        {
            ILog logger = LogManager.GetLogger(typeof(StaticSessionManager));

            try
            {                
                logger.Debug("Initializing Session Factory");

                if (DMDDataSessionFactory != null)
                    throw new Exception("trying to init SessionFactory twice!");

                string location = AppDomain.CurrentDomain.BaseDirectory;

                logger.Debug("location: " + location);

                DMDDataSessionFactory = new Configuration()
                    .Configure(String.Format("{0}/dmddata.cfg.xml", location))
                    .BuildSessionFactory();

                logger.Debug("configure dmdata");

                MWDataStoreSessionFactory = new Configuration()
                    .Configure(String.Format("{0}/mwdatastore.cfg.xml", location))
                    .BuildSessionFactory();

                logger.Debug("configure mwdata");

                GEMAppSessionFactory = new Configuration()
                    .Configure(String.Format("{0}/gemapp.cfg.xml", location))
                    .BuildSessionFactory();

                logger.Debug("configure gemapp");

                GEMSql01SessionFactory = new Configuration()
                    .Configure(String.Format("{0}/gemsql01.cfg.xml", location))
                    .BuildSessionFactory();

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("NHibernate initialization failed : {0}", ex.Message);
                throw new Exception("NHibernate initialization failed", ex);
            }
        }

        public static ISession OpenSessionForDMDData()
        {
            return DMDDataSessionFactory.OpenSession(); 
        }

        public static ISession OpenSessionForMWDataStore()
        {
            return MWDataStoreSessionFactory.OpenSession();
        }

        public static ISession OpenSessionForGEMApp()
        {            
            return GEMAppSessionFactory.OpenSession();
        }

        public static ISession OpenSessionForGEMSql01()
        {
            return GEMSql01SessionFactory.OpenSession();
        }
    }
}
