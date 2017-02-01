using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter.Secondary;
using Bling.Repository.Secondary;
using Bling.Domain.Secondary;

namespace Bling.Tests.Presenter.Secondary
{
    [TestFixture]
    public class HideByInvestorPresenterTests
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
        public void Should_be_able_to_set_Investor_Dropdown_when_load_is_called()
        {
            IHideByInvestorView view = m_mocks.DynamicMock<IHideByInvestorView>();
            ILSMapDao dao = m_mocks.DynamicMock<ILSMapDao>();
            IProgramCodeByInvestorDao pcbidao = m_mocks.DynamicMock<IProgramCodeByInvestorDao>();

            ProgramCodeByInvestor pcbi = new ProgramCodeByInvestor
            {
                Investor = "Investor1",
                Total = 100,
                Displayed = 50,
                Hidden = 50
            };
            List<ProgramCodeByInvestor> list = new List<ProgramCodeByInvestor> { pcbi };

            List<string> investor = new List<string> { "Investor1", "Investor2"};
            using (m_mocks.Record())
            {
                Expect.Call(view.InvestorDropdown = "").Repeat.Once().IgnoreArguments();
                Expect.Call(dao.GetInvestor()).Repeat.Once().Return(investor);
                Expect.Call(pcbidao.GetProgramCodeByInvestor(1)).Repeat.Once().Return(list);
            }
            using (m_mocks.Playback())
            {
                HideByInvestorPresenter presenter = new HideByInvestorPresenter(view, dao, pcbidao);
                presenter.Load();
            }
        }
    }
}
