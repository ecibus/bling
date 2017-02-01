using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.Secondary
{
    public partial class TradeForm : BasePage
    {
        public string From { get; set; }
        public string To { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            From = To = DateTime.Now.ToString("MM/dd/yyyy");

        }

        public string ResponseText { get; set; }

    }
}