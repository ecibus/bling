using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Presenter;
using Bling.Repository;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Presenter
{
    [TestFixture]
    public class MainPresenterTests : IMainView
    {
        private MockRepository m_mocks;
        private string m_allowedApplicationScript;
        private string m_applicationList;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();

        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_top_level_application()
        {
            IGEMApplicationDao dao = m_mocks.DynamicMock<IGEMApplicationDao>();
            
            List<GEMApplication> apps = new List<GEMApplication>();
            apps.Add (new GEMApplication() { ApplicationName = "AppName", Id = 1, Include = true, Image="Image"});

            using (m_mocks.Record())
            {
                Expect.Call(dao.GetApplicationByParent(0))
                    .Return(apps);
            }
            using (m_mocks.Playback())
            {
                MainPresenter presenter = new MainPresenter(this, dao);

                presenter.GetApplicationByParent(0);
                Assert.That(m_applicationList, Is.EqualTo("<ul id='main'><li id='App1' href=''><img src='Image' alt='AppName' /><br />AppName</li></ul>"));                                
            }
        }

        [Test]
        public void Should_be_able_to_generate_script_that_contains_allowed_application()
        {                 
            MainPresenter presenter = new MainPresenter(this, null);
            presenter.CreateScriptForAllowedApplication();

            Assert.That(m_allowedApplicationScript, Is.EqualTo("var allowed = [1, 2];"));
        }

        #region IMainView Members

        public string AllowedApplicationScript
        {
            set { m_allowedApplicationScript = value; }
        }

        public string ApplicationList
        {
            set { m_applicationList = value; }
        }

        public GEMUser CurrentUser
        {
            get
            {
                List<GEMApplication> apps = new List<GEMApplication>();
                apps.Add(new GEMApplication() { ApplicationName = "AppName1", Id = 1, Image = "Image1" });
                apps.Add(new GEMApplication() { ApplicationName = "AppName2", Id = 2, Image = "Image2" });

                IList<GEMGroup> groups = new List<GEMGroup>();
                groups.Add(new GEMGroup() { GroupName = "Group1", Id = 1, Applications = apps });

                GEMUser user = new GEMUser { UserName = "test", Groups = groups };

                return user;
            }
        }

        #endregion
    }
}
