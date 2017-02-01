using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository;
using Bling.Domain;

namespace Bling.Presenter
{
    public class CSVExportPresenter : Presenter
    {
        private ICSVExportDao m_Dao;
        private string m_Path;


        public CSVExportPresenter()
            : this(new CSVExportDao(DMDDataSession()))
        {
        }

        public CSVExportPresenter(ICSVExportDao dao)
        {
            m_Dao = dao;
        }

        public List<CSVExport> GetByType(string type)
        {
            return m_Dao.GetCSVExportByType(type);
        }
    }
}
