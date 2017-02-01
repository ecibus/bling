using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Tests.Presenter
{
    [TestFixture]
    public class BasePagePresenterTests 
    {
        private MockRepository m_mocks;
        private IGEMUserDao m_UserDao;
        private IGEMApplicationDao m_AppDao;
        private IBasePageView m_View;
        private GEMUser m_User;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_View = m_mocks.DynamicMock<IBasePageView>();
            m_UserDao = m_mocks.DynamicMock<IGEMUserDao>();
            m_AppDao = m_mocks.DynamicMock<IGEMApplicationDao>();

            List<GEMApplication> apps = new List<GEMApplication>();
            apps.Add(new GEMApplication() { Id = 1, ApplicationName = "App1" });
            apps.Add(new GEMApplication() { Id = 2, ApplicationName = "App2" });

            List<GEMGroup> groups = new List<GEMGroup>();
            groups.Add(new GEMGroup() { GroupName = "Group1", Id = 1, Applications = apps });
            m_User = new GEMUser { UserName = "test", Groups = groups };
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_current_user()
        {            
            using (m_mocks.Record())
            {
                Expect.Call(m_UserDao.GetCurrentUser())
                    .Return(m_User);
                m_View.CurrentUser = m_User;
                LastCall.Repeat.Once();
            }

            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
            }
        }

        [Test]
        public void Should_be_able_to_build_top_level_menu_and_title()
        {
            using (m_mocks.Record())
            {
                m_View.Menu = "Home";
                LastCall.Repeat.Once();

                m_View.PageTitle = "GEMCentral";
                LastCall.Repeat.Once();
            }            

            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                presenter.BuildMenu(0);
            }
        }

        [Test]
        public void Should_be_able_to_build_second_level_menu_and_title()
        {
            GEMApplication app1 = new GEMApplication() { ApplicationName = "App1", Id = 1, Parent = 0 };

            using (m_mocks.Record())
            {
                Expect.Call(m_AppDao.GetApplicationById(1)).Return(app1);

                m_View.Menu = "<a href='/Main.aspx'>Home</a> &#149; App1";
                LastCall.Repeat.Once();

                m_View.PageTitle = "App1";
                LastCall.Repeat.Once();
            }

            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                presenter.BuildMenu(1);
            }
        }

        [Test]
        public void Should_be_able_to_build_third_level_menu_and_title()
        {
            GEMApplication app1 = new GEMApplication() { ApplicationName = "App1", Id = 1, Parent = 0 };
            GEMApplication app2 = new GEMApplication() { ApplicationName = "App2", Id = 2, Parent = 1 };

            using (m_mocks.Record())
            {
                Expect.Call(m_AppDao.GetApplicationById(1)).Return(app1);
                Expect.Call(m_AppDao.GetApplicationById(2)).Return(app2);

                m_View.Menu = "<a href='/Main.aspx'>Home</a> &#149; <a href='?a=1'>App1</a> &#149; App2";
                LastCall.Repeat.Once();

                m_View.PageTitle = "App2";
                LastCall.Repeat.Once();
            }

            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                presenter.BuildMenu(2);
            }
        }

        [Test]
        public void NotAllowed_should_return_fakse_when_application_id_is_zero()
        {
            using (m_mocks.Record())
            {
            }
            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                Assert.That(presenter.NotAllowed(0), Is.False);
            }
        }

        [Test]
        public void NotAllowed_should_return_true_when_current_user_is_null()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.CurrentUser).Repeat.Once()
                    .Return(null);
            }
            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                Assert.That(presenter.NotAllowed(1), Is.True);
            }
        }

        [Test]
        public void Not_allowed_should_return_true_when_application_id_is_not_on_the_list()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.CurrentUser).Repeat.Once()
                    .Return(m_User);

            }
            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                Assert.That(presenter.NotAllowed(3), Is.True);
            }
        }

        [Test]
        public void Not_allowed_should_return_false_when_application_id_is_on_the_list()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.CurrentUser).Repeat.Once()
                    .Return(null);

                Expect.Call(m_UserDao.GetCurrentUser())
                    .Return(m_User);

                m_View.CurrentUser = m_User;
                LastCall.Repeat.Once();

                Expect.Call(m_View.CurrentUser).Repeat.Once()
                    .Return(m_User);

            }
            using (m_mocks.Playback())
            {
                BasePagePresenter presenter = new BasePagePresenter(m_View, m_UserDao, m_AppDao);
                Assert.That(presenter.NotAllowed(1), Is.False);
            }
        }

    }
}
