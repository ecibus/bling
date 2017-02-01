using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface ILOAdjustFormView
    {
        string LODropDown { set; }
        string FromPayDate { set; }
        string ToPayDate { set; }
    }

    public class LOAdjustFormPresenter : Presenter
    {
        private ILOAdjustFormView m_View;
        private ILOMasterDao m_LOMDao;

        public LOAdjustFormPresenter(ILOAdjustFormView view)
            : this (view, new LOMasterDao(DMDDataSession()))
            //: this (view, new LOMasterDao(MWDataStoreSession()))
        {
        }

        public LOAdjustFormPresenter(ILOAdjustFormView view, ILOMasterDao lomDao)
        {
            m_View = view;
            m_LOMDao = lomDao;
        }

        public void Load()
        {
            //IList<LOMaster> list = m_LOMDao.GetAll().Where(x => x.Name != null)
            //    .OrderBy(x => x.Code)
            //    .ToList();

            IList<LOMaster> list = m_LOMDao.GetActiveLO().ToList();

            m_View.LODropDown = LOMaster.ToHTMLDropDown(list);
            DateTime now = DateTime.Now;
            m_View.FromPayDate = now.Month.ToString() + "/01/" + now.Year.ToString();
            m_View.ToPayDate = now.ToShortDateString();
        }
    }
}
