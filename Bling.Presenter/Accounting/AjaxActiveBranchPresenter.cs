using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;
using Bling.Domain.Extension;

namespace Bling.Presenter.Accounting
{
    public class AjaxActiveBranchPresenter : Presenter
    {
        private IAjaxView m_View;
        private IActiveBranchDao m_Dao;

        public AjaxActiveBranchPresenter(IAjaxView view)
            : this(view, new ActiveBranchDao(DMDDataSession()))
        {
        }

        public AjaxActiveBranchPresenter(IAjaxView view, IActiveBranchDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Save(ActiveBranch ab)
        {
            m_Dao.Save(ab);
            m_View.ResponseText = String.Format("{{ Id : '{0}' }}", ab.Id);
        }

        public void Update(string id, string monthEnd, string currentMonth, string currentMonthMinus1, string currentMonthMinus2, string fytd)
        {
            try
            {
                m_Dao.Update(id, monthEnd, currentMonth, currentMonthMinus1, currentMonthMinus2, fytd);
                m_View.ResponseText = String.Format("{{ Id : '{0}' }}", id);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("duplicate"))
                {
                    throw new ApplicationException(String.Format("Month End {0} already exist in Active Branch table.  Please try again.", monthEnd));
                }
                else
                {
                    throw ex;
                }
            }
        }

        public void Load()
        {
            IList<ActiveBranch> list = m_Dao.GetAll().OrderByDescending(x => x.MonthEnd.ToDateTime()).ToList();

            StringBuilder tr = new StringBuilder();

            foreach (var l in list)
            {
                tr.AppendFormat(
                    "<tr>" +
                       "<td id='me_{6}'>{0}</td>" +
                       "<td class='number' id='cm_{6}'>{1}</td>" +
                       "<td class='number' id='cmm1_{6}'>{2}</td>" +
                       "<td class='number' id='cmm2_{6}'>{3}</td>" +
                       "<td class='number' id='fytd_{6}'>{7}</td>" +
                       "<td>{4}</td><td id='upd_{6}'>{5}</td></tr>",
                       
                    l.MonthEnd.ToDateTime().ToShortDateString(),
                    l.CurrentMonth.ToString("#,###"),
                    l.CurrentMonthMinus1.ToString("#,###"),
                    l.CurrentMonthMinus2.ToString("#,###"), 
                    String.Format("<a href='#' class='del' id='del_{0}'>Delete</a> ", l.Id.ToString()),
                    String.Format("<a href='#' class='edit' id='edit_{0}'>Edit</a> ", l.Id.ToString()),
                    l.Id,
                    l.FYTD

                    );
            }

            m_View.ResponseText = tr.ToString();
        }

        public void Delete(int id)
        {
            var rr = m_Dao.GetById(id);
            m_Dao.Delete(rr);
        }

    }
}
