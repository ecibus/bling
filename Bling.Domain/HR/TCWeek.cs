using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class TCWeek : TCTotal
    {
        private List<TCLineItems> m_Week { get; set; }

        public TCWeek ()
        {
            m_Week = new List<TCLineItems>();
        }

        public int Days
        {
            get { return m_Week.Count; }
        }

        public void AddDay(TCLineItems day)
        {
            m_Week.Add(day);
        }

        public void Summarize(decimal lastWeekHours, bool isLastWeek)
        {
            decimal weekWorkingHours = 0.0m;
            decimal missedWeekHours = 0.0m;

            foreach (var day in m_Week)
            {
                day.Calculate();
                
                CalculateWorkHours(day);
                CalculateMissedHours(day);

                if (day.WorkDay != "Sat" && day.WorkDay != "Sun" && day.Reason == "N")
                {
                    weekWorkingHours += 8.0m;
                }
            }

            if (isLastWeek)
            {
                //weekWorkingHours = 40;
            }

            if (Weekend > 0)
            {
                decimal regWeekend = 0;

                missedWeekHours = weekWorkingHours - Reg - lastWeekHours;
                if (missedWeekHours > 0)
                {
                    if (Weekend >= missedWeekHours)
                    {
                        Reg += missedWeekHours;
                        Weekend -= missedWeekHours;
                        NotPaid -= missedWeekHours;
                    }
                    else
                    {
                        Reg += Weekend;
                        NotPaid -= Weekend;
                        Weekend = 0;
                    }
                }
                OT += Weekend;
            }

            NotPaid -= MakeUp;
            NetOT = OT;
        }

        public decimal WeekHours
        {
            get { return Reg + Sick + Vacation + Holiday + Bereave; }
        }

        private void CalculateWorkHours(TCLineItems day)
        {
            Reg += day.CalculatedTotal.Reg;
            OT += day.CalculatedTotal.OT + day.ExtraWeekdayWork;
            DT += day.CalculatedTotal.DT;
            Weekend += day.CalculatedTotal.Weekend;

        }

        private void CalculateMissedHours(TCLineItems day)
        {
            switch (day.Reason)
            {
                case "S":
                    Sick += Convert.ToDecimal(day.Hour2);
                    break;
                case "V":
                    Vacation += Convert.ToDecimal(day.Hour2);
                    break;
                case "H":
                    Holiday += Convert.ToDecimal(day.Hour2);
                    break;
                case "B":
                    Bereave += Convert.ToDecimal(day.Hour2);
                    break;
                case "M":
                    MakeUp += Convert.ToDecimal(day.ExtraWeekdayWork);
                    OT -= Convert.ToDecimal(day.ExtraWeekdayWork);
                    if (day.ExtraWeekdayWork == 0)
                    {
                        NotPaid += Convert.ToDecimal(day.Hour2);
                    }
                    break;
                default:
                    NotPaid += Convert.ToDecimal(day.Hour2);
                    break;
            }
        }
    }
}
