using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace Bling.Web.HR
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //LoadReports();
                SetDBLogonForReport();
                //setupReport();
            }

        }

        private void LoadReports()
        {
            //string[] reports = Directory.GetFiles(Server.MapPath("Report"), "*.rpt");
            //Dictionary<string, string> rpts = new Dictionary<string, string>();
            //Dictionary<string, string> rptFiles = getReports();


            //for (int i = 0; i < reports.Length; i++)
            //{
            //    string k = Path.GetFileNameWithoutExtension(reports[i]);
            //    rpts.Add(k, reports[i]);
            //}

            //foreach (KeyValuePair<string, string> pair in rptFiles)
            //{
            //    if (rpts.Keys.Contains(pair.Key.ToString()))
            //    {
            //        string[] titles;
            //        titles = rptFiles[pair.Key.ToString()].Split(new char[] { ',' });
            //        ListItem item = new ListItem(titles[0] + " / " + titles[1], rpts[pair.Key.ToString()]);
            //        ddlReports.Items.Add(item);
            //    }
            //}
        }

        //protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetDBLogonForReport();
        //    CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        //    rpt.FileName = ddlReports.SelectedValue;
        //    crsOne.Report = rpt;
        //    crvOne.RefreshReport();
        //}

        private void SetDBLogonForReport()
        {
            crvOne.LogOnInfo.Clear();

            TableLogOnInfo t = new TableLogOnInfo();

            t.ConnectionInfo.ServerName = Properties.Settings.Default.dbServer;
            t.ConnectionInfo.DatabaseName = Properties.Settings.Default.dbName;
            t.ConnectionInfo.UserID = Properties.Settings.Default.dbUser;
            t.ConnectionInfo.Password = Properties.Settings.Default.dbPass;

            crvOne.LogOnInfo.Add(t);
        }

        private Dictionary<string,string> getReports()
        {
            Dictionary<string,string> list = new Dictionary<string,string>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dmddata"].ConnectionString))
            {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT rpt_id, file_name, title_one, title_two FROM xGEM_HR_Reports ORDER BY rpt_id";

                    SqlDataReader dr;

                    dr = cmd.ExecuteReader();
                    string temp = "";

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            temp = dr[2].ToString() + "," + dr[3].ToString();
                            list.Add(dr[1].ToString(), temp);
                        }
                    }

                    dr.Close();
                }

                return list;
            }
        }

        private void setupReport()
        {
            //CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
            //string path = Server.MapPath("Report/BranchBPHistory2.rpt");
            //rpt.FileName = path;
            //crsOne.Report = rpt;
            //crvOne.RefreshReport();
        }
    }

}