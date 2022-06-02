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
    public partial class consulta_pipeline : System.Web.UI.Page
    {
        Pipeline pipe = new Pipeline();
        string filtroPipeline = null;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Session["idPerfil"].ToString() != "4")
            {
                Response.Redirect("default.aspx");
            }

            if (!Page.IsPostBack)
            {
                GetPipelines();
                MonitorPainel();
            }

            
        }

        protected void GetPipelines()
        {
            int cont = 0;
            DataSet listPipelines = pipe.Buscar();

            foreach (DataRow row in listPipelines.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListPipeline.DataSource = listPipelines;
                DropDownListPipeline.DataTextField = "pipeline";
                DropDownListPipeline.DataValueField = "pipeline";
                DropDownListPipeline.DataBind();
                DropDownListPipeline.Items.Insert(0, new ListItem("Selecione Todos", "Selecione Todos"));
            }
        }

        protected void MonitorPainel()
        {
            DataSet listPipelines;

            if (String.IsNullOrEmpty(filtroPipeline) || filtroPipeline.ToString() == "Selecione Todos")
            {
                listPipelines = pipe.Buscar();
            }
            else
            {
                listPipelines = pipe.BuscarPipeline(filtroPipeline.ToString());
            }

            //DataSet listPipelines = pipe.Buscar();

            int numcells = 6;

            foreach (DataRow row in listPipelines.Tables[0].Rows)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numcells; i++)
                {
                    TableCell c = new TableCell();
                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl(row["pipeline"].ToString()));
                    }
                    else if (i == 1)
                    {
                        c.Controls.Add(new LiteralControl(row["perfil"].ToString()));
                    }
                    else if (i == 2)
                    {
                        string na = row["Name"].ToString();
                        if (na == "")
                        {
                            c.Controls.Add(new LiteralControl("-"));
                        }
                        else
                        {
                            c.Controls.Add(new LiteralControl(row["Name"].ToString()));
                        }
                    }
                    else if (i == 3)
                    {
                        c.Controls.Add(new LiteralControl(row["execucaoManual"].ToString()));
                    }
                    else if (i == 4)
                    {
                        c.Controls.Add(new LiteralControl(row["dataCadastro"].ToString()));
                    }
                    else if (i == 5)
                    {
                        string idPipelineBanco = row["id"].ToString();
                        string valor = "<a href='editar-pipeline.aspx?idPipelineValor=" + idPipelineBanco + "'>Editar</a>";
                        c.Controls.Add(new LiteralControl(valor));
                    }
                    r.Cells.Add(c);
                }
                Table1.Rows.Add(r);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            filtroPipeline = DropDownListPipeline.SelectedValue.ToString();
            //int qtdSelecionado = 0;
            MonitorPainel();
        }
    }
}