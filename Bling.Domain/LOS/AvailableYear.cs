using System;
using System.Collections.Generic;

namespace Bling.Domain.LOS
{
    public class AvailableYear
    {
        private DateTime m_Now;

        public AvailableYear(DateTime now)
        {
            m_Now = now;
        }

        public List<string> GetListOfAvailableYear()
        {
            return new List<string> { m_Now.Year.ToString(), m_Now.AddYears(-1).Year.ToString() };
        }
    }
}
