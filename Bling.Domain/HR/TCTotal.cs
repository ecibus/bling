using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.HR
{
    public class TCTotal
    {
        public virtual int Id { get; set; }
        public virtual int HeaderId { get; set; }
        public virtual decimal Reg { get; set; }
        public virtual decimal OT { get; set; }
        public virtual decimal DT { get; set; }
        public virtual decimal Sick { get; set; }
        public virtual decimal Vacation { get; set; }
        public virtual decimal Holiday { get; set; }
        public virtual decimal Bereave { get; set; }
        public virtual decimal NotPaid { get; set; }
        public virtual decimal MakeUp { get; set; }
        public virtual decimal NetOT { get; set; }
        public virtual decimal Weekend { get; set; }

        public static TCTotal operator +(TCTotal t1, TCTotal t2)
        {
            var t = new TCTotal();
            t.Reg = t1.Reg + t2.Reg;
            t.OT = t1.OT + t2.OT;
            t.DT = t1.DT + t2.DT;
            t.Sick = t1.Sick + t2.Sick;
            t.Vacation = t1.Vacation + t2.Vacation;
            t.Holiday = t1.Holiday + t2.Holiday;
            t.Bereave = t1.Bereave + t2.Bereave;
            t.NotPaid = t1.NotPaid + t2.NotPaid;
            t.MakeUp = t1.MakeUp + t2.MakeUp;
            t.NetOT = t1.NetOT + t2.NetOT;

            return t;
        }

        public virtual void ShowNotEqual(StringBuilder sb, string empName, string empId, string submitted, TCTotal t)
        {
            var warn = "style='background:yellow'";

            if (Reg != t.Reg || OT != t.OT || DT != t.DT || NetOT != t.NetOT || 
                    NotPaid != t.NotPaid || MakeUp != t.MakeUp ||
                    Sick != t.Sick || Vacation != t.Vacation ||
                    Holiday != t.Holiday || Bereave != t.Bereave)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", empName);
                    sb.AppendFormat("<td>{0}</td>", empId);
                    sb.AppendFormat("<td>{0}</td>", submitted);
                    sb.AppendFormat("<td {1}>{0}</td>", t.Reg, t.Reg == Reg ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", Reg, t.Reg == Reg ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.OT, t.OT == OT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", OT, t.OT == OT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.DT, t.DT == DT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", DT, t.DT == DT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.NetOT, t.NetOT == NetOT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", NetOT, t.NetOT == NetOT ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.NotPaid, t.NotPaid == NotPaid ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", NotPaid, t.NotPaid == NotPaid ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.MakeUp, t.MakeUp == MakeUp ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", MakeUp, t.MakeUp == MakeUp ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.Sick, t.Sick == Sick ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", Sick, t.Sick == Sick ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.Vacation, t.Vacation == Vacation ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", Vacation, t.Vacation == Vacation ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.Holiday, t.Holiday == Holiday ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", Holiday, t.Holiday == Holiday ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", t.Bereave, t.Bereave == Bereave ? "" : warn);
                    sb.AppendFormat("<td {1}>{0}</td>", Bereave, t.Bereave == Bereave ? "" : warn);

                    sb.Append("</tr>");

                }

        }
    }
}
