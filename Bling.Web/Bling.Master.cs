using System;
using System.Web.UI;

namespace Bling.Web
{
    public partial class Bling : MasterPage
    {
        public string m_menu = "Home";
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string DisplayName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
