using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Bling.Domain.RestApi.TurningPoint;
using Newtonsoft.Json;

namespace Bling.Presenter.RestApi.TurningPoint
{
    public class TurningPointPresenter : Presenter
    {
        private IAjaxView m_View;
        private ITurningPointDao m_Dao;

        public TurningPointPresenter(IAjaxView view)
            : this(view, new TurningPointDao(DMDDataSession()))
        {
        }

        public TurningPointPresenter(IAjaxView view, ITurningPointDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void SearchUser(string crit)
        {
            var result = m_Dao.SearchUser(crit);

            m_View.ResponseText = JsonConvert.SerializeObject(result);
        }

        public void GetTurningPointUser()
        {
            var result = m_Dao.GetTurningPointUser();

            m_View.ResponseText = JsonConvert.SerializeObject(result);
        }

    }
}
