using System;
using Bling.Presenter;
using Bling.Presenter.IT;
using Bling.Domain.Extension;
using Bling.Domain;

namespace Bling.Web.IT
{
    public partial class AjaxInventory : BasePage, IAjaxView
    {
        protected string m_ResponseText;
        private AjaxInventoryPresenter m_Presenter;

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxInventoryPresenter(this);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "add":                        
                        AddInventory();
                        break;

                    case "display":
                        m_Presenter.GetAllInventoryWithPage(Request["page"].ToInteger());
                        break;

                    case "getusers":
                        m_Presenter.GetDistinctAssignTo();
                        break;

                    case "getbranches":
                        m_Presenter.GetDistinctBranches();
                        break;

                    case "displayfiltereddata":
                        m_Presenter.GetFilteredData(Request["page"].ToInteger(), Request["assignto"], Request["branch"]);
                        break;

                    case "delete":
                        m_Presenter.DeleteInventory(Request["idtodelete"].ToInteger());
                        break;

                    case "search":
                        m_Presenter.Search(Request["page"].ToInteger(), Request["searchstring"]);
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

        private void AddInventory()
        {
            global::Bling.Domain.IT.Inventory inventory = BuildInventory();

            string message = inventory.Validate();
            if (message != String.Empty)
            {
                m_ResponseText = message;
                return;
            }

            m_Presenter.Add(inventory);
        }

        private global::Bling.Domain.IT.Inventory BuildInventory()
        {
            global::Bling.Domain.IT.Inventory inventory = new global::Bling.Domain.IT.Inventory();
            inventory.IssuedOn = Request["issuedon"].ToDateTime();
            inventory.AddedBy = CurrentUser.UserName;
            inventory.Make = Request["make"];
            inventory.Model = Request["model"];
            inventory.SerialNumber = Request["serialnumber"];
            inventory.Quantity = Request["quantity"].ToInteger();
            inventory.IssuedTo = Request["issuedto"] ;
            inventory.BranchName = Request["branchname"];
            inventory.AddedOn = DateTime.Now;
            return inventory;
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }
    }
}
