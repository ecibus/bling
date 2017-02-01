using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Domain;

namespace Bling.Web.Funding
{
    public partial class FundingExceptionSummaryForm : BasePage
    {
        protected string MonthHtml { get; set; }
        protected string YearHtml { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            CalendarHtml cal = new CalendarHtml();

            MonthHtml = cal.MonthDropDown(DateTime.Now.AddMonths(-1).ToString("MM"));
            YearHtml = cal.YearDropDown2(2, DateTime.Now.Year.ToString());

        }
    }
}