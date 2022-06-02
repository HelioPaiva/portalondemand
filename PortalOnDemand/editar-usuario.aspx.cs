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
    public partial class editar_usuario : System.Web.UI.Page
    {
        string idUsuarioValor;
        Usuario usuario = new Usuario();
        string usuarioRetornoBanco;
        string idPerfilRetornoBanco;
        string nomePerfilRetornoBanco;

        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuarioValor = Request.QueryString["idUsuarioValor"];

            if (Session["idPerfil"].ToString() != "4")
            {
                Response.Redirect("default.aspx");
            }

            mensagemOk.Visible = false;
            mensagemErro.Visible = false;
            mensagemErroConexao.Visible = false;
            mensagemErroEmail.Visible = false;
            mensagemErroPerfil.Visible = false;

            if (!Page.IsPostBack)
            {
                PreencheDados();
            }

        }
        protected void PreencheDados()
        {

            DataSet listValorUsuarios = usuario.BuscarRegistroEmail(idUsuarioValor);

            foreach (DataRow row in listValorUsuarios.Tables[0].Rows)
            {
                usuarioRetornoBanco = row[0].ToString();
                idPerfilRetornoBanco = row[1].ToString();
                nomePerfilRetornoBanco = row[2].ToString();
            }

            //Preenche nome do usuario
            txtEmail.Text = usuarioRetornoBanco;

            //Preenche combo perfil com retorno do banco
            int cont = 0;
            Perfil perfil = new Perfil();
            DataSet listPerfil = perfil.Buscar("usuario");

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
                DropDownListPerfil.Items.Insert(0, new ListItem(nomePerfilRetornoBanco, idPerfilRetornoBanco));
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string validaEmailNestle = "@br.nestle.com";
            string validaEmailGaroto = "@garoto.com.br";
            string p = DropDownListPerfil.SelectedValue.ToString();

            if (!email.Contains(validaEmailNestle) && !email.Contains(validaEmailGaroto))
            {
                mensagemErroEmail.Visible = true;
            }
            else if (email == "")
            {
                mensagemErroEmail.Visible = true;
            }
            else if (p == "Selecione")
            {
                mensagemErroPerfil.Visible = true;
            }
            else
            {
                //if (ValidaUsuarioCadastrado(email) == false)
                //{
                    usuario.nameUser = email;
                    usuario.idProfileUser = Convert.ToInt16(p);
                    usuario.email = email;
                    usuario.Editar(idUsuarioValor);
                    Response.Redirect("consulta-usuario.aspx?status=ok");
                    //mensagemOk.Visible = true;
                //}
                //else
                //{
                //    mensagemErroUsuarioCadastrado.Visible = true;
                //}
            }
        }

        protected bool ValidaUsuarioCadastrado(string _email)
        {
            int cont = 0;
            usuario.nameUser = _email;
            DataSet listUsuarios = usuario.Buscar("usuario");

            foreach (DataRow row in listUsuarios.Tables[0].Rows)
            {
                string _user = row[1].ToString();
                if (_user == _email)
                {
                    cont++;
                    break;
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