using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Compliance;
using Bling.Domain.Extension;

namespace Bling.Web.Compliance
{
    public partial class AjaxDIRW : BasePage, IAjaxView
    {
        private AjaxDIRWPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load(Request.Form["LoanNumber"].Trim());
                        break;

                    case "loadfield":                        
                        m_Presenter.LoadGroup(Server.HtmlDecode(Request.Form["fileid"].Trim()),
                            Request.Form["State"], Request.Form["ReviewType"]);
                        break;

                    case "savefield":
                        m_Presenter.SaveField(Server.HtmlDecode(Request.Form["fileid"].Trim()),
                            Request.Form["fieldid"].Trim(),
                            Request.Form["oldvalue"].Trim(),
                            Request.Form["newvalue"].Trim(),
                            Request.Form["elevated"].Trim(),
                            Request.Form["dropdownText"].Trim(),
                            CurrentUser.ActorId,
                            Server.HtmlDecode(Request.Form["keyid"].Trim())
                            );
                        break;

                    case "markasyes":
                        m_Presenter.MarkAsYes(Server.HtmlDecode(Request.Form["fileid"].Trim()),
                            Request.Form["fieldid"].Trim(),
                            Request.Form["oldvalue"].Trim(),
                            CurrentUser.ActorId,
                            Request.Form["keyid"].Trim()
                            );
                        break;

                    case "savereview":
                        m_Presenter.SaveReview(Server.HtmlDecode(Request.Form["fileid"].Trim()),
                            CurrentUser.ActorId,
                            Request.Form["notes"].Trim(), Request.Form["ReviewType"].Trim());
                        break;

                    case "loadfinal1003":
                        //m_Presenter.LoadFinal1003(Server.HtmlDecode(Request.Form["fileid"].Trim()));
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                ResponseText = ex.Message.Escape();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxDIRWPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { set; get; }
    }
}