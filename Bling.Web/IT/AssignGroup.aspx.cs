using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Domain;
using Bling.Domain.Extension;
using Bling.Presenter.IT;

namespace Bling.Web.IT
{
    public partial class AssignGroup : BasePage, IAssignGroupView
    {
        private AssignGroupPresenter m_Presenter;

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
            m_Presenter = new AssignGroupPresenter(this);
            base.OnInit(e);
        }

        public List<GEMUser> CurrentAppUser
        {
            set
            {
                lbCurrentUser.Items.Clear();

                value.ForEach(user =>
                {
                    string fullname = user.UserInfo == null ? m_Presenter.GetFullName(user.ActorId) : user.UserInfo.FullName;
                    lbCurrentUser.Items.Add(new ListItem(fullname, user.UserName));
                });
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


        public List<GEMGroup> MemberOf
        {
            set
            {
                lbMemberOf.Items.Clear();

                value.ForEach(group =>
                {
                    lbMemberOf.Items.Add(new ListItem(group.GroupName, group.Id.ToString()));
                });
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbCurrentUser.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select User to Modify.";
                    return;
                }

                if (lbAvailableGroup.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Group to be Added.";
                    return;
                }
                
                ListItem groupToAdd = lbAvailableGroup.SelectedItem;
                if (lbMemberOf.Items.Contains(groupToAdd))
                {
                    ErrorMessage = String.Format("{0} is already a member of {1}", 
                        lbCurrentUser.SelectedItem.Text, groupToAdd.Text);
                    return;
                }

                m_Presenter.AddGroup(lbCurrentUser.SelectedValue, 
                    new GEMGroup { Id = lbAvailableGroup.SelectedValue.ToInteger(), 
                        GroupName = lbAvailableGroup.SelectedItem.Text });

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
                    ErrorMessage = "Please Select User to Modify.";
                    return;
                }

                if (lbMemberOf.SelectedValue == String.Empty)
                {
                    ErrorMessage = "Please Select Group to be Removed.";
                    return;
                }

                m_Presenter.RemoveGroup(lbCurrentUser.SelectedValue, 
                    new GEMGroup { Id = lbMemberOf.SelectedValue.ToInteger(), 
                        GroupName = lbMemberOf.SelectedItem.Text });

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void lbCurrentUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.GetUserAssociation(lbCurrentUser.SelectedValue);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }            
        }

    }
}
