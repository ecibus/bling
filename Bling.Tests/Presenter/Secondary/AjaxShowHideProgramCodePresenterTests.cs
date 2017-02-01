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
using Bling.Presenter;

namespace Bling.Tests.Presenter.Secondary
{
    [TestFixture]
    public class AjaxShowHideProgramCodePresenterTests
    {
        private ILSMapDao m_Lsmapdao;
        private IProgramCodeByInvestorDao m_Pcbidao;
        private IAjaxView m_View;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_View = m_mocks.DynamicMock<IAjaxView>();
            m_Pcbidao = m_mocks.DynamicMock<IProgramCodeByInvestorDao>();
            m_Lsmapdao = m_mocks.DynamicMock<ILSMapDao>();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_set_response_text_when_getting_program_code_by_investor()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.ResponseText = "any data")
                    .Repeat.Once()
                    .IgnoreArguments();

                Expect.Call(m_Pcbidao.GetProgramCodeByInvestor(1))
                    .Repeat.Once()
                    .Return(new List<ProgramCodeByInvestor>());

            }
            using (m_mocks.Playback())
            {
                AjaxShowHideProgramCodePresenter presenter = new AjaxShowHideProgramCodePresenter(m_View, m_Pcbidao, m_Lsmapdao);
                presenter.GetProgramCodeByInvestor(1);
            }
        }

        [Test]
        public void Should_be_able_to_call_Show_Hide_By_Investor_and_set_response_text_when_ShowHideByInvestor_is_called()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.ResponseText = "any data")
                    .Repeat.Once()
                    .IgnoreArguments();

                m_Pcbidao.ShowHideByInvestor("investor", "1", "updatedby");
                LastCall.Repeat.Once();

                Expect.Call(m_Pcbidao.GetProgramCodeByInvestor(1))
                    .Repeat.Once()
                    .Return(new List<ProgramCodeByInvestor>());

            }
            using (m_mocks.Playback())
            {
                AjaxShowHideProgramCodePresenter presenter = new AjaxShowHideProgramCodePresenter(m_View, m_Pcbidao, m_Lsmapdao);
                presenter.ShowHideByInvestor("investor", "1", 1, "updatedby");
            }
        }

        [Test]
        public void Should_be_able_to_set_response_text_when_getting_program_by_program_code()
        {
            using (m_mocks.Record())
            {
                Expect.Call(m_View.ResponseText = "any data")
                    .Repeat.Once()
                    .IgnoreArguments();

                Expect.Call(m_Lsmapdao.GetByProgramCode("code"))
                    .Repeat.Once()
                    .Return (new List<LSMap>());
                
            }
            using (m_mocks.Playback()) 
            {
                AjaxShowHideProgramCodePresenter presenter = new AjaxShowHideProgramCodePresenter(m_View, m_Pcbidao, m_Lsmapdao);
                presenter.GetProgramByProgramCode("code");
            }
        }

        [Test]
        public void Update_program_code_should_load_lsmap_by_id_then_save_lsmap_then_load_by_product_code_and_set_the_response_text()
        {
            using (m_mocks.Record())
            {
                LSMap lsmap = new LSMap();

                Expect.Call(m_Lsmapdao.GetById(1))
                    .Repeat.Once()
                    .Return(lsmap);
                                
                Expect.Call(m_Lsmapdao.Save(lsmap))
                    .Repeat.Once()
                    .Return(lsmap);
            }

            using (m_mocks.Playback())
            {
                AjaxShowHideProgramCodePresenter presenter = new AjaxShowHideProgramCodePresenter(m_View, m_Pcbidao, m_Lsmapdao);
                presenter.UpdateProgramCode(1, true, "");

            }
        }
    }
}
