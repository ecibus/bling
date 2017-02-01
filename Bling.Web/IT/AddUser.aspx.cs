using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Bling.Domain;
using Bling.Presenter.IT;

namespace Bling.Web.IT
{
    public partial class AddUser : BasePage, IAddUserView
    {
        AddUserPresenter m_Presenter;

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
            m_Presenter = new AddUserPresenter(this);
            base.OnInit(e);
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
                GEMUser user = m_Presenter.Add(lbAvailableUser.SelectedValue);
                //InfoMessage = String.Format("{0} has been added as a user.", m_Presenter.GetFullName(user.ActorId));
                InfoMessage = String.Format("{0} has been added as a user.", m_Presenter.GetFullName2(user.UserName));
                
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
                    ErrorMessage = "Please select Allowed User to Remove";
                    return;
                }
                GEMUser user = m_Presenter.Remove(lbCurrentUser.SelectedValue);
                InfoMessage = String.Format("{0} has been remove as a user.", m_Presenter.GetFullName2(user.UserName));
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public List<GEMUser> CurrentAppUser
        {
            set
            {
                lbCurrentUser.Items.Clear();

                var byteUser = m_Presenter.GetByteAllUserFullName();

                value.ForEach(user =>
                {
                    //string fullname = user.UserInfo == null ? m_Presenter.GetFullName2(user.UserName) : user.UserInfo.FullName;
                    string fullname = byteUser.Find(u => u.Key.ToLower().Trim() == user.UserName.ToLower().Trim()).Value;
                    lbCurrentUser.Items.Add(new ListItem(fullname + " (" + user.UserName + ")", user.UserName));
                });
            }
        }

        public List<UserInfo> AvailableUser
        {
            set
            {
                lbAvailableUser.Items.Clear();
                value.ForEach(ui => lbAvailableUser.Items.Add(new ListItem(ui.FullName + " (" + ui.Actor.LoginName + ")", ui.Actor.LoginName)));
            }
        }

    }
}
