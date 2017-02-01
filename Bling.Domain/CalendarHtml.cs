using System;
using System.Text;

namespace Bling.Domain
{
    public class CalendarHtml
    {
        private DateTime m_currentDate;
        private string m_prefix;

        public CalendarHtml() : this ("")
        {            
        }

        public CalendarHtml(string prefix) : this (prefix, DateTime.Now)
        {            
        }

        public CalendarHtml(string prefix, DateTime currentDate)
        {
            m_currentDate = currentDate;
            m_prefix = prefix;
        }

        public string MonthDropDown(string selected)
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<select id='{0}_month'>", m_prefix);

            DateTime dt = new DateTime(m_currentDate.Year, 1, 1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime month = new DateTime(m_currentDate.Year, i, 1);
                html.AppendFormat("<option value='{0}' {1}='{1}'>{2}</option>", month.ToString("MM"), month.ToString("MM") == selected ? "selected" : "", 
                    month.ToString("MMMM"));
            }

            html.Append("</select>");
            return html.ToString();
        }

        public string YearDropDown(int count)
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<select id='{0}_year'>", m_prefix);

            for (int i = 0; i < count; i++)
            {
                html.AppendFormat("<option value='{0}'>{0}</option>", DateTime.Now.Year + i);
            }
                        
            html.Append("</select>");
            return html.ToString();
        }

        public string QuarterlyDropDown()
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<select id='{0}_quarter'>", m_prefix);
            html.AppendFormat("<option value='1'>{0}</option>", "1st");
            html.AppendFormat("<option value='2'>{0}</option>", "2nd");
            html.AppendFormat("<option value='3'>{0}</option>", "3rd");
            html.AppendFormat("<option value='4'>{0}</option>", "4th");


            html.Append("</select>");
            return html.ToString();
        }

        public string YearDropDown2(int count, string selectedyear)
        {
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<select id='{0}_year'>", m_prefix);

            for (int i = 0; i < count; i++)
            {
                html.AppendFormat("<option value='{0}' {1}='{1}'>{0}</option>", DateTime.Now.Year - 1 + i,
                    selectedyear == (DateTime.Now.Year - 1 + i).ToString() ? "selected" : "");
            }

            html.Append("</select>");
            return html.ToString();
        }
    }
}
