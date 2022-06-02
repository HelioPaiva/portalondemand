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
    public partial class consulta_usuario : System.Web.UI.Page
    {
        Usuario usuario = new Usuario();
        string filtroUsuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idPerfil"].ToString() != "4")
            {
                Response.Redirect("default.aspx");
            }

            if (!Page.IsPostBack)
            {
                GetUsuarios();
                MonitorPainel();
            }
        }
        protected void GetUsuarios()
        {
            int cont = 0;
            DataSet listUsuarios = usuario.Buscar("usuario");

            foreach (DataRow row in listUsuarios.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListUsuario.DataSource = listUsuarios;
                DropDownListUsuario.DataTextField = "email";
                DropDownListUsuario.DataValueField = "email";
                DropDownListUsuario.DataBind();
                DropDownListUsuario.Items.Insert(0, new ListItem("Selecione Todos", "Selecione Todos"));
            }
        }

        protected void MonitorPainel()
        {
            DataSet listUsuarios;

            if (String.IsNullOrEmpty(filtroUsuario) || filtroUsuario.ToString() == "Selecione Todos")
            {
                listUsuarios = usuario.Buscar("usuario");
            }
            else
            {
                listUsuarios = usuario.BuscarUsuario(filtroUsuario.ToString());
            }
            

            int numcells = 4;

            foreach (DataRow row in listUsuarios.Tables[0].Rows)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numcells; i++)
                {
                    TableCell c = new TableCell();
                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl(row["email"].ToString()));
                    }
                    else if (i == 1)
                    {
                        c.Controls.Add(new LiteralControl(row["perfil"].ToString()));
                    }
                    else if (i == 2)
                    {
                        c.Controls.Add(new LiteralControl(row["dataCadastro"].ToString()));
                    }
                    else if (i == 3)
                    {
                        string idUsuarioBanco = row["id"].ToString();
                        string valor = "<a href='editar-usuario.aspx?idUsuarioValor=" + idUsuarioBanco + "'>Editar</a>";
                        c.Controls.Add(new LiteralControl(valor));
                    }
                    r.Cells.Add(c);
                }
                Table1.Rows.Add(r);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            filtroUsuario = DropDownListUsuario.SelectedValue.ToString();
            //int qtdSelecionado = 0;
            MonitorPainel();


        }
    }
}