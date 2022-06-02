using PortalOnDemand.model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class consulta_log : System.Web.UI.Page
    {
        //controle
        //controle
        Pipeline pipeline = new Pipeline();
        Usuario usuario = new Usuario();
        Log log = new Log();
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
                DropDownListUsuario.Items.Insert(0, new ListItem("Selecione Usuário", "Selecione Usuário"));
            }
        }

        protected void MonitorPainel()
        {
            DataSet ds;

            if (String.IsNullOrEmpty(filtroUsuario) || filtroUsuario.ToString() == "Selecione Usuário")
            {
                ds = log.Buscar();
            }
            else
            {
                ds = log.BuscarUsuarioLog(filtroUsuario.ToString());
            }

            int numcells = 5;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numcells; i++)
                {
                    TableCell c = new TableCell();
                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl(row["usuario"].ToString()));
                    }
                    else if (i == 1)
                    {
                        c.Controls.Add(new LiteralControl(row["funcao"].ToString()));
                    }
                    else if (i == 2)
                    {
                        c.Controls.Add(new LiteralControl(row["nome"].ToString().ToLower()));
                    }
                    else if (i == 3)
                    {
                        c.Controls.Add(new LiteralControl(row["dataExecucao"].ToString()));
                    }
                    else if (i == 4)
                    {
                        c.Controls.Add(new LiteralControl(row["idPipeline"].ToString()));
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
