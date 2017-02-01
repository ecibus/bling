using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.IT
{
    public interface IAssignApplicationView
    {
        List<GEMApplication> AvailableApplication { set; }
        List<GEMGroup> AvailableGroup { set; }
        List<GEMApplication> GroupApplication { set; }
    }

    public class AssignApplicationPresenter : Presenter
    {
        private IAssignApplicationView m_View;
        private IGEMApplicationDao m_GemApplicationDao;
        private IGEMGroupDao m_GemGroupDao;

        public AssignApplicationPresenter(IAssignApplicationView view)
            : this (view, new GEMApplicationDao(DMDDataSession()), new GEMGroupDao(DMDDataSession()))            
        {           
        }

        public AssignApplicationPresenter(IAssignApplicationView view, IGEMApplicationDao gemApplicationDao,
            IGEMGroupDao gemGroupDao)
        {
            m_View = view;
            m_GemApplicationDao = gemApplicationDao;
            m_GemGroupDao = gemGroupDao;
        }

        public void Load()
        {
            m_View.AvailableApplication = m_GemApplicationDao.GetAll().OrderBy(x => x.ApplicationName).ToList();
            m_View.AvailableGroup = m_GemGroupDao.GetAvailableGroup().OrderBy(x => x.GroupName).ToList();
        }

        public void GetGroupApplication(string groupName)
        {
            m_View.GroupApplication = m_GemGroupDao.GetApplicationByGroup(groupName).OrderBy(x => x.ApplicationName).ToList();
        }

        public void AddApplication(string groupName, GEMApplication app)
        {
            m_GemGroupDao.AddApplication(groupName, app);
            GetGroupApplication(groupName);
        }

        public void RemoveApplication(string groupName, GEMApplication app)
        {
            m_GemGroupDao.RemoveApplication(groupName, app);
            GetGroupApplication(groupName);
        }
    }
}
