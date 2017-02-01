using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;
using Bling.Domain.HR;

namespace Bling.Web.HR
{
    public partial class AddEnrolee : BasePage, IAddEnroleeView
    {
        private AddEnroleePresenter m_presenter;

        public InsuranceTitle InsuranceTitle { get; set; }
        public string EmployeeDropdown { get; set; }
        public string Rate1DropDown { get; set; }
        public string Rate3DropDown { get; set; }
        public string Rate4DropDown { get; set; }
        public string Rate5DropDown { get; set; }
        public string Rate6DropDown { get; set; }
        public string Rate7DropDown { get; set; }
        public string Rate9DropDown { get; set; }
        public string Rate10DropDown { get; set; }
        public string Rate11DropDown { get; set; }
        public string Rate12DropDown { get; set; }
        public string EEStatusDropDown { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_presenter.Load(Request.QueryString["branchNo"], Request.QueryString["yearmonth"]);
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new AddEnroleePresenter(this);
            base.OnInit(e);
        }

    }
}
