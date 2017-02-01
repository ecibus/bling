using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.LOS;
using Bling.Repository.LOS;

namespace Bling.Presenter.LOS
{
    public interface IHMDAView : IHMDA
    {        
        string CurrentMonthMessage { set; }
        string HMDALink { set; }
        string APRAndDenialLink { set; }
        string Year { get; }
        bool IncludeCurrentMonth { get; }
    }

    public class HMDAPresenter : Presenter
    {
        IHMDAView m_view;
        IHMDADao m_dao;
        
        public HMDAPresenter(IHMDAView view) : this(view, new HMDADao(DMDDataSession()))        
        {
            m_view = view;
        }

        public HMDAPresenter(IHMDAView view, IHMDADao dao)
        {
            m_view = view;
            m_dao = dao;

            DateTime now = m_view.Now;
            m_view.AvailableYear = new AvailableYear(now).GetListOfAvailableYear();
            m_view.CurrentMonthMessage = String.Format("Include {0:MMMM} Data?", now);
        }

        public List<HMDA> GenerateHMDA(string filename)
        {
            List<HMDA> hmdas = m_dao.GetAllData(m_view.Year, 
                DateTime.Now.Year.ToString() != m_view.Year ? true :
                    m_view.IncludeCurrentMonth).ToList();

            HMDA.SaveAsCSV(hmdas, filename);
            m_view.HMDALink = String.Format("Click <a href='{0}'>this link</a> to open your HMDA Data", filename.Substring(filename.IndexOf("HMDAData")));

            return hmdas;
        }

        public void GenerateAPRAndDenialWorkbook(string path, string filename, List<HMDA> hmdas)
        {
            new APRAndDenialWorkbook().Generate(path, filename, hmdas);
            m_view.APRAndDenialLink = String.Format("Click <a href='APRAndDenial/{0}'>this link</a> to open your APR and Denial Workbook", filename);
        }


    }
}
