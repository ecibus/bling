using System;
using System.Threading;
using System.Configuration;
using CRAXDRT;
using System.Collections.Generic;

namespace Bling.Presenter
{
    public class Crystal : IDisposable
    {
        private CRAXDRT.Application m_CrystalApplication;
        private CRAXDRT.Report m_CrystalReport;
        private string m_Path;


        public Crystal(string path)
        {
            m_Path = path;
            m_CrystalApplication = new CRAXDRT.Application();
        }

        public Crystal ConnectToSQLBeast()
        {
            string server = ConfigurationManager.AppSettings["SQLBeastServer"];
            string db = ConfigurationManager.AppSettings["SQLBeastDB"];
            string user = ConfigurationManager.AppSettings["SQLBeastUsername"];
            string pwd = ConfigurationManager.AppSettings["SQLBeastPassword"];

            OpenReport(server, db, user, pwd);

            /*
            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", user, pwd);
            m_CrystalReport.Database.LogOnServer("p2ssql.dll", server, db, user, pwd);
            */
            //m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", "sa", "ripple");
            //m_CrystalReport.Database.LogOnServer("p2ssql.dll", "sqlbeast", "mw_datastore", "sa", "ripple");
            return this;
        }

        public Crystal ConnectToDataDepot()
        {
            string server = ConfigurationManager.AppSettings["Server"];
            string db = ConfigurationManager.AppSettings["Database"];
            string user = ConfigurationManager.AppSettings["Username"];
            string pwd = ConfigurationManager.AppSettings["Password"];

            OpenReport(server, db, user, pwd);

            /*
            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", user, pwd);
            m_CrystalReport.Database.LogOnServer("p2ssql.dll", server, db, user, pwd);
            */

            //m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", "dmdreporting", "techies77!");
            //m_CrystalReport.Database.LogOnServer("p2ssql.dll", "datadepot\\sql2008", "dmd_data", "dmdreporting", "techies77!");
            return this;
        }

        public Crystal ConnectToDynamic()
        {
            string server = ConfigurationManager.AppSettings["DynamicServer"];
            string db = ConfigurationManager.AppSettings["DynamicDB"];
            string user = ConfigurationManager.AppSettings["DynamicUsername"];
            string pwd = ConfigurationManager.AppSettings["DynamicPassword"];

            OpenReport(server, db, user, pwd);

            /*
            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", user, pwd);
            m_CrystalReport.Database.LogOnServer("p2ssql.dll", server, db, user, pwd);
            */

            //m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", "sa", "sa4dynamic");
            //m_CrystalReport.Database.LogOnServer("p2ssql.dll", "dynamic", "gem_app", "sa", "sa4dynamic");
            return this;
        }

        public Crystal ConnectToAMB()
        {
            string oradb = "Data Source=Con5;User Id=admin;Password=admin";
            //oradb = "Data Source=(DESCRIPTION="
            //    + "(ADDRESS=(PROTOCOL=TCP)(HOST=GEMAMB01)(PORT=1521))"
            //    + "(CONNECT_DATA=(SERVICE_NAME=Con5)));"
            //    + "User Id=admin;Password=admin";
            string ambhost = "";

            oradb = ConfigurationManager.AppSettings["AMBConnectionString"];
            ambhost = ConfigurationManager.AppSettings["AMBHost"];

            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo(ambhost, "Con5", "admin", "admin");
            //m_CrystalReport.Database.Tables[2].SetLogOnInfo("GEMAMB01", "Con5", "admin", "admin");
            //m_CrystalReport.Database.LogOnServer("p2sora7.dll", "GEMAMB01", "Con5", "admin", "admin");
            //m_CrystalReport.Database.LogOnServer("p2soledb.dll", "GEMAMB01", "CON5", "admin", "admin");
            m_CrystalReport.Database.LogOnServer("crdb_oracle.dll", "", oradb, "", "");
            //m_CrystalReport.Database.LogOnServer("crdb_oracle.dll", "GEMAMB01", "Con5", "admin", "admin");
            //m_CrystalReport.Database. .LogOnServer("p2soledb.dll", "GEMAMB01", "", "", "", "", "");
            //m_CrystalReport.Database.Tables[1].ConnectionProperties["user id"] = "admin";
            

            return this;
        }

        public Crystal ConnectToPCL()
        {
            string server = ConfigurationManager.AppSettings["PCLServer"];
            string db = ConfigurationManager.AppSettings["PCLDatabase"];
            string user = ConfigurationManager.AppSettings["PCLUsername"];
            string pwd = ConfigurationManager.AppSettings["PCLPassword"];

            OpenReport(server, db, user, pwd);

            /*
            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", user, pwd);
            m_CrystalReport.Database.LogOnServer("p2ssql.dll", server, db, user, pwd);
            */

            //m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", "dmdreporting", "techies77!");
            //m_CrystalReport.Database.LogOnServer("p2ssql.dll", "datadepot\\sql2008", "dmd_data", "dmdreporting", "techies77!");
            return this;
        }

        private void OpenReport(string server, string db, string user, string pwd)
        {
            m_CrystalReport = m_CrystalApplication.OpenReport(m_Path, null);
            m_CrystalReport.Database.Tables[1].SetLogOnInfo("", "", user, pwd);
            m_CrystalReport.Database.LogOnServer("p2ssql.dll", server, db, user, pwd);
        }

        public List<CrystalInfo> GetReportInfo(List<string> filenames)
        {
            var crystalInfos = new List<CrystalInfo>();

            foreach (var file in filenames)
            {
                m_CrystalReport = m_CrystalApplication.OpenReport(file, null);

                if (m_CrystalReport != null)
                {
                    var ci = new CrystalInfo();
                    ci.Title = m_CrystalReport.ReportTitle;
                    ci.FileName = file;

                    var param = new List<CrystalParameter>();
                    ParameterFieldDefinitions p = m_CrystalReport.ParameterFields;
                    foreach (ParameterFieldDefinition d in p)
                    {
                        param.Add(new CrystalParameter
                            {
                                Name = d.ParameterFieldName.Replace("@", ""),
                                Prompt = d.Prompt,
                                Type = d.ValueType.ToString()
                            });
                    }
                    ci.Parameters = param;

                    crystalInfos.Add(ci);
                }
            }
            return crystalInfos;
        }

        public Crystal SetPaperToLegal()
        {
            m_CrystalReport.PaperSize = CRAXDRT.CRPaperSize.crPaperLegal;           
            return this;
        }

        public Crystal SetDestinationToPDF()
        {
            m_CrystalReport.ExportOptions.FormatType = CRAXDRT.CRExportFormatType.crEFTPortableDocFormat;            
            m_CrystalReport.ExportOptions.PDFExportAllPages = true;
            m_CrystalReport.ExportOptions.DestinationType = CRAXDRT.CRExportDestinationType.crEDTDiskFile;
            m_CrystalReport.ExportOptions.DiskFileName = m_Path.Replace(".rpt", ".pdf");
            return this;
        }

        public Crystal SetDestinationToPDFAndRename(string reportName, string pdfName)
        {
            m_CrystalReport.ExportOptions.FormatType = CRAXDRT.CRExportFormatType.crEFTPortableDocFormat;
            m_CrystalReport.ExportOptions.PDFExportAllPages = true;
            m_CrystalReport.ExportOptions.DestinationType = CRAXDRT.CRExportDestinationType.crEDTDiskFile;
            m_CrystalReport.ExportOptions.DiskFileName = m_Path.Replace(reportName, pdfName);
            return this;
        }

        public Crystal ViewReport()
        {
            return ViewReport(true);
        }

        public Crystal ViewReport(bool exportEmpty)
        {
            
            m_CrystalReport.ReadRecords();
            NumberOfRecordsSelected = m_CrystalReport.PrintingStatus.NumberOfRecordSelected;

            if (exportEmpty)
            {
                m_CrystalReport.Export(false);
            }
            else
            {
                if (NumberOfRecordsSelected > 0)
                    m_CrystalReport.Export(false);

                Thread.Sleep(1000);
            }
            return this;
        }

        public Crystal AddParameter(string name, object value)
        {
            m_CrystalReport.ParameterFields.GetItemByName(name, null)
                .ClearCurrentValueAndRange();

            m_CrystalReport.ParameterFields.GetItemByName(name, null)
                .AddCurrentValue(value);

            return this;
        }

        public Crystal ClearParameter()
        {
            int count = m_CrystalReport.ParameterFields.Count;
            for (int i = 0; i < count; i++)
            {
                m_CrystalReport.ParameterFields.Delete(i);
            }

            return this;
        }

        public int NumberOfRecordsSelected { get; set; }

        public void Dispose()
        {
            m_CrystalReport = null;
            m_CrystalApplication = null;
            //m_CrystalReport.Database.LogOffServer("p2ssql.dll", "datadepot\\sql2008");
        }
    }
}
