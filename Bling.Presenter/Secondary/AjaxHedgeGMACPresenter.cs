using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Secondary;
using System.IO;

namespace Bling.Presenter.Secondary
{
    public class AjaxHedgeGMACPresenter : Presenter
    {
        private IAjaxView m_View;
        private IHedgeGMACDao m_Dao;

        public AjaxHedgeGMACPresenter(IAjaxView view)
            : this(view, new HedgeGMACDao(DMDDataSession()))
        {
        }

        public AjaxHedgeGMACPresenter(IAjaxView view, IHedgeGMACDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Generate(string targetFile, string start, string end)
        {
            using (TextWriter writer = File.CreateText(String.Format("{0}/GMAC.csv", targetFile)))
            {
                IList<string> list = m_Dao.GetList(start, end);

                list.ToList().ForEach(line => writer.WriteLine(line.ToString()));
            }
        }
    }
}
