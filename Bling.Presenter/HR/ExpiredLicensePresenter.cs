using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface IExpiredLicenseView
    {
        string List { set; }
        string DeadLine { get; }
        string AttachedFile { get; }
    }

    public class ExpiredLicensePresenter : Presenter
    {
        private IExpiredLicenseView m_View;
        private IExpiredLicenseDao m_Dao;

        public ExpiredLicensePresenter(IExpiredLicenseView view) : this(view, new ExpiredLicenseDao(DMDDataSession()))
        {
        }

        public ExpiredLicensePresenter(IExpiredLicenseView view, IExpiredLicenseDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }
        
        public void Load()
        {
            m_View.List = ExpiredLicense.ToHTMLTable(m_Dao.GetAllEmployee().ToList());
        }
        
        public void SendMail()
        {
            ExpiredLicenseEmail mail = new ExpiredLicenseEmail(m_View.AttachedFile);

            List<ExpiredLicense> list = m_Dao.GetAllEmployee().ToList();
            
            var branches = from b in list
                           select b.Branch;

            foreach (var branch in branches.Distinct()) 
            {
                var br = from b in list
                         where b.Branch == branch
                         select b;
                
                mail.Send(br.ToList(), Convert.ToDateTime(m_View.DeadLine));
            }
        }
    }
}
