using PortalOnDemand.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionUserId = Session["usuario"] as string;

            if (String.IsNullOrEmpty(sessionUserId))
            {
                Response.Redirect("default.aspx");
            }

            Page.Title = "ADI - On Demand";

            if (LoginStart.idProfileUserLogin > 0)
            {
                //acessoLiberado.Visible = true;
            }
            else
            {
                //acessoLiberado.Visible = false;
            }

        }
    }
}