using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Bling.Presenter.Accounting
{
    public class CrystalForCommission
    {
        public int GenerateLOCommission(string reportName, string pdfName, string payDate,
            string fundedAsOf, string employId, string isWeekly, bool generateEmpty)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(reportName);
            rpt.SetDatabaseLogon("DMDReporting", "techies77!", "DataTrac Data", "DMD_data");
            rpt.SetParameterValue("@paydate", payDate);
            rpt.SetParameterValue("@fundedasof", fundedAsOf);
            rpt.SetParameterValue("@employid", employId);
            rpt.SetParameterValue("@isweekly", Convert.ToBoolean(isWeekly));

            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdfName);

            rpt.Close();
            rpt.Dispose();

            return 1;
        }
    }
}
