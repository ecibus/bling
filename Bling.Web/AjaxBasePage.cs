using System;
using Bling.Presenter;

namespace Bling.Web
{
    public class AjaxBasePage : BasePage, IAjaxView
    {
        protected string m_ResponseText;

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }
    }
}
