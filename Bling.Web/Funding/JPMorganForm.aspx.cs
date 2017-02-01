using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.Funding
{
    public partial class JPMorganForm : BasePage
    {
        public string FundedFrom { get; set; }
        public string FundedTo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            FundedFrom = Convert.ToDateTime(now.Month.ToString() + "/1/" + now.Year.ToString()).ToString("MM/dd/yyyy");
            FundedTo = Convert.ToDateTime(now.AddMonths(1).Month.ToString() + "/1/" + now.AddMonths(1).Year.ToString()).AddDays(-1).ToString("MM/dd/yyyy");
        }
    }
}