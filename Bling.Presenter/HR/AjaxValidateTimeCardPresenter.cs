using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public class AjaxValidateTimeCardPresenter : Presenter
    {
        private IAjaxView m_View;
        private IValidateTimeCardDao m_Dao;

        public AjaxValidateTimeCardPresenter(IAjaxView view)
            : this(view, new ValidateTimeCardDao(GEMSql01Session()))
        {
        }

        public AjaxValidateTimeCardPresenter(IAjaxView view, IValidateTimeCardDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Validate(string start, string end)
        {
            var temp = "";
            try
            {
                var header = m_Dao.GetTimeCardHeader(start, end);
                var lineItem = m_Dao.GetTimeCardLineItem(start, end);
                var total = m_Dao.GetTimeCardTotal(start, end);

                var currentStart = Convert.ToDateTime(start);
                var yesterday = currentStart.AddDays(-1);

                var pStart = yesterday.Month.ToString() + (currentStart.Day == 1 ? "/16/" : "/1/") + yesterday.Year.ToString();
                var pEnd = yesterday.ToShortDateString();

                var pHeader = m_Dao.GetTimeCardHeader(pStart, pEnd);
                var pLineItem = m_Dao.GetTimeCardLineItem(pStart, pEnd);

                StringBuilder dailyTable = new StringBuilder();
                dailyTable.Append("<table border='1'>");
                dailyTable.Append(GetDayHeader());

                StringBuilder totalTable = new StringBuilder();
                totalTable.Append("<table border='1'>");
                totalTable.Append(GetTotalHeader());

                foreach (var h in header)
                {
                    temp = h.EmpId;
                    var dbCutoff = lineItem.Where(x => x.HeaderId == h.Id).OrderBy(x => x.WorkDate);
                    var dbTotal = total.FirstOrDefault(x => x.HeaderId == h.Id);

                    var dbPrevHeader = pHeader.FirstOrDefault(x => x.EmpId == h.EmpId);
                    List<TCLineItems> dbPrevCutoff = new List<TCLineItems>(); ;

                    if (dbPrevHeader != null)
                    {
                        dbPrevCutoff = pLineItem.Where(x => x.HeaderId == dbPrevHeader.Id).OrderBy(x => x.WorkDate).ToList();
                    }

                    foreach (var day in dbCutoff)
                    {
                        day.Calculate();
                        day.ShowNotEqual(dailyTable, h.EmpName);
                    }

                    var co = new TCCutoff();
                    co.BreakToWeeks(dbCutoff.ToList());

                    var pco = new TCCutoff();
                    pco.BreakToWeeks(dbPrevCutoff);

                    var compTotal = co.Calculate(pco.Weeks.Last());

                    compTotal.ShowNotEqual(totalTable, h.EmpName, h.EmpId, h.Submitted, dbTotal);
                }

                dailyTable.Append("</table>");
                totalTable.Append("</table>");
                m_View.ResponseText = dailyTable.ToString() + "<br/><br/>" + totalTable;
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message + " " + temp;
            }
        }

        private string GetDayHeader()
        {
            return "<thead>" +
                "<tr><td>Name</td><td>Date</td><td>Hour1</td><td>C Hour1</td><td>Hour2</td><td>C Hour2</td></tr>" +
                "</thead>";
        }

        private string GetTotalHeader()
        {
            return "<thead>" +
                "<tr><td>Name</td><td>Emp Id </td><td>Sub</td><td>Reg</td><td>C Reg</td><td>OT</td><td>C OT</td><td>DT</td><td>C DT</td><td>Net OT</td><td>C Net OT</td><td>Not Paid</td><td>C Not Paid</td>" +
                "<td>MakeUp</td><td>C MakeUp</td><td>Sick</td><td>C Sick</td><td>Vacation</td><td>C Vacation</td><td>Holiday</td><td>C Holiday</td><td>Bereave</td><td>C Bereave</td></tr>" +
                "</thead>";
        }

        public void Validate1(string start, string end)
        {
            var header = m_Dao.GetTimeCardHeader(start, end);
            var lineItem = m_Dao.GetTimeCardLineItem(start, end);
            var total = m_Dao.GetTimeCardTotal(start, end);
            var previousLineItem = m_Dao.GetTimeCardPreviousCutoff(start);

            StringBuilder table = new StringBuilder();
            table.Append("<table border='1'>");
            table.Append("<thead>");
            table.Append("<tr><td>Name</td><td>Date</td><td>Hour1</td><td>C Hour1</td><td>Hour2</td><td>C Hour2</td></tr>");
            table.Append("</thead>");
            table.Append("<tbody>");

            StringBuilder tableTotal = new StringBuilder();
            tableTotal.Append("<table border='1'>");

            tableTotal.Append("<thead>");
            tableTotal.Append("<tr><td>Name</td><td>Reg</td><td>C Reg</td><td>OT</td><td>C OT</td><td>DT</td><td>C DT</td><td>Net OT</td><td>C Net OT</td><td>Not Paid</td><td>C Not Paid</td>");
            tableTotal.Append("<td>MakeUp</td><td>C MakeUp</td><td>Sick</td><td>C Sick</td><td>Vacation</td><td>C Vacation</td><td>Holiday</td><td>C Holiday</td><td>Bereave</td><td>C Bereave</td></tr>");
            tableTotal.Append("</thead>");
            tableTotal.Append("<tbody>");

            foreach (var h in header)
            {
                var cutoff = lineItem.Where(x => x.HeaderId == h.Id).OrderBy(x => x.WorkDate);
                var t = total.FirstOrDefault(x => x.HeaderId == h.Id);
                var pCutoff = previousLineItem.Where(x => x.HeaderId == h.Id).OrderBy(x => x.WorkDate);

                var totalReg = 0.0m;
                var totalOT = 0.0m;
                var totalDT = 0.0m;
                var totalNetOT = 0.0m;
                var totalNotPaid = 0.0m;

                var totalExtraWeekendWork = 0.0m;
                var totalExtraWeekdayWork = 0.0m;
                var totalMissedHour = 0.0m;
                var totalNetMakeUp = 0.0m;

                var totalSick = 0.0m;
                var totalVacation = 0.0m;
                var totalHoliday = 0.0m;
                var totalBereave = 0.0m;

                var warn = "style='background:yellow'";

                foreach (var day in cutoff)
                {
                    day.Calculate();
                    totalReg += day.CalculatedTotal.Reg;
                    totalOT += day.CalculatedTotal.OT;
                    totalDT += day.CalculatedTotal.DT;

                    totalExtraWeekdayWork += day.ExtraWeekdayWork;
                    totalExtraWeekendWork += day.ExtraWeekendWork;
                    totalMissedHour += day.MissedHour;

                    switch (day.Reason)
                    {
                        case "S":
                            totalSick += Convert.ToDecimal(day.Hour2);
                            break;
                        case "V":
                            totalVacation += Convert.ToDecimal(day.Hour2);
                            break;
                        case "H":
                            totalHoliday += Convert.ToDecimal(day.Hour2);
                            break;
                        case "B":
                            totalBereave += Convert.ToDecimal(day.Hour2);
                            break;
                    }

                    if (!day.IsTheSame)
                    {
                        table.Append("<tr>");
                        table.AppendFormat("<td>{0}</td>", h.EmpName);
                        table.AppendFormat("<td>{0}</td>", Convert.ToDateTime(day.WorkDate).ToShortDateString());
                        table.AppendFormat("<td {1}>{0}</td>", day.Hour1, day.Hour1 == day.ComputedHours ? "" : warn);
                        table.AppendFormat("<td {1}>{0}</td>", day.ComputedHours, day.Hour1 == day.ComputedHours ? "" : warn);
                        table.AppendFormat("<td {1}>{0}</td>", day.Hour2, day.Hour2 == day.ComputedOther ? "" : warn);
                        table.AppendFormat("<td {1}>{0}</td>", day.ComputedOther, day.Hour2 == day.ComputedOther ? "" : warn);
                        table.Append("</tr>");
                    }
                }

                var extraWork = totalExtraWeekdayWork + totalExtraWeekendWork;
                if (extraWork >= totalMissedHour)
                {
                    totalNetMakeUp = totalMissedHour;
                    totalOT += extraWork - totalMissedHour;
                    totalNotPaid = 0.0m;
                }
                else
                {
                    totalNetMakeUp = extraWork;
                    totalNotPaid = totalMissedHour - totalNetMakeUp;
                }

                totalNetOT = totalOT;

                /////////////////////////////



                /////////////////////////////

                //if (t != null)
                if (totalReg != t.Reg || totalOT != t.OT || totalDT != t.DT || totalNetOT != t.NetOT || 
                    totalNotPaid != t.NotPaid || totalNetMakeUp != t.MakeUp ||
                    totalSick != t.Sick || totalVacation != t.Vacation ||
                    totalHoliday != t.Holiday || totalBereave != t.Bereave)
                {
                    tableTotal.Append("<tr>");
                    tableTotal.AppendFormat("<td>{0}</td>", h.EmpName);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.Reg, t.Reg == totalReg ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalReg, t.Reg == totalReg ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.OT, t.OT == totalOT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalOT, t.OT == totalOT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.DT, t.DT == totalDT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalDT, t.DT == totalDT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.NetOT, t.NetOT == totalNetOT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalNetOT, t.NetOT == totalNetOT ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.NotPaid, t.NotPaid == totalNotPaid ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalNotPaid, t.NotPaid == totalNotPaid ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.MakeUp, t.MakeUp == totalNetMakeUp ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalNetMakeUp, t.MakeUp == totalNetMakeUp ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.Sick, t.Sick == totalSick ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalSick, t.Sick == totalSick ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.Vacation, t.Vacation == totalVacation ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalVacation, t.Vacation == totalVacation ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.Holiday, t.Holiday == totalHoliday ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalHoliday, t.Holiday == totalHoliday ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", t.Bereave, t.Bereave == totalBereave ? "" : warn);
                    tableTotal.AppendFormat("<td {1}>{0}</td>", totalBereave, t.Bereave == totalBereave ? "" : warn);

                    tableTotal.Append("</tr>");

                }
            }
            table.Append("</tbody>");
            table.Append("</table>");

            tableTotal.Append("</tbody>");
            tableTotal.Append("</table>");

            m_View.ResponseText = "<br /><br />" + table.ToString() + "<br/><br/>" + tableTotal.ToString();
        }
    }
}
