using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.HR
{
    public partial class CommissionAnalysisForm : BasePage
    {
        public string LoanNumber { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoanNumber = Request.QueryString["ln"];
            }
        }
    }
}