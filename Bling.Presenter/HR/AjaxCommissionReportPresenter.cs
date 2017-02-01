using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bling.Presenter.HR
{
    public class AjaxCommissionReportPresenter : Presenter
    {
        private IAjaxView m_view;

        public AjaxCommissionReportPresenter(IAjaxView view)
        {
        }

        public void ViewReport(string reportName, string pdfName, string type, string start, string end, string lo, string branchNo)
        {
            if (File.Exists(pdfName))
                File.Delete(pdfName);

            string criteria = type == "1" ? lo : branchNo;

            new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@start", start)
               .AddParameter("@end", end)
               .AddParameter("@type", type)
               .AddParameter("@criteria", criteria)
               .ViewReport();

            //new Crystal("C:\\SourceCode\\Bling\\Bling.Web\\HR\\Report\\ambtest.rpt")
            //   .ConnectToAMB()
            //    //.ConnectToDataDepot()
            //   .SetDestinationToPDFAndRename("report/ambtest.rpt", "report/ambtest.pdf")
            //   .AddParameter("startdate", "01-apr-2012")
            //   .AddParameter("enddate", "15-apr-2012")
            //   .ViewReport();
        }
    }
}
