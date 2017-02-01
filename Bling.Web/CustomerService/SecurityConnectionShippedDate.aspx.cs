using System;
using Bling.Presenter.CustomerService;
using Bling.Domain.CustomerService;
using System.Collections.Generic;
using log4net;

namespace Bling.Web.CustomerService
{
    public partial class SecurityConnectionShippedDate : BasePage, ISecurityConnectionView
    {
        protected string m_data;
        private SecurityConnectionPresenter m_presenter;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.FileName == String.Empty)
                {
                    ErrorMessage = "Please select a file to upload.";
                    return;
                }
                FileUpload1.SaveAs(SourceFileName);
                Session["SecurityConnection"] = m_presenter.LoadData();
                Literal1.Visible = true;
                btnUpdateDataTrac.Visible = true;
                m_logger.DebugFormat("Uploading {0}", FileUpload1.FileName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new SecurityConnectionPresenter(this);
            m_logger = LogManager.GetLogger(typeof(SecurityConnectionShippedDate));
            base.OnInit(e);
        }

        public string SourceFileName
        {
            get { return Server.MapPath(@"~\CustomerService\Data\Source.txt"); }
        }


        public string SecurityConnectionData
        {
            set { m_data = value; }
        }

        protected void btnUpdateDataTrac_Click(object sender, EventArgs e)
        {
            if (Session["SecurityConnection"] == null)
            {
                ErrorMessage = "Nothing to Upload.";
                return;
            }

            m_logger.DebugFormat("Updating DataTrac and Byte");

            List<SecurityConnectionShipDateInfo> list = Session["SecurityConnection"] as List<SecurityConnectionShipDateInfo>;

            m_presenter.SaveData(list);
            Session["SecurityConnection"] = null;

            InfoMessage = "Done updating DataTrac and Byte.";
            Literal1.Visible = false;
            btnUpdateDataTrac.Visible = false;
        }

    }
}
