using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Compliance;

namespace Bling.Web.Compliance
{
    public partial class MERSReconciliationForm : BasePage, IMERSReconciliationFormView
    {
        private MERSReconciliationFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {

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

                m_Presenter.LoadData();
                
                //Session["SecurityConnection"] = m_presenter.LoadData();
                //Literal1.Visible = true;
                //btnUpdateDataTrac.Visible = true;
                //m_logger.DebugFormat("Uploading {0}", FileUpload1.FileName);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            m_Presenter = new MERSReconciliationFormPresenter(this);
            base.OnLoad(e);
        }

        public string SourceFileName
        {
            get { return Server.MapPath(@"~\Compliance\Data\MERSSource.txt"); }
        }

        public string MERSData { set; get; }
    }
}