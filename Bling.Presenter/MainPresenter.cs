using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter
{
    public interface IMainView
    {
        GEMUser CurrentUser { get; }
        string AllowedApplicationScript { set; }
        string ApplicationList { set; }
    }

    public class MainPresenter : Presenter
    {
        private IMainView m_view;
        private IGEMApplicationDao m_applicationDao;

        public MainPresenter(IMainView view)
            : this(view, new GEMApplicationDao(DMDDataSession()))
        {
        }

        public MainPresenter(IMainView view, IGEMApplicationDao applicationDao)
        {
            m_view = view;
            m_applicationDao = applicationDao;
        }

        public void GetApplicationByParent(int parent)
        {
            List<GEMApplication> applications = m_applicationDao
                .GetApplicationByParent(parent)
                .ToList<GEMApplication>();

            StringBuilder list = new StringBuilder();
            
            list.Append(parent == 0 ? "<ul id='main'>" : "<ul id='submain'>");

            applications.FindAll(a => a.Include == true).ForEach(a =>
                list.AppendFormat(a.GetApplicationAsListItem())
                );
            
            
            list.Append("</ul>");

            m_view.ApplicationList = list.ToString();
        }

        public void CreateScriptForAllowedApplication()
        {
            IList<GEMGroup> groups = m_view.CurrentUser.Groups;
            StringBuilder script = new StringBuilder("var allowed = [");

            foreach (GEMGroup group in groups)
            {
                IList<GEMApplication> apps = group.Applications;
                foreach (GEMApplication app in apps)
                {
                    script.AppendFormat("{0}, ", app.Id);
                }                
            }
 
            script.Remove(script.Length - 2, 2);
            script.Append("];");
            m_view.AllowedApplicationScript = script.ToString();
        }

        public GEMApplication GetApplicationById(int id)
        {
            return m_applicationDao.GetApplicationById(id);
        }
    }
}
