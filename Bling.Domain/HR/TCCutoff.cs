using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class TCCutoff : TCTotal
    {
        public List<TCWeek> Weeks { get; set; }

        public TCCutoff()
        {
            Weeks = new List<TCWeek>();
        }

        public TCTotal Calculate(TCWeek lastWeek)
        {
            var total = new TCTotal();

            lastWeek.Summarize(0.0m, false);

            var firstWeek = true;
            int weekCounter = 0;
            foreach (var week in Weeks)
            {
                weekCounter++;
                week.Summarize(firstWeek ? lastWeek.WeekHours : 0.0m, weekCounter == Weeks.Count);
                total += week;
                firstWeek = false;
            }

            return total;
        }

        public void BreakToWeeks(List<TCLineItems> cutoff)
        {
            var week = GetWeek();

            foreach (var day in cutoff)
            {
                if (day.WorkDay == "Sun")
                {
                    week = GetWeek();
                }

                week.AddDay(day);
            }
        }

        private TCWeek GetWeek()
        {
            var week = new TCWeek();
            Weeks.Add(week);
            return week;

        }

    }
}
