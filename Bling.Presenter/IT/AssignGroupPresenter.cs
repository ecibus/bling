using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.IT
{
    public interface IAssignGroupView
    {
        List<GEMUser> CurrentAppUser { set; }
        List<GEMGroup> AvailableGroup { set; }
        List<GEMGroup> MemberOf { set; }
    }

    public class AssignGroupPresenter : Presenter
    {
        private IAssignGroupView m_View;
        private IGEMUserDao m_GemUserDao;
        private IUserInfoDao m_UserInfoDao;
        private IGEMGroupDao m_GemGroupDao;

        public AssignGroupPresenter(IAssignGroupView view) :
            this(view, new GEMUserDao(DMDDataSession()), new UserInfoDao(DMDDataSession()),
                new GEMGroupDao(DMDDataSession()))            
        {
        }

        public AssignGroupPresenter(IAssignGroupView view, IGEMUserDao gemUserDao, IUserInfoDao userInfoDao,
            IGEMGroupDao gemGroupDao)
        {
            m_View = view;
            m_GemUserDao = gemUserDao;
            m_UserInfoDao = userInfoDao;
            m_GemGroupDao = gemGroupDao;
        }

        public void Load()
        {
            List<GEMUser> currentUser = m_GemUserDao.GetAllUser();
            List<GEMGroup> availableGroup = m_GemGroupDao.GetAvailableGroup();

            m_View.CurrentAppUser = currentUser;
            m_View.AvailableGroup = availableGroup;
        }

        public void GetUserAssociation(string username)
        {
            GEMUser gemUser = m_GemUserDao.GetUserByLoginName(username);
            List<GEMGroup> groups = gemUser.Groups.ToList();
            m_View.MemberOf = groups;
        }

        public void AddGroup(string username, GEMGroup group)
        {
            m_GemUserDao.AddGroup(username, group);
            GetUserAssociation(username);
        }

        public void RemoveGroup(string username, GEMGroup group)
        {
            m_GemUserDao.RemoveGroup(username, group);
            GetUserAssociation(username);
        }

        public string GetFullName(string actorId)
        {
            return m_UserInfoDao.GetByActorId(actorId).FullName;
        }
    }
}
