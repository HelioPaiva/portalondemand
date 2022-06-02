using PortalOnDemand.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class cadastro_acesso : System.Web.UI.Page
    {
        Perfil perfil = new Perfil();
        Usuario usuario = new Usuario();
        LoginStart loginStart = new LoginStart();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionUserId = Session["usuario"] as string;

            if (String.IsNullOrEmpty(sessionUserId))
            {
                Response.Redirect("default.aspx");
            }

            if (Session["idPerfil"].ToString() != "4")
            {
                Response.Redirect("default.aspx");
            }

            mensagemOk.Visible = false;
            mensagemErro.Visible = false;
            mensagemErroConexao.Visible = false;
            mensagemErroPerfil.Visible = false;
            mensagemErroAcessoCadastrado.Visible = false;
            mensagemErroUsuario.Visible = false;

            if (!Page.IsPostBack)
            {
                GetPerfil();
            }
            if (!Page.IsPostBack)
            {
                GetUsuario();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string usuarioSelecionado = DropDownListUsuario.SelectedValue.ToString();
            string idPerfil = DropDownListPerfil.SelectedValue.ToString();

            if (idPerfil == "Selecione")
            {
                mensagemErroPerfil.Visible = true;
            }
            else if (usuarioSelecionado == "Selecione")
            {
                mensagemErroUsuario.Visible = true;
            }
            else
            {
                if (ValidaAcessoCadastrado(usuarioSelecionado, idPerfil) == false)
                {
                    usuario.idUser = Convert.ToInt16(usuarioSelecionado);
                    usuario.idProfileUser = Convert.ToInt16(idPerfil);
                    loginStart.Cadastro(usuario);
                    //mensagemOk.Visible = true;
                    Response.Redirect("consulta-acesso.aspx?status=ok");
                }
                else
                {
                    mensagemErroAcessoCadastrado.Visible = true;
                }

            }
        }
        protected void GetPerfil()
        {
            int cont = 0;
            DataSet listPerfil = perfil.Buscar("acesso");

            foreach (DataRow row in listPerfil.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListPerfil.DataSource = listPerfil;
                DropDownListPerfil.DataTextField = "perfil";
                DropDownListPerfil.DataValueField = "id";
                DropDownListPerfil.DataBind();
                DropDownListPerfil.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
            }
        }

        protected void GetUsuario()
        {
            int cont = 0;
            DataSet listUsuarios = usuario.Buscar("acesso");

            foreach (DataRow row in listUsuarios.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListUsuario.DataSource = listUsuarios;
                DropDownListUsuario.DataTextField = "email";
                DropDownListUsuario.DataValueField = "id";
                DropDownListUsuario.DataBind();
                DropDownListUsuario.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
            }
        }
        protected bool ValidaAcessoCadastrado(string _idUsuario, string _idPerfil)
        {
            int cont = 0;
            usuario.idUser = Convert.ToInt16(_idUsuario);
            usuario.idProfileUser = Convert.ToInt16(_idPerfil);
            DataSet listAcessos = loginStart.BuscarRegistro(usuario);

            foreach (DataRow row in listAcessos.Tables[0].Rows)
            {
                string idUsuario = row[2].ToString();
                string idPerfil = row[1].ToString();
                if (idUsuario == _idUsuario && idPerfil == _idPerfil)
                {
                    cont++;
                }
            }

            if (cont > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}