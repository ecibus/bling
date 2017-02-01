using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;
using log4net;
using System.Net.Mail;
using System.Configuration;

namespace Bling.Presenter.Secondary
{
    public interface IUploadMCMView
    {
        bool LockLoan { get; }
        bool ClosedLoan { get; }
        bool FalloutLoan { get; }
        bool FTP { get; }
        bool Trades { get; }
        bool IncludeByte { get; }
    }

    public class UploadMCMPresenter : Presenter
    {
        private IUploadMCMView m_View;
        private IMCMDao m_mcmDao;

        private const int NumberOfTry = 5;

        public UploadMCMPresenter(IUploadMCMView view)
            : this(view, new MCMDao(StaticSessionManager.DMDDataSessionFactory.OpenSession()))
        {
            m_View = view;
            m_logger = LogManager.GetLogger(typeof(UploadMCMPresenter));

        }

        public UploadMCMPresenter(IUploadMCMView view, IMCMDao dao)
        {
            m_View = view;
            m_mcmDao = dao;
        }

        public void SendFile()
        {
            string TargetDirectory = @"\\transmit\dept$\SecMrkt\MCM\Uploads"; // @"V:\SecMrkt\MCM\Uploads"; //
            //TargetDirectory = @"c:\\test";
            if (m_View.LockLoan)
                CreateLockLoan(TargetDirectory, m_View.IncludeByte);

            if (m_View.ClosedLoan)
                CreateClosedLoan(TargetDirectory, m_View.IncludeByte);

            if (m_View.FalloutLoan)
                CreateFalloutLoan(TargetDirectory, m_View.IncludeByte);

            if (m_View.Trades)
                CreateTradesFile(TargetDirectory, m_View.IncludeByte);
        }

        private void CreateTradesFile(string TargetDirectory, bool includeByte)
        {
            Archive(String.Format("{0}\\MCMTRDS.csv", TargetDirectory), "Trade File Archive");

            TryTradeFiles(TargetDirectory, includeByte);
        }

        private void TryTradeFiles(string TargetDirectory, bool includeByte)
        {
            var counter = 0;
            var filename = String.Format("{0}\\MCMTRDS.csv", TargetDirectory);
            while (counter < NumberOfTry)
            {
                counter++;
                try
                {
                    using (TextWriter writer = File.CreateText(filename))
                    {
                        IList<MCMTrades> closedLoan = m_mcmDao.GetTrades(includeByte ? 1 : 0);

                        writer.WriteLine(MCMTrades.Header());
                        closedLoan.ToList().ForEach(loan => writer.WriteLine(loan.ToString()));
                    }
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Length > 0)
                    {
                        counter = NumberOfTry;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.DebugFormat("Exception: {0}", ex.Message);
                }
            }

            TryFTP("MCMTRDS.csv", TargetDirectory);
            //if (m_View.FTP)
            //    new FTP().Upload("MCMTRDS.csv");
        }

        private void CreateFalloutLoan(string TargetDirectory, bool includeByte)
        {
            Archive(String.Format("{0}\\MCMFALL.csv", TargetDirectory), "Fallout File Archive");

            TryFalloutLoan(TargetDirectory, includeByte);
        }

        private void TryFalloutLoan(string TargetDirectory, bool includeByte)
        {
            var counter = 0;
            var filename = String.Format("{0}\\MCMFALL.csv", TargetDirectory);
            while (counter < NumberOfTry)
            {
                counter++;
                try
                {
                    using (TextWriter writer = File.CreateText(filename))
                    {
                        IList<MCMFallOutLoan> closedLoan = m_mcmDao.GetFallOutLoan(includeByte ? 1 : 0);

                        writer.WriteLine(MCMFallOutLoan.Header());
                        closedLoan.ToList().ForEach(loan => writer.WriteLine(loan.ToString()));
                    }
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Length > 0)
                    {
                        counter = NumberOfTry;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.DebugFormat("Exception: {0}", ex.Message);
                }
            }

            TryFTP("MCMFALL.csv", TargetDirectory);
        }

        private void CreateClosedLoan(string TargetDirectory, bool includeByte)
        {
            Archive(String.Format("{0}\\MCMCL.csv", TargetDirectory), "Closed File Archive");
            TryClosedLoan(TargetDirectory, includeByte);
        }

        private void TryClosedLoan(string TargetDirectory, bool includeByte)
        {
            var counter = 0;
            var filename = String.Format("{0}\\MCMCL.csv", TargetDirectory);
            while (counter < NumberOfTry)
            {
                counter++;
                try
                {
                    using (TextWriter writer = File.CreateText(filename))
                    {
                        IList<MCMClosedLoan> closedLoan = m_mcmDao.GetClosedLoan(includeByte ? 1 : 0);

                        writer.WriteLine(MCMClosedLoan.Header());
                        closedLoan.ToList().ForEach(loan => writer.WriteLine(loan.ToString()));
                    }
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Length > 0)
                    {
                        counter = NumberOfTry;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.DebugFormat("Exception: {0}", ex.Message);
                }
            }

            TryFTP("MCMCL.csv", TargetDirectory);
        }

        private void CreateLockLoan(string TargetDirectory, bool includeByte)
        {
            Archive(String.Format("{0}\\MCMLckd.csv", TargetDirectory), "Locked File Archive");
            TryLockLoan(TargetDirectory, includeByte);
        }

        private void TryLockLoan(string TargetDirectory, bool includeByte)
        {
            var counter = 0;
            var filename = String.Format("{0}\\MCMLckd.csv", TargetDirectory);
            while (counter < NumberOfTry)
            {
                counter++;
                try
                {
                    using (TextWriter writer = File.CreateText(filename))
                    {
                        IList<MCMLockedLoan> lockedLoan = m_mcmDao.GetLockedLoan(includeByte ? 1 : 0);

                        writer.WriteLine(MCMLockedLoan.Header());
                        lockedLoan.ToList().ForEach(loan => writer.WriteLine(loan.ToString()));
                    }
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Length > 0)
                    {
                        counter = NumberOfTry;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.DebugFormat("Exception: {0}", ex.Message);
                }

            }

            TryFTP("MCMLckd.csv", TargetDirectory);

            //if (m_View.FTP)
            //    new FTP().Upload("MCMLckd.csv");
        }

        public void TryFTP(string filename, string targetDirectory)
        {
            if (!m_View.FTP)
            { 
                return;
            }

            EmailFile(filename, targetDirectory);

            var counter = 0;

            while (counter < NumberOfTry)
            {
                counter++;
                try
                {
                    new FTP().Upload(filename);

                    counter = NumberOfTry;
                }
                catch (Exception ex)
                {
                    m_logger.DebugFormat("Exception: {0}", ex.Message);
                }
            }


        }

        public void Archive(string filename, string destination)
        {
            FileInfo fi = new FileInfo(filename);

            if (!fi.Exists)
            {
                return;
            }

            if (!String.IsNullOrEmpty(destination))
            {
                string timeStamp = "-" + fi.LastWriteTime.ToString("yyyyMMdd-HHmmss") + ".csv";
                string newName = (String.Format("{0}\\{1}\\{2}", fi.Directory, destination, fi.Name.Replace(".csv", timeStamp)));
                fi.MoveTo(newName);
            }

        }

        public void EmailFile(string filename, string targetDirectory)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("administrator@gemcorp.com");

                mm.To.Add("johnp@mortcap.com");
                mm.CC.Add("SecondaryMarketing@gemcorp.com");
                mm.Bcc.Add("eibus@gemcorp.com");
                
                mm.Subject = "GEM Mortgage - " + filename;

                mm.Body += "";

                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(String.Format("{0}\\{1}", targetDirectory, filename)));

                string host = ConfigurationManager.AppSettings["gemhost"];
                SmtpClient client = new SmtpClient(host);

                client.Send(mm);
                mm.Dispose();

            }
            catch (Exception ex)
            {
            }
        }



    }
}
