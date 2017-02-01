using System;
using Bling.Domain;
using Bling.Repository;
using System.Collections.Generic;
using Bling.Domain.Processing;
using Bling.Repository.Processing;
using Bling.Repository.Calyx;
using System.Security.Principal;

namespace Bling.Presenter.Processing
{
    public class AjaxAppraiserSelectorPresenter : Presenter
    {
        private IAjaxView m_View;
        private IGenDao m_GenDao;
        private IAppraiserDao m_AppraiserDao;
        private ISelectedAppraiserDao m_SelectedAppraiserDao;
        private IOrderAppraisalDao m_OrderAppraisalDao;
        
        public AjaxAppraiserSelectorPresenter(IAjaxView view)
            : this (view, 
            new GenDao(DMDDataSession()), 
            new AppraiserDao(DMDDataSession()), 
            new SelectedAppraiserDao(DMDDataSession()),
            new OrderAppraisalDao(DMDDataSession())
            )
        {
            m_View = view;
        }

        public AjaxAppraiserSelectorPresenter(IAjaxView view, IGenDao genDao, IAppraiserDao appraiserDao,
            ISelectedAppraiserDao selectedAppraiserDao, IOrderAppraisalDao orderAppraisalDao)
        {
            m_View = view;
            m_GenDao = genDao;
            m_AppraiserDao = appraiserDao;
            m_SelectedAppraiserDao = selectedAppraiserDao;
            m_OrderAppraisalDao = orderAppraisalDao;            
        }

        public void LoadLoan(string loanNumber)
        {
            try
            {
                Gen gen = m_GenDao.GetByLoanNumber(loanNumber);

                m_View.ResponseText = gen.ToJson();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAppraiser(string loannumber, string loantype)
        {
            string county = m_AppraiserDao.GetCountyForLoan(loannumber);
            string branchno = loannumber.Substring(0, 3);

            IList<Appraiser> appraisers = m_AppraiserDao.GetAppraiserForBranchAndCounty(branchno, county);
            string lastAppraiser = m_SelectedAppraiserDao.GetLastAppraiserForBranch(branchno);

            m_View.ResponseText = Appraiser.ToHTMLDropDown(appraisers, lastAppraiser, loantype);
        }

        public void AddSelectedAppraiser(string loanNumber, string appraiserId, string addedBy)
        {
            try
            {
                SelectedAppraiser appraiser = new SelectedAppraiser
                {
                    BranchNo = loanNumber.Substring(0, 3),
                    LoanNumber = loanNumber,
                    AppraiserId = appraiserId,
                    AddedBy = addedBy
                };

                m_SelectedAppraiserDao.Save(appraiser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInDataTrac(OrderAppraisal orderAppraisal)
        {
            try
            {
                if (orderAppraisal.TicketNo != String.Empty)    
                    m_OrderAppraisalDao.SaveInDT(orderAppraisal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInPoint(string loannumber, string appraiserId)
        {
            try
            {
                UpdatePoint(loannumber, appraiserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdatePoint(string loannumber, string appraiserId)
        {
            string folderpath = m_OrderAppraisalDao.GetFolderPathInPoint(loannumber);

            Appraiser appraiser = m_AppraiserDao.GetById(appraiserId);

            using (IPointDao dao = new PointDao())
            {
                dao.OpenPExportFile(folderpath);
                dao.UpdateField(330, String.Format("{0} {1}", appraiser.FirstName, appraiser.LastName));
                dao.UpdateField(331, appraiser.Company);
                dao.UpdateField(332, appraiser.Phone.ToString());
                dao.UpdateField(333, appraiser.Address.Add1);
                dao.UpdateField(334, appraiser.Address.Add2);
                dao.UpdateField(335, appraiser.Fax.ToString());
                dao.UpdateField(12368, appraiser.EMail);
            }
        }
    }
}
