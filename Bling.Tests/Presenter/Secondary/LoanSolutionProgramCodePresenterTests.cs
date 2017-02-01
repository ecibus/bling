using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter.Secondary;
using Bling.Repository.Secondary;
using System.IO;

namespace Bling.Tests.Presenter.Secondary
{
    [TestFixture]
    public class LoanSolutionProgramCodePresenterTests 
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
        public void Should_be_able_to_display_error_message_when_file_is_not_valid()
        {
            ILoanSolutionProgramCodeView view = m_mocks.StrictMock<ILoanSolutionProgramCodeView>();
            ILoanSolutionDao dao = m_mocks.StrictMock<ILoanSolutionDao>();

            string filename = @"..\..\Presenter\Secondary\Test1.csv";
            using (m_mocks.Record())
            {
                Expect.Call(view.SourceFileName).Repeat.Once().Return(filename);
                Expect.Call(view.Warning = "The file you are trying to upload is invalid.<br/>" +
                    "Expecting a header of \"InvestorName,InvestorProductName,InvestorProductCodeAlias\"");
            }

            using (m_mocks.Playback())
            {
                LoanSolutionProgramCodePresenter presenter = new LoanSolutionProgramCodePresenter(view, dao);
                presenter.LoadFile();
            }
        }

        [Test]
        public void Should_not_display_message_when_header_is_right_format()
        {
            ILoanSolutionProgramCodeView view = m_mocks.StrictMock<ILoanSolutionProgramCodeView>();
            ILoanSolutionDao dao = m_mocks.StrictMock<ILoanSolutionDao>();

            string filename = @"..\..\Presenter\Secondary\Test2.csv";
            using (m_mocks.Record())
            {
                Expect.Call(view.SourceFileName).Repeat.Once().Return(filename);
                Expect.Call(() => dao.DeleteAll()).Repeat.Once();
            }

            using (m_mocks.Playback())
            {
                LoanSolutionProgramCodePresenter presenter = new LoanSolutionProgramCodePresenter(view, dao);
                presenter.LoadFile();
            }
        }
        
    }
}
