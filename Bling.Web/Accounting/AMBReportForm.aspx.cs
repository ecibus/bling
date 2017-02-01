using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Domain.Extension;

namespace Bling.Web.Accounting
{
    public partial class AMBReportForm : BasePage
    {
        public string From { get; set; }
        public string To { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime firstOfTheMonth = (now.Month.ToString() + "/1/" + now.Year.ToString()).ToDateTime();

            From = firstOfTheMonth.ToString("MM/dd/yyyy");
            To = firstOfTheMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");

        }
    }
}