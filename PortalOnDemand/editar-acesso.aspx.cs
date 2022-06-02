using PortalOnDemand.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class editar_acesso : System.Web.UI.Page
    {
        string idAcessoValor;
        LoginStart login_start = new LoginStart();
        
        string usuarioRetornoBanco;
        string idPerfilRetornoBanco;
        string nomePerfilRetornoBanco;

        protected void Page_Load(object sender, EventArgs e)
        {
            idAcessoValor = Request.QueryString["idUsuarioValor"];

            if (Session["idPerfil"].ToString() != "4")
            {
                Response.Redirect("default.aspx");
            }
        }
    }
}