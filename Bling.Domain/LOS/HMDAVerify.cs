using System;
using System.Collections.Generic;
using System.Text;

namespace Bling.Domain.LOS
{    

    public interface IHMDARule
    {
        string Check(List<HMDA> list);
    }

    public class HMDAVerify
    {
        private List<IHMDARule> m_Rules = new List<IHMDARule>();
        private List<HMDA> m_HMDAList;

        public HMDAVerify(List<HMDA> list)
        {
            m_HMDAList = list;
        }

        public void RegisterRule(IHMDARule rule)
        {
            m_Rules.Add(rule);
        }

        public string GetWarningMessage()
        {
            StringBuilder message = new StringBuilder();

            m_Rules.ForEach(rule => message.Append(rule.Check(m_HMDAList)));

            if (message.ToString() == String.Empty)
                return String.Empty;

            message.Insert(0, "Validation Warning:<br/><ul>");
            message.Append("</ul>");
            return message.ToString();
        }
    }
}
