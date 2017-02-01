using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bling.Web.HR
{
    public partial class StampPayDateForm : BasePage
    {
        public string PayDate { get; set; }
        public string EndDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime payDate = DateTime.Now;

            while (payDate.DayOfWeek != DayOfWeek.Friday)
            {
                payDate = payDate.AddDays(1);
            }

            PayDate = payDate.ToString("MM/dd/yyyy");
            EndDate = payDate.AddDays(-14).ToString("MM/dd/yyyy");
        }
    }
}