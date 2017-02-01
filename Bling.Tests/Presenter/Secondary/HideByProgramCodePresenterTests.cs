using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter.Secondary;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Tests.Presenter.Secondary
{
    [TestFixture]
    public class HideByProgramCodePresenterTests
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
        public void Should_be_able_to_set_program_code_dropdown_when_calling_load()
        {
            IHideByProgramCodeView view = m_mocks.DynamicMock<IHideByProgramCodeView>();
            ILSMapDao dao = m_mocks.DynamicMock<ILSMapDao>();

            using (m_mocks.Record())
            {
                Expect.Call(view.ProgramCodeDropdown = "dropdown html")
                    .Repeat.Once()
                    .IgnoreArguments();
                Expect.Call(dao.GetProgramCode())
                    .Repeat.Once()
                    .Return(new List<string> { "Code1" });
            }

            using (m_mocks.Playback())
            {
                HideByProgramCodePresenter presenter = new HideByProgramCodePresenter(view, dao);
                presenter.Load();
            }
        }
    }
}
