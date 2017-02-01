using System;
using Bling.Domain.Accounting;
using Bling.Presenter.Accounting;

namespace Bling.Web.Accounting
{
    public class AddBranch<T> : BasePage, IAddBranchView<T>
            where T : IBranchId, new()
    {
        protected AddBranchPresenter<T> m_Presenter;
        protected string m_AvailableBranch;
        protected string m_TBranch;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.GetBranches();
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
                m_Presenter.AddBranch();
                m_Presenter.GetBranches();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AddBranchPresenter<T>(this);
            base.OnInit(e);
        }

        public string AvailableBranch
        {
            set { m_AvailableBranch = value; }
        }

        public string TBranch
        {
            set { m_TBranch = value; }
        }

        protected void SortOrder_CheckedChanged(object sender, EventArgs e)
        {
            m_Presenter.GetBranches();
        }

        public T NewBranch
        {
            get
            {
                return new T { BranchId = Request.Form["AvailableBranch"].ToString(), CreatedBy = CurrentUser.UserName };
            }
        }

        public virtual bool SortByBranchName
        {
            get { throw new NotImplementedException(); }
        }

    }
}
