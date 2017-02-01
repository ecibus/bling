using System;
using log4net;

namespace Bling.Web.Accounting
{
    public partial class RemoveDocTrustAccountLogEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            m_logger = LogManager.GetLogger(typeof(RemoveDocTrustAccountLogEntry));
            base.OnInit(e);
        }    
    }
}
