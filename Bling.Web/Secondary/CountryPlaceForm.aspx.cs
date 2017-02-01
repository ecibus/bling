using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class CountryPlaceForm : BasePage, ICountryPlaceFormView
    {
        private CountryPlaceFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            //FileUpload1.FileName = @"H:\My Documents\091312.csv";
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new CountryPlaceFormPresenter(this);
            base.OnInit(e);
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

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public string SourceFileName
        {
            get { return Server.MapPath(@"~\Secondary\Data\CountryPlace.csv"); }
        }

        public string TargetPath
        {
            get { return Server.MapPath(@"~\Secondary\Report"); }
        }

        public string CountryPlaceData { set; get; }
    }
}