using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter.LOS;
using Bling.Domain.LOS;
using NHibernate;
using NHibernate.Context;
using Bling.Presenter;
using Bling.Repository.LOS;

namespace Bling.Tests.Presenter.LOS
{
    [TestFixture]
    public class HMDAPresenterTests
    {
        private MockRepository m_mocks;

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
        public void Should_be_able_to_get_list_of_year()
        {
            IHMDAView view = m_mocks.DynamicMock<IHMDAView>();
            IHMDADao dao = m_mocks.DynamicMock<IHMDADao>();

            using (m_mocks.Record())
            {
                Expect.Call(view.Now).Repeat.Once().Return(new DateTime(2008, 1, 1));
                Expect.Call(view.AvailableYear = new List<string> { "2008", "2007" });
            }
            using (m_mocks.Playback())
            {
                HMDAPresenter presenter = new HMDAPresenter(view, dao);                
            }
        }

        [Test]
        public void Should_be_able_to_get_current_month()
        {
            IHMDAView view = m_mocks.DynamicMock<IHMDAView>();
            IHMDADao dao = m_mocks.DynamicMock<IHMDADao>();

            using (m_mocks.Record())
            {
                Expect.Call(view.Now).Repeat.Once().Return(new DateTime(2008, 2, 1));
                Expect.Call(view.CurrentMonthMessage = "Include February Data?");
            }
            using (m_mocks.Playback())
            {
                HMDAPresenter presenter = new HMDAPresenter(view, dao);
            }
        }

        
    }
}
