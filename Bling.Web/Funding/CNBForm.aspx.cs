using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.Funding
{
    public partial class CNBForm : BasePage
    {
        public string FundedFrom { get; set; }
        public string FundedTo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            FundedFrom = DateTime.Now.ToShortDateString();
            FundedTo = DateTime.Now.ToShortDateString();

        }
    }
}