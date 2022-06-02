using Newtonsoft.Json;
using PortalOnDemand.banco;
using PortalOnDemand.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class monitor : System.Web.UI.Page
    {
        DataFactory df = new DataFactory();
        Log lg = new Log();
        string valor;
        DateTime lastWeek;
        string usuario;
        string idPipeline;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionUserId = Session["usuario"] as string;

            if (String.IsNullOrEmpty(sessionUserId))
            {
                Response.Redirect("default.aspx");
            }

            if (!Page.IsPostBack)
            {
                GetPipeline();
                MonitorPainel();
            }
        }
        protected void GetPipeline()
        {
            int cont = 0;
            Pipeline pipeline = new Pipeline();
            DataSet listPipeline = pipeline.Buscar();

            foreach (DataRow row in listPipeline.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListPipeline.DataSource = listPipeline;
                DropDownListPipeline.DataTextField = "pipeline";
                DropDownListPipeline.DataValueField = "pipeline";
                DropDownListPipeline.DataBind();
                DropDownListPipeline.Items.Insert(0, new ListItem("Selecione Todos", "Selecione Todos"));
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            MonitorPainel();
        }

        protected void Atualizar_Click(object sender, EventArgs e)
        {
            MonitorPainel();
        }
        protected void MonitorPainel()
        {

            List<string> listaPipeline = new List<string>();
            dynamic retornoPipeline = new Newtonsoft.Json.Linq.JObject();

            string periodoAnalisado = DropDownListPeriodo.SelectedValue.ToString();
            string filtroPipeline = DropDownListPipeline.SelectedValue.ToString();
            int qtdSelecionado = 0;

            for (int i = 0; i < DropDownListPipeline.Items.Count; i++)
            {
                if (DropDownListPipeline.Items[i].Selected)
                {
                    listaPipeline.Add(DropDownListPipeline.Items[i].ToString());
                    qtdSelecionado += 1;
                }
            }

            if (qtdSelecionado == 0 || filtroPipeline == "Selecione Todos")
            {
                for (int i = 0; i < DropDownListPipeline.Items.Count; i++)
                {
                    listaPipeline.Add(DropDownListPipeline.Items[i].ToString());
                }
            }

            //Periodo consultar
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Now;

            if (periodoAnalisado == "Ultimas 24 horas")
            {
                lastWeek = now.AddDays(-1);
                lblPeriodo.Text = "";
            }
            else if (periodoAnalisado == "Ultimos 7 dias")
            {
                lastWeek = now.AddDays(-7);
                lblPeriodo.Text = "";
            }

            lblInicioPeriodo.Text = lastWeek.ToString();
            LalblFimPeriodobel.Text = today.ToString();

            string detalheRetorno = df.MonitorDataFactory(listaPipeline, lastWeek, today);
            var listaNova = JsonConvert.DeserializeObject<ListaPipelines>(detalheRetorno);
            var listaOrdenada = listaNova.Detalhes.OrderByDescending(x => Convert.ToDateTime(x.PipelineInicio));
            DataSet listLog = lg.BuscarUsuario();



            int numcells = 8;

            foreach (var p in listaOrdenada)
            {
                TableRow r = new TableRow();

                for (int i = 0; i < numcells; i++)
                {
                    TableCell c = new TableCell();
                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl(p.PipelineName.ToString()));
                    }
                    else if (i == 1)
                    {
                        c.Controls.Add(new LiteralControl(p.PipelineInicio.ToString()));
                    }
                    else if (i == 2)
                    {
                        double s;
                        if (String.IsNullOrEmpty(p.PipelineDuracao))
                        {
                            s = 0;
                            var objeto = new DuracaoPipeline();

                            DateTime h = DateTime.Parse(p.PipelineInicio);
                            string start = h.ToString("yyyy-MM-dd HH:mm:ss");
                            DateTime entrada = DateTime.ParseExact(start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            objeto.HoraEntrada = entrada;
                            objeto.HoraSaida = DateTime.Now.AddHours(-3);
                            c.Controls.Add(new LiteralControl($"{objeto.TempoPermanencia():hh\\:mm\\:ss}"));
                        }
                        else
                        {
                            s = Convert.ToDouble(p.PipelineDuracao);
                            TimeSpan t = TimeSpan.FromMilliseconds(s);
                            string tempo = t.ToString(@"hh\:mm\:ss");
                            c.Controls.Add(new LiteralControl(tempo.ToString()));
                        }


                    }
                    else if (i == 3)
                    {
                        if (p.PipelineStatus.ToString() == "InProgress")
                        {
                            valor = "<div style='width: 200px;'><a href='' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-refresh'></span></a>&nbsp;&nbsp;&nbsp;Em processamento</div>";
                        }
                        else if (p.PipelineStatus.ToString() == "Succeeded")
                        {
                            valor = "<div style='width: 200px;'><a href='' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-ok'></span></a>&nbsp;&nbsp;&nbsp;Concluído</div>";
                        }
                        else if (p.PipelineStatus.ToString() == "Cancelled")
                        {
                            valor = "<div style='width: 200px;'><a href='' class='btn btn-warning btn-xs'><span class='glyphicon glyphicon-ban-circle'></span></a>&nbsp;&nbsp;&nbsp;Cancelado</div>";
                        }
                        else if (p.PipelineStatus.ToString() == "Queued")
                        {
                            valor = "<div style='width: 200px;'><a href='' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-time'></span></a>&nbsp;&nbsp;&nbsp;Em Fila</div>";
                        }
                        else if (p.PipelineStatus.ToString() == "Failed")
                        {
                            valor = "<div style='width: 200px;'><a href='' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></a>&nbsp;&nbsp;&nbsp;Falhou</div>";
                        }

                        c.Controls.Add(new LiteralControl(valor));
                    }
                    else if (i == 4)
                    {
                        string statusErro = p.PipelineStatus;
                        if (statusErro == "Failed")
                        {
                            string msgErroLog = p.PipelineErro.ToString().Replace(" ", "&nbsp;");
                            string valor = "<a href='#' data-toggle='modal' data-target='.bd-example-modal-lg'><span class='glyphicon glyphicon-comment' runat='server' title=" + msgErroLog + "></span> </a>";
                            c.Controls.Add(new LiteralControl(valor));
                        }
                        else
                        {
                            c.Controls.Add(new LiteralControl(""));
                        }
                    }
                    else if (i == 5)
                    {
                        c.Controls.Add(new LiteralControl(p.PipelineID.ToString()));
                    }
                    else if (i == 6)
                    {

                        if (p.PipelineTrigger == "Manual")
                        {
                            foreach (DataRow row in listLog.Tables[0].Rows)
                            {
                                if (p.PipelineID.ToString() == row[0].ToString())
                                {
                                    usuario = row[1].ToString().ToLower();
                                    break;
                                }
                            }

                            if (usuario == "" || String.IsNullOrEmpty(usuario))
                            {
                                c.Controls.Add(new LiteralControl("manual adf"));
                            }
                            else
                            {
                                c.Controls.Add(new LiteralControl(usuario.ToString().ToLower()));
                            }
                            usuario = null;
                        }
                        else
                        {
                            c.Controls.Add(new LiteralControl(p.PipelineTrigger.ToString().ToLower()));
                        }
                    }
                    r.Cells.Add(c);

                }

                Table1.Rows.Add(r);
            }
        }


    }
}