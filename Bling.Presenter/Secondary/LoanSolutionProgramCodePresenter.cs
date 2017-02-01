using System;
using System.IO;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public interface ILoanSolutionProgramCodeView
    {
        string SourceFileName { get; }
        string Warning { set; }
    }

    public class LoanSolutionProgramCodePresenter : Presenter
    {
        private ILoanSolutionProgramCodeView m_View;
        private ILoanSolutionDao m_Dao;

        public LoanSolutionProgramCodePresenter(ILoanSolutionProgramCodeView view)
            : this(view, new LoanSolutionDao(DMDDataSession()))
        {
        }

        public LoanSolutionProgramCodePresenter(ILoanSolutionProgramCodeView view, ILoanSolutionDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void LoadFile()
        {
            using (TextReader reader = File.OpenText(m_View.SourceFileName))
            {
                string header = reader.ReadLine();
                if (header != "InvestorName,InvestorProductName,InvestorProductCodeAlias")
                {
                    m_View.Warning = "The file you are trying to upload is invalid.<br/>" +
                               "Expecting a header of \"InvestorName,InvestorProductName,InvestorProductCodeAlias\"";
                }
                else
                {
                    m_Dao.DeleteAll();
                    while (reader.Peek() != -1)
                    {
                        LoanSolutionProgram lsp = new LoanSolutionProgram(reader.ReadLine());
                        m_Dao.Save(lsp);
                    }
                }
                reader.Close();
            }            
        }
    }
}
