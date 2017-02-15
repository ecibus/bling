using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;

namespace Bling.Web.Accounting
{
    public partial class TurningPoint : BasePage, IAjaxView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string ResponseText { get; set; }

    }
}