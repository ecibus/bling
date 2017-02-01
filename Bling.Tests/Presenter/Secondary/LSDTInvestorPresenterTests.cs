using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Domain.Secondary;
using Bling.Presenter.Secondary;
using Bling.Repository;
using Bling.Repository.Secondary;
using NUnit.Framework;
using Rhino.Mocks;

namespace Bling.Tests.Presenter.Secondary
{
    [TestFixture]
    public class LSDTInvestorPresenterTests
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
        public void Should_be_able_to_return_empty_string_when_no_data_to_process()
        {
            ILoanSolutionDao lsdao = m_mocks.DynamicMock<ILoanSolutionDao>();
            ILSDTInvestorMapperView view = m_mocks.DynamicMock<ILSDTInvestorMapperView>();
            IInvestorDao idao = m_mocks.DynamicMock<IInvestorDao>();
            ILSDTInvestorMappingDao lsdtdao = m_mocks.DynamicMock<ILSDTInvestorMappingDao>();

            using (m_mocks.Record())
            {
                Expect.Call(lsdao.GetLSInvestor())
                    .Repeat.Once()
                    .Return(new List<string> ());

                string investorSelect = "<select id='one'>" +
                "<option value=''> -- Please Select --</option>" +
                "<option value='AAA' selected>INV (Name)</option>" +
                "</select>";

                string expected =
                    String.Format("<table>" +
                        "<tr><td>Loan Solution Investor</td><td>DataTrac Investor</td></tr>" +
                        "<tr><td>one</td><td>{0}</td></tr>" +
                    "</table>", investorSelect);

                Expect.Call(view.InvestorMapping = "");
            }

            using (m_mocks.Playback())
            {
                LSDTInvestorMapperPresenter presenter = new LSDTInvestorMapperPresenter(view, lsdao, idao, lsdtdao);
                presenter.LoadData();
            }
        }

        [Test]
        public void Should_be_able_to_build_html_table_with_matching_investor_code()
        {
            ILoanSolutionDao lsdao = m_mocks.DynamicMock<ILoanSolutionDao>();
            ILSDTInvestorMapperView view = m_mocks.DynamicMock<ILSDTInvestorMapperView>();
            IInvestorDao idao = m_mocks.DynamicMock<IInvestorDao>();
            ILSDTInvestorMappingDao lsdtdao = m_mocks.DynamicMock<ILSDTInvestorMappingDao>();

            using (m_mocks.Record())
            {
                Expect.Call(lsdao.GetLSInvestor())
                    .Repeat.Once()
                    .Return(new List<string> { "one" });

                Expect.Call(idao.GetAllActiveInvestor())
                    .Repeat.Once()
                    .Return(new List<Investor> { new Investor() { Id = "AAA", Inv = "INV", Name = "Name" } });

                Expect.Call(lsdtdao.GetAll())
                    .Repeat.Once()
                    .Return(new List<LSDTInvestorMapping>() { new LSDTInvestorMapping () { LoanSolutionInvestor = "one", DataTracInvestor="aaa"}} );


                string investorSelect = "<select id='one' class='s1'>" +
                "<option value=''> -- Please Select --</option>" +
                "<option value='AAA' selected>INV (Name)</option>" +
                "</select>";

                string expected = 
                    String.Format("<table>" +
                        "<tr><td>Loan Solution Investor</td><td>DataTrac Investor</td></tr>" +
                        "<tr><td>one</td><td>{0}</td></tr>" +
                    "</table>", investorSelect);

                Expect.Call(view.InvestorMapping = expected);
            }

            using (m_mocks.Playback())
            {
                LSDTInvestorMapperPresenter presenter = new LSDTInvestorMapperPresenter(view, lsdao, idao, lsdtdao);
                presenter.LoadData();
            }
        }

        [Test]
        public void Should_be_able_to_build_html_table_without_matching_investor_code()
        {
            ILoanSolutionDao lsdao = m_mocks.DynamicMock<ILoanSolutionDao>();
            ILSDTInvestorMapperView view = m_mocks.DynamicMock<ILSDTInvestorMapperView>();
            IInvestorDao idao = m_mocks.DynamicMock<IInvestorDao>();
            ILSDTInvestorMappingDao lsdtdao = m_mocks.DynamicMock<ILSDTInvestorMappingDao>();

            using (m_mocks.Record())
            {
                Expect.Call(lsdao.GetLSInvestor())
                    .Repeat.Once()
                    .Return(new List<string> { "one" });

                Expect.Call(idao.GetAllActiveInvestor())
                    .Repeat.Once()
                    .Return(new List<Investor> { new Investor() { Id = "AAA", Inv = "INV", Name = "Name" } });

                Expect.Call(lsdtdao.GetAll())
                    .Repeat.Once()
                    .Return(new List<LSDTInvestorMapping>() { new LSDTInvestorMapping() { LoanSolutionInvestor = "one", DataTracInvestor = "bbb" } });


                string investorSelect = "<select id='one' class='s1'>" +
                "<option value=''> -- Please Select --</option>" +
                "<option value='AAA'>INV (Name)</option>" +
                "</select>";

                string expected =
                    String.Format("<table>" +
                        "<tr><td>Loan Solution Investor</td><td>DataTrac Investor</td></tr>" +
                        "<tr><td>one</td><td>{0}</td></tr>" +
                    "</table>", investorSelect);
                Expect.Call(view.InvestorMapping = expected);
            }

            using (m_mocks.Playback())
            {
                LSDTInvestorMapperPresenter presenter = new LSDTInvestorMapperPresenter(view, lsdao, idao, lsdtdao);                
                presenter.LoadData();                
            }
        }
    }

    
}
