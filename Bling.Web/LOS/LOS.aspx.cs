using System;
using Bling.Domain.Extension;
using Bling.Presenter.LOS;
using log4net;

namespace Bling.Web.LOS
{
    public partial class LOS : BasePage, ILOSView
    {
        private LOSPresenter m_Presenter;
        protected string m_ResponseText;
                                                   
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "gethmdachangesbyloannumber":                        
                        m_Presenter.GetHMDAChangesByLoanNumber(Request["loannumber"].ToString());
                        break;

                    case "addhmdachanges":
                        AddHMDAChanges();
                        break;

                    case "deletehmdachanges":
                        m_Presenter.DeleteHMDAChanges(Request["idtodelete"].ToString().ToInteger());
                        break;

                    default:
                        break;

                }
                
            }
            catch (Exception ex)
            {
                m_ResponseText = ex.Message;
            }
        }               

        protected override void  OnInit(EventArgs e)
        {
            m_subTitle = "LOS Ajax Call";
            m_Presenter = new LOSPresenter(this);
            m_logger = LogManager.GetLogger(typeof(LOS));
         	base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

        private void AddHMDAChanges()
        {
            var newData = new global::Bling.Domain.LOS.HMDAChanges 
            { 
                LoanNumber = Request["loannumber"].ToString(), 
                ReportYear = Request["reportyear"].ToString(), 
                FieldName = Request["fieldname"].ToString(), 
                NewData = Request["newdata"].ToString(),
                GEMUser = CurrentUser
                //CreatedBy = CurrentUser.UserName 
            };

            m_Presenter.AddHMDAChanges(newData);
        }
    }
}
