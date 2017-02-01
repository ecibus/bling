using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.LOS;
using Bling.Repository.LOS;

namespace Bling.Presenter.LOS
{
    public interface IHMDAChangesView : IHMDA
    {
        List<HMDAField> FieldName { set; }
    }

    public class HMDAChangesPresenter : Presenter
    {
        IHMDAChangesView m_View;
        IHMDAFieldDao m_Dao;

        public HMDAChangesPresenter(IHMDAChangesView view) : this(view, new HMDAFieldDao(DMDDataSession()))        
        {
        }

        public HMDAChangesPresenter(IHMDAChangesView view, IHMDAFieldDao dao)
        {
            m_View = view;
            m_Dao = dao;

            m_View.AvailableYear = new AvailableYear(m_View.Now).GetListOfAvailableYear();
            m_View.FieldName = dao.GetAll().OrderBy(field => field.Name).ToList();
        }
    }
}
