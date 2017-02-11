using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.RestApi.TurningPoint;

namespace Bling.Web.RestApi
{
    public partial class AjaxTurningPoint : Page, IAjaxView
    {
        private TurningPointPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            //Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:3000/");
            Response.AppendHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS,PUT,DELETE");
            Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type,Accept,Authorization");
            //Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            Response.AppendHeader("Content-Type", "application/json");
            Response.AppendHeader("Accept", "application/json");

            if (Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 200;
                var httpApplication = sender as HttpApplication;
                httpApplication.CompleteRequest();
            }

            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "searchuser":
                        m_Presenter.SearchUser(Request["crit"].ToString());
                        break;

                    case "getturningpointusers":
                        m_Presenter.GetTurningPointUser();
                        break;


                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new TurningPointPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }

    }
}