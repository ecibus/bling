using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;
using Bling.Repository.Compliance;
using System.IO;
using Bling.Domain.Compliance;
using System.Text.RegularExpressions;

namespace Bling.Presenter.Compliance
{
    public class AjaxComplianceEasePresenter : Presenter
    {
        private IAjaxView m_View;
        private IComplianceEaseDao m_Dao;

        public AjaxComplianceEasePresenter(IAjaxView view)
            : this (view, new ComplianceEaseDao(DMDDataSession()))
        {
        }

        public AjaxComplianceEasePresenter(IAjaxView view, IComplianceEaseDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Generate(string path, string start, string end, string loans)
        { 
            try
            {
                using (TextWriter writer = File.CreateText(String.Format("{0}\\ComplianceEase.csv", path)))
                {
                    ComplianceEase ce = m_Dao.GetData(start, end, loans);

                    //ce.Header.ToList().ForEach(x => writer.Write(RemoveNumber(x) + (x == ce.Header.Last() ? "" : ",")));

                    ce.Header.ToList().ForEach(x => writer.Write(x + ","));

                    var feesCount = ce.Fees.Count;

                    for (var i = 1; i < feesCount; i++)
                    {
                        writer.Write("Fee,");
                    }

                    writer.Write("Fee");

                    foreach (var row in ce.Data.ToList())
                    {
                        int colCounter = 0;
                        writer.WriteLine("");
                        foreach (var col in row.ToList())
                        {
                            writer.Write("\"" + col + "\"" + (++colCounter < row.Count() ? "," : ""));                            
                        }
                    }

                    //var data = m_Dao.GetData2(start, end);

                    //foreach (var row in data)
                    //{
                    //    int colCount = row.Count;
                    //    int counter = 1;
                    //    foreach (var col in row)
                    //    {
                    //        writer.Write("\"{0}\"{1}", col, counter++ < colCount ? "," : "");
                    //    }
                    //    writer.WriteLine("");
                    //}
                }

                m_View.ResponseText = "{ FileName : 'ComplianceEase.csv' }";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        private string RemoveNumber(string s)
        {
            Regex r = new Regex("[0-9]");
            return r.Replace(s, "");            
        }
    }
}
