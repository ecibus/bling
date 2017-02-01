using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Domain;
using Bling.Presenter.LOS;

namespace Bling.Web.LOS
{
    public partial class AddUserInUnderwritingReport : BasePage, IAddUserInReportView
    {
        private IAddUserInReportPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    m_Presenter.Load();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AddUserInUnderwritingReportPresenter(this);
            base.OnInit(e);
        }

        public List<ReportUser> CurrentReportUser
        {
            set
            {
                lbCurrentUser.Items.Clear();
                value.ForEach(ru => lbCurrentUser.Items.Add(new ListItem(ru.FullName, ru.EmployId)));
            }
        }

        public List<UserInfo> AvailableUser
        {
            set
            {
                lbAvailableUser.Items.Clear();
                value.ForEach(ui => lbAvailableUser.Items.Add(new ListItem(ui.FullName, ui.EmployId)));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbAvailableUser.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please select Available User to Add";
                    return;
                }
                ReportUser ru = m_Presenter.Add(lbAvailableUser.SelectedValue);
                InfoMessage = String.Format("{0} has been added to the report.", ru.FullName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbCurrentUser.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please select Report User to Remove";
                    return;
                }
                ReportUser ru = m_Presenter.Remove(lbCurrentUser.SelectedValue);
                InfoMessage = String.Format("{0} has been removed from the report.", ru.FullName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }
}
