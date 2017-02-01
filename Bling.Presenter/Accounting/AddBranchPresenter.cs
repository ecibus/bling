using System;
using System.Linq;
using Bling.Domain.Accounting;
using Bling.Repository.Accounting;
using Bling.Repository;
using Bling.Domain.Extension;
using Bling.Domain;

namespace Bling.Presenter.Accounting
{
    public interface IAddBranchView <T>
    {
        string AvailableBranch { set; }
        string TBranch { set; }
        T NewBranch { get; }
        bool SortByBranchName { get; }
    }

    public class AddBranchPresenter<T> : Presenter
        where T : IBranchId
    {
        private IAddBranchView<T> m_View;
        private IBranchDao<T> m_Dao;
        private IBrokerDao m_BrokerDao;

        public AddBranchPresenter(IAddBranchView<T> view) :
            this(view, new BranchDao<T>(MWDataStoreSession()), new BrokerDao(DMDDataSession()))
        {
        }

        public AddBranchPresenter(IAddBranchView<T> view, IBranchDao<T> dao, IBrokerDao brokerdao)
        {
            m_View = view;
            m_Dao = dao;
            m_BrokerDao = brokerdao;
        }

        public void GetBranches()
        {            
            var available = m_BrokerDao.GetActiveBroker();
            var included = m_Dao.GetTBranch();
            bool sortByName = m_View.SortByBranchName;

            var includedBranch =
                included.Join(available, a => a.BranchId, b => b.IDNum.ToString(),
                    (a, b) => new Branch { BranchCode = b.IDNum.ToInteger(), BranchName = b.DBA }).ToList();

            includedBranch.ForEach(x =>
                available.Remove(available.Find(a => a.IDNum.ToInteger() == x.BranchCode))
                );

            m_View.AvailableBranch = Broker.ToHtmlOptionList("AvailableBranch",
                sortByName ? available.OrderBy(x => x.IDNum.ToInteger()).ToList()
                : available.OrderBy(x => x.IDNum.ToInteger()).ToList());

            m_View.TBranch = Branch.ToHtmlOptionList("TBranch",
                sortByName ? includedBranch.OrderBy(x => x.BranchName).ToList() : includedBranch.OrderBy(x => x.BranchCode).ToList());
        }

        public void AddBranch()
        {
            m_Dao.Add(m_View.NewBranch);
        }
    }
}
