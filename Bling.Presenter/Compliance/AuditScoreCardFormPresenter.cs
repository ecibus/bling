using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository;
using Bling.Domain;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;

namespace Bling.Presenter.Compliance
{
    public interface IAuditScoreCardFormView
    {
        string InitialAuditorDropdown { get; set; }
        string ScoreHtml { get; set; }
    }

    public class AuditScoreCardFormPresenter : Presenter
    {
        private IAuditScoreCardFormView m_View;
        private IAuditScoreCardGroupDao m_Dao;
        private ILUPopupDao m_LUDao;

        public AuditScoreCardFormPresenter(IAuditScoreCardFormView view)
            : this (view, new AuditScoreCardGroupDao(DMDDataSession()), new LUPopupDao(DMDDataSession()))
        {
        }

        public AuditScoreCardFormPresenter(IAuditScoreCardFormView view, IAuditScoreCardGroupDao dao, ILUPopupDao luDao)
        {
            m_View = view;
            m_Dao = dao;
            m_LUDao = luDao;
        }

        public void Load()
        {
            IList<LookUp> lookup = m_LUDao.GetByType("shp_12").ToList().ConvertAll(t => new LookUp { Name = t.Description, Value = t.Alias });

            m_View.InitialAuditorDropdown = LookUp.ToHTMLDropDown(lookup.ToList(), "InitialAuditor");


            IList<AuditScoreCardGroup> groups = m_Dao.GetAll();

            StringBuilder html = new StringBuilder();

            html.Append("<ul id ='ScoreCard'>");
            //groups.OrderBy(x => x.Ordering)
            groups.Where(x => x.Include).OrderBy(x => x.Ordering)
                .ToList()
                .ForEach(x => html.Append(x.ToHtml()));

            html.Append("<li id='Total'>Final <span id='TotalScore'>0.00</span></li>");
            html.Append("<li><input type='button' id='btnCreateNote' value='Create Note in DataTrac' /></li>");
            html.Append("</ul>");
            m_View.ScoreHtml = html.ToString();
        }
    }
}
