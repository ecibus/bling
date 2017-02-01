using System;
using System.IO;

namespace Bling.Web.Secondary
{
    public partial class ChangeProgramCode : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var sr = File.OpenText(@"C:\SourceCode\OBMassEmail\OBMassEmail\OBUser.csv"))
            {
                string l;
                //Mail mail = new Mail();
                while ((l = sr.ReadLine()) != null)
                {
                    Data data = new Data(l);
                    Console.WriteLine(data.Email);
                    //if (data.Email != "Email")
                    //    mail.SendEmail(data);
                }

            }
        }


        class Data
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

            public Data(string data)
            {
                string[] aData = data.Split(',');
                FirstName = aData[0];
                LastName = aData[1];
                UserName = aData[2];
                Password = aData[3];
                Email = aData[4];
            }
        }
    }
}
