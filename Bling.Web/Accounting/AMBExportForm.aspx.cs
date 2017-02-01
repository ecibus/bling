using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.Accounting
{
    public partial class AMBExportForm : BasePage
    {
        public string FundedFrom { get; set; }
        public string FundedTo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            FundedFrom = FundedTo = DateTime.Now.ToShortDateString();
        }
    }
}