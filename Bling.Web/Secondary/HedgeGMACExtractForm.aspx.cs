using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.Secondary
{
    public partial class HedgeGMACExtractForm : BasePage
    {
        public string FundedFrom { get; set; }
        public string FundedTo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            FundedFrom = "01/01/" + now.Year.ToString();
            FundedTo = now.ToShortDateString();
        }
    }
}