using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.IT
{
    public interface IAddUserView
    {
        List<GEMUser> CurrentAppUser { set; }
        List<UserInfo> AvailableUser { set; }
    }

    public class AddUserPresenter : Presenter
    {
        private IAddUserView m_View;
        private IUserInfoDao m_UserInfoDao;
        private IGEMUserDao m_GemUserDao;
        private IActorDao m_ActorDao;

        public AddUserPresenter(IAddUserView view) 
            : this(view, new UserInfoDao(DMDDataSession()), new GEMUserDao(DMDDataSession()), new ActorDao(DMDDataSession()))            
        {
        }

        public AddUserPresenter(IAddUserView view, IUserInfoDao userInfoDao, IGEMUserDao gemUserDao, IActorDao actorDao)
        {
            m_View = view;
            m_UserInfoDao = userInfoDao;
            m_GemUserDao = gemUserDao;
            m_ActorDao = actorDao;
        }

        public void Load()
        {
            List<UserInfo> availableUser = m_UserInfoDao.GetLicensedUser();
            List<GEMUser> currentUser = m_GemUserDao.GetAllUser();

            //currentUser.ForEach(x => availableUser.Remove(availableUser.Find(user => user.ActorId == x.ActorId)));
            currentUser.ForEach(x => availableUser.Remove(availableUser.Find(user => user.Actor.LoginName == x.UserName)));

            m_View.AvailableUser = availableUser;
            m_View.CurrentAppUser = currentUser;
        }

        public GEMUser Add(string loginName)
        {
            //var actor = m_ActorDao.GetByLoginName(loginName);

            string actorId = "";
            string employId = "";

            //if (actor != null)
            //{
            //    actorId = m_ActorDao.GetByLoginName(loginName).ActorId;
            //    employId = m_UserInfoDao.GetByActorId(actorId).EmployId;
            //}

            GEMUser gemUser = new GEMUser { UserName = loginName, ActorId = actorId, EmployId = employId };
            m_GemUserDao.Save(gemUser);

            //Load();
            return gemUser;
        }

        public GEMUser Remove(string loginName)
        {
            GEMUser user = m_GemUserDao.Delete(loginName);
            Load();
            return user;
        }

        public string GetFullName(string actorId)
        {
            return m_UserInfoDao.GetByActorId(actorId).FullName;
        }

        public string GetFullName2(string username)
        {
            return m_UserInfoDao.GetByteUserFullName(username);
        }

        public List<KeyValuePair<string, string>> GetByteAllUserFullName()
        {
            return m_UserInfoDao.GetByteAllUserFullName();
        }
    }
}
