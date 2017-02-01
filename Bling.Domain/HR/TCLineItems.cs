using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class TCLineItems
    {
        public virtual int Id { get; set; }
        public virtual int HeaderId { get; set; }
        public virtual DateTime WorkDate { get; set; }
        public virtual string WorkDay { get; set; }
        public virtual string In1 { get; set; }
        public virtual string In2 { get; set; }
        public virtual string In3 { get; set; }
        public virtual string Out1 { get; set; }
        public virtual string Out2 { get; set; }
        public virtual string Out3 { get; set; }
        public virtual string Reason { get; set; }
        public virtual string Hour1 { get; set; }
        public virtual string Hour2 { get; set; }
        public virtual string Comment { get; set; }

        public virtual bool IsTheSame { get; set; }
        public virtual string ComputedHours { get; set; }
        public virtual string ComputedOther { get; set; }
        public virtual TCTotal CalculatedTotal { get; set; }
        public virtual decimal NotPaid { get; set; }
        public virtual decimal ExtraWeekdayWork { get; set; }
        public virtual decimal ExtraWeekendWork { get; set; }
        public virtual decimal MissedHour { get; set; }

        public virtual void Calculate()
        {
            CalculatedTotal = new TCTotal();

            var min1 = GetDifferenceInMinutes(In1.Trim(), Out1.Trim());
            var min2 = GetDifferenceInMinutes(In2.Trim(), Out2.Trim());
            var min3 = GetDifferenceInMinutes(In3.Trim(), Out3.Trim());

            var totalMinutes = min1 + min2 + min3;

            var hours = ((totalMinutes % 15m) < 8m ? (totalMinutes - (totalMinutes % 15m)) : (totalMinutes - (totalMinutes % 15m) + 15m)) / 60m;
            var reg = hours > 8 ? "8.00" : String.Format("{0:0.00}", hours);
            var dbl = hours > 12 ? String.Format("{0:0.00}", hours - 12) : "0.00";
            var ovt = hours > 8 ? String.Format("{0:0.00}", hours - 8m - Convert.ToDecimal(dbl)) : "0.00";
            var other = 8m - Convert.ToDecimal(reg);
            ExtraWeekendWork = 0.00m;
            ExtraWeekdayWork = 0.00m;
            MissedHour = 0.00m;

            if (WorkDay == "Sat" || WorkDay == "Sun") 
            {
                other = 0.00m;
                MissedHour = 0.00m;
                ExtraWeekendWork = Convert.ToDecimal(hours);
                CalculatedTotal.Weekend = Convert.ToDecimal(hours);

                reg = "0.00";
                ovt = "0.00";
            } 
            else 
            {
                if (Reason == "N" || Reason == "M")
                {
                    MissedHour = 8 - Convert.ToDecimal(reg);
                    other = MissedHour;
                }
                if (Reason == "M")
                {
                    ExtraWeekdayWork = Convert.ToDecimal(ovt);
                    ovt = "0.00";
                }

            }

            NotPaid = (Reason == "N" || Reason == "M") ? other : 0.00m;
            ComputedHours = String.Format("{0:0.00}", hours);
            ComputedOther = String.Format("{0:0.00}", other);
            IsTheSame = ComputedHours == Hour1 && ComputedOther == Hour2;

            CalculatedTotal.Reg = Convert.ToDecimal(reg);
            CalculatedTotal.OT = Convert.ToDecimal(ovt);
            CalculatedTotal.DT = Convert.ToDecimal(dbl);
        }

        public virtual void Calculate1()
        {
            CalculatedTotal = new TCTotal();

            var min1 = GetDifferenceInMinutes(In1, Out1);
            var min2 = GetDifferenceInMinutes(In2, Out2);
            var min3 = GetDifferenceInMinutes(In3, Out3);

            var totalMinutes = min1 + min2 + min3;

            var hours = ((totalMinutes % 15m) < 8m ? (totalMinutes - (totalMinutes % 15m)) : (totalMinutes - (totalMinutes % 15m) + 15m)) / 60m;
            var reg = hours > 8 ? "8.00" : String.Format("{0:0.00}", hours);
            var dbl = hours > 12 ? String.Format("{0:0.00}", hours - 12) : "0.00";
            var ovt = hours > 8 ? String.Format("{0:0.00}", hours - 8m - Convert.ToDecimal(dbl)) : "0.00";
            var other = 8m - Convert.ToDecimal(reg);
            ExtraWeekendWork = 0.00m;
            ExtraWeekdayWork = 0.00m;
            MissedHour = 0.00m;

            if (WorkDay == "Sat" || WorkDay == "Sun") 
            {
                other = 0.00m;
                MissedHour = 0.00m;
                ExtraWeekendWork = Convert.ToDecimal(hours);
                CalculatedTotal.Weekend = Convert.ToDecimal(hours);

                reg = "0.00";
                ovt = "0.00";
            } 
            else 
            {
                if (Reason == "N" || Reason == "M")
                {
                    MissedHour = 8 - Convert.ToDecimal(reg);
                    other = MissedHour;
                }
                if (Reason == "M")
                {
                    ExtraWeekdayWork = Convert.ToDecimal(ovt);
                    ovt = "0.00";
                }

            }

            NotPaid = (Reason == "N" || Reason == "M") ? other : 0.00m;
            ComputedHours = String.Format("{0:0.00}", hours);
            ComputedOther = String.Format("{0:0.00}", other);
            IsTheSame = ComputedHours == Hour1 && ComputedOther == Hour2;

            CalculatedTotal.Reg = Convert.ToDecimal(reg);
            CalculatedTotal.OT = Convert.ToDecimal(ovt);
            CalculatedTotal.DT = Convert.ToDecimal(dbl);
        }

        public virtual void ShowNotEqual(StringBuilder sb, string empName)
        {
            var warn = "style='background:yellow'";

            if (!(ComputedHours == Hour1 && ComputedOther == Hour2))
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", empName);
                sb.AppendFormat("<td>{0}</td>", Convert.ToDateTime(WorkDate).ToShortDateString());
                sb.AppendFormat("<td {1}>{0}</td>", Hour1, Hour1 == ComputedHours ? "" : warn);
                sb.AppendFormat("<td {1}>{0}</td>", ComputedHours, Hour1 == ComputedHours ? "" : warn);
                sb.AppendFormat("<td {1}>{0}</td>", Hour2, Hour2 == ComputedOther ? "" : warn);
                sb.AppendFormat("<td {1}>{0}</td>", ComputedOther, Hour2 == ComputedOther ? "" : warn);
                sb.Append("</tr>");
            }
        }

        private int GetDifferenceInMinutes(string timeIn, string timeOut)
        {
            if (timeIn == "" || timeOut == "" || timeIn == "0" || timeOut == "0")
            {
                return 0;
            }

            return ToMinutes(timeOut) - ToMinutes(timeIn);
        }

        private int ToMinutes(string t)
        {
            if (t == "")
            {
                return 0;
            }

            var pos = t.IndexOf(":");
            var hour = Convert.ToInt32(t.Substring(0, pos));
            var minute = Convert.ToInt32(t.Substring(pos + 1, 2));

            if ((hour < 12) && (t.ToLower().IndexOf("pm") > 0)) {
                hour += 12;
            }

            if ((hour == 12) && (t.ToLower().IndexOf("am") > 0)) {
                hour = 0;
            }

            return (hour * 60) + minute;
        }
    }
}
