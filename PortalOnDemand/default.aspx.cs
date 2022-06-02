using PortalOnDemand.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            if (Request.IsAuthenticated)
            {
                LoginStart loginStart = new LoginStart();
                LoginStart.environmentLogin = ConfigurationManager.AppSettings["AppEnvironment"];
                LoginStart.nameUserLogin = User.Identity.Name.ToLower();
                loginStart.Buscar();
                Session["usuario"] = LoginStart.nameUserLogin;
                Session["idPerfil"] = LoginStart.idProfileUserLogin;   
            }
            else
            {
                LoginStart loginStart = new LoginStart();
                LoginStart.environmentLogin = ConfigurationManager.AppSettings["AppEnvironment"];
                //if (LoginStart.environmentLogin == null)
                //{
                //    LoginStart.environmentLogin = "dev";
                //}
                LoginStart.nameUserLogin = User.Identity.Name.ToLower();
                if (String.IsNullOrEmpty(LoginStart.nameUserLogin))
                {
                    LoginStart.nameUserLogin = "brserviceazure@br.nestle.com";
                }
                loginStart.Buscar();
                Session["usuario"] = LoginStart.nameUserLogin;
                Session["idPerfil"] = LoginStart.idProfileUserLogin;
            }

            if (LoginStart.idProfileUserLogin == 4 || LoginStart.idProfileUserLogin == 6)
            {
                acessoLiberado.Visible = true;
                mensagemAlertaAcesso.Visible = false;
            }
            else
            {
                acessoLiberado.Visible = false;
                mensagemAlertaAcesso.Visible = true;
            }
        }
    }
}