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
    /// Summary description for create_csv
    /// </summary>
    public class create_csv : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //Step 1 pull the parameters
            NameValueCollection nvc = context.Request.Params;
            string headers = "Plan ID, Pay Date, Type, SSN, Def Amount, Last Name, First Name";
            string today = DateTime.Now.ToShortDateString();
            double total = 0.00;
            string planID = "32489";
            string fileName = "GEM_" + planID + "_deferral_" + today.Replace('/', '_');
            string line = "";
            //Step 2 Create the file
            //Once the fields are created in solomon I will be calling a stored procedure to do this.
            //string npath = AppDomain.CurrentDomain.BaseDirectory + @"handlers\output\";
            //string path = npath + fileName + ".csv";
            string npath = @"\\devsrv\d$\Application\Bling\HR\handlers\output\" + fileName + ".csv";
            //StreamWriter sw = null;
            //try
            //{
            //    if (!File.Exists(fileName))
            //    {
            //        File.Copy(npath, npath);
            //    }
            //    sw = new StreamWriter(npath);
            //}
            //catch (Exception ex)
            //{
            //    context.Response.Write("<label class='ui-state-error' style='font-size:large'>An Error has occured creating the file please contact IT</label>");
            //    return;
            //}
            StreamWriter sw = new StreamWriter(npath);

            sw.WriteLine(headers);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dmddata"].ToString()))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    SqlDataReader dr;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_MTBG";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@start", nvc["start"]);
                    cmd.Parameters.AddWithValue("@end", nvc["end"]);
                    cmd.Parameters.AddWithValue("@getBy", nvc["selectBy"]);

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            line = dr[0] + "," + dr[1] + "," + dr[2] + "," + dr[3] + "," + dr[4] + "," + dr[5] + "," + dr[6];
                            total += double.Parse(dr[4].ToString());
                            sw.WriteLine(line);
                        }

                        line = ",,,," + total.ToString() + ",,";
                        sw.WriteLine(line);
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

                    dr.Close();
                }
            }
           
            //Step 3 Output a link to the file as the response.
            context.Response.Write("<a href='" + npath + "' class='ui-widget'>File Ready (Click me to save this file to your computer.)</a>");
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