using System;
using System.Linq;
using Bling.Domain.Underwriting;
using System.Collections.Generic;
using Bling.Repository.Underwriting;
using System.Text;

namespace Bling.Presenter.Underwriting
{
    public interface IScoreCardView
    {
        string ScoreHtml { set; }
    }

    public class ScoreCardPresenter : Presenter
    {
        private IScoreCardView m_View;
        private IScoreCardGroupDao m_Dao;

        public ScoreCardPresenter(IScoreCardView view) : this (view, new ScoreCardGroupDao(DMDDataSession()))        
        {
        }

        public ScoreCardPresenter(IScoreCardView view, IScoreCardGroupDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            IList<ScoreCardGroup> groups = m_Dao.GetAll();

            StringBuilder html = new StringBuilder();

            html.Append("<ul id ='ScoreCard'>");
            //groups.OrderBy(x => x.Ordering)
            groups.Where(x => x.Include).OrderBy(x => x.Ordering)            
                .ToList()
                .ForEach(x => html.Append(x.ToHtml()));

            html.Append("<li id='Total'>Final <span id='TotalScore'>0</span></li>");
            html.Append("</ul>");
            m_View.ScoreHtml = html.ToString();
        }
    }
}
