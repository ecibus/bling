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
using System.Text;
using System.Web;

namespace Bling.Web.handlers
{
    /// <summary>
    /// Summary description for greentree
    /// </summary>
    public class greentree : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //Step 1 pull the parameters
            NameValueCollection nvc = context.Request.Params;
            string start = nvc["start"];
            string end = nvc["end"];
            string today = DateTime.Now.ToShortDateString();
            string fileName = "GREENTREE_XTRACT_" + today.Replace('/', '_');
            string line = "";

            //Step 2 create the file
            //Once the fields are created in solomon I will be calling a stored procedure to do this.
            string npath = AppDomain.CurrentDomain.BaseDirectory + @"handlers\output\";
             npath = npath + fileName + ".csv";
            //string npath = @"\\devsrv\d$\Application\Bling\HR\handlers\output\" + fileName + ".csv";
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(npath);
            }
            catch (Exception ex)
            {
                context.Response.Write("<label class='ui-state-error' style='font-size:large'>An Error has occured creating the file please contact IT</label>");
                return;
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dmddata"].ToString()))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    SqlDataReader dr;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GreenTree";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            line = "";
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                line += dr[i] + ",";
                            }
                            line = line.Substring(0, (line.Length - 1));
                            sw.WriteLine(line);
                        }
                        dr.Close();
                        sw.Flush();
                        sw.Close();
                    }
                    else
                    {
                        dr.Close();
                        sw.Flush();
                        sw.Close();
                    }
                }
            }

            context.Response.Write("<a href='" + npath + "' class='ui-widget'>File Ready (Right click and 'save target as' to save this file to your computer.)</a>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}