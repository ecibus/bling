using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface IReserveRequirementView
    {
        string Data { set; }
    }

    public class ReserveRequirementPresenter : Presenter
    {
        private IReserveRequirementView m_View;
        private IReserveRequirementDao m_Dao;

        public ReserveRequirementPresenter(IReserveRequirementView view)
            : this(view, new ReserveRequirementDao(DMDDataSession()))
        {
        }

        public ReserveRequirementPresenter(IReserveRequirementView view, IReserveRequirementDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            IList<ReserveRequirement> list = m_Dao.GetAll().OrderBy(x => x.CostCenter).ToList();

            StringBuilder tr = new StringBuilder();

            foreach (var l in list)
            {
                tr.AppendFormat("<tr><td>{0}</td><td class='number dollarAmount'>{1}</td><td class='number dollarAmount'>{2}</td><td></td></tr>",
                    l.CostCenter, l.ReserveMinimum.HasValue ? l.ReserveMinimum.Value.ToString("#,###") : "", 
                    l.FixedReserve.HasValue ? l.FixedReserve.Value.ToString("#,###") : ""
                    );
            }

            m_View.Data = tr.ToString();
        }
    }

}
