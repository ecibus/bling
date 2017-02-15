using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;

namespace Bling.Presenter.Accounting
{
    public class AjaxTurningPointPresenter : Presenter
    {
        private IAjaxView m_View;
        private ITurningPointDao m_Dao;

        public AjaxTurningPointPresenter(IAjaxView view)
            : this(view, new TurningPointDao(DMDDataSession()))
        {
        }

        public AjaxTurningPointPresenter(IAjaxView view, ITurningPointDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            var list = m_Dao.GetTurningPointUser();

            StringBuilder html = new StringBuilder();
            int c = 1;
            foreach (var tp in list)
            {
                html.AppendFormat(
                    "<tr>" +
                       "<td>{0}.</td>" +
                       "<td>{1}</div>" +
                       "<td>{2}</div>" +
                       "<td><a href='#' class='remove' id='{2}'>Remove</a></td>" +
                    "</tr>",
                    c,
                    tp.Fullname,
                    tp.Username
                    );
                c++;
            }

            m_View.ResponseText = html.ToString();
        }

        public void Search(string crit)
        {
            var list = m_Dao.SearchUser(crit);

            StringBuilder html = new StringBuilder();
            int c = 1;
            if (list.Count > 0)
            {
                html.Append("<thead>");
                html.Append("<tr class='yellow'>");
                html.Append("    <td></td>");
                html.Append("    <td>Loan Officer</td>");
                html.Append("    <td>Username</td>");
                html.Append("    <td>&nbsp;</td>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody>");
            }
            foreach (var tp in list)
            {
                html.AppendFormat(
                    "<tr>" +
                       "<td>{0}.</td>" +
                       "<td>{1}</div>" +
                       "<td>{2}</div>" +
                       "<td><a href='#' class='add' id='{2}'>Add</a></td>" +
                    "</tr>",
                    c,
                    tp.Fullname,
                    tp.Username
                    );
                c++;
            }
            if (list.Count > 0)
            {
                html.Append("</tbody>");
            }

            m_View.ResponseText = html.ToString();
        }

        public void Add(string username)
        {
            m_Dao.AddUser(username);
        }

        public void Remove(string username)
        {
            m_Dao.RemoveUser(username);
        }

    }
}
