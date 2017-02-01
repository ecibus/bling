using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Domain;
using Bling.Domain.Extension;
using Bling.Presenter.IT;

namespace Bling.Web.IT
{
    public partial class AssignApplication : BasePage, IAssignApplicationView
    {
        private AssignApplicationPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    m_Presenter.Load();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbAvailableGroup.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Group.";
                    return;
                }

                if (lbApplication.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Application to be Added.";
                    return;
                }

                m_Presenter.AddApplication(lbAvailableGroup.SelectedItem.Text,
                    new GEMApplication { Id = lbApplication.SelectedValue.ToInteger(), 
                        ApplicationName = lbApplication.SelectedItem.Text });
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
                if (lbAvailableGroup.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Group.";
                    return;
                }
                if (lbMember.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Application to be Removed.";
                    return;
                }
                m_Presenter.RemoveApplication(lbAvailableGroup.SelectedItem.Text,
                    new GEMApplication
                    {
                        Id = lbMember.SelectedValue.ToInteger(),
                        ApplicationName = lbMember.SelectedItem.Text
                    });
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        
        protected void lbAvailableGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.GetGroupApplication(lbAvailableGroup.SelectedItem.Text);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }            
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AssignApplicationPresenter(this);
            base.OnInit(e);
        }


        public List<GEMApplication> AvailableApplication
        {
            set
            {
                lbApplication.Items.Clear();
                value.ForEach(apps => { lbApplication.Items.Add(new ListItem(apps.ApplicationName.RemoveHTMLTag(), 
                    apps.Id.ToString())); });
            }
        }

        public List<GEMGroup> AvailableGroup
        {
            set
            {
                lbAvailableGroup.Items.Clear();

                value.ForEach(group =>
                {
                    lbAvailableGroup.Items.Add(new ListItem(group.GroupName, group.Id.ToString()));
                });
            }
        }

        public List<GEMApplication> GroupApplication
        {
            set
            {
                lbMember.Items.Clear();

                value.ForEach(apps =>
                {
                    lbMember.Items.Add(new ListItem(apps.ApplicationName.RemoveHTMLTag(), apps.Id.ToString()));
                });
            }
        }

    }
}
