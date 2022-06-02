using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using PortalOnDemand.model;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;

namespace PortalOnDemand
{
    public partial class mobile3 : System.Web.UI.Page
    {
        DataFactory df = new DataFactory();
        DateTime lastWeek;
        string tempo;
        string imgStatusPipeline;

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
            DataSet listPipeline = pipeline.BuscarMobile();

            foreach (DataRow row in listPipeline.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListPipelines.DataSource = listPipeline;
                DropDownListPipelines.DataTextField = "pipeline";
                DropDownListPipelines.DataValueField = "pipeline";
                DropDownListPipelines.DataBind();
                DropDownListPipelines.Items.Insert(0, new ListItem("Selecione Todos", "Selecione Todos"));
            }
        }
        protected void Button2_Click2(object sender, EventArgs e)
        {
            MonitorPainel();
        }

        protected void Atualizar_Click1(object sender, EventArgs e)
        {
            MonitorPainel();
        }

        protected void MonitorPainel()
        {
            /*****************************Filtro do período consultado*************************/
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Now;
            string periodoAnalisado = DropDownListPeriodo.SelectedValue.ToString();
            if (periodoAnalisado == "ultimas 24 horas")
            {
                lastWeek = now.AddDays(-1);
                lblPeriodo.Text = "";
            }
            else if (periodoAnalisado == "ultimos 7 dias")
            {
                lastWeek = now.AddDays(-7);
                lblPeriodo.Text = "";
            }
            
            //lblInicioPeriodo.Text = lastWeek.ToString();
            //LalblFimPeriodobel.Text = today.ToString();

            /*****************************Filtro pipelines consultado*************************/
            List<string> listaPipeline = new List<string>();
            dynamic retornoPipeline = new Newtonsoft.Json.Linq.JObject();

            string filtroPipeline = DropDownListPipelines.SelectedValue.ToString();
            int qtdSelecionado = 0;

            for (int i = 0; i < DropDownListPipelines.Items.Count; i++)
            {
                if (DropDownListPipelines.Items[i].Selected)
                {
                    listaPipeline.Add(DropDownListPipelines.Items[i].ToString());
                    qtdSelecionado += 1;
                }
            }

            if (qtdSelecionado == 0 || filtroPipeline == "Selecione Todos")
            {
                for (int i = 0; i < DropDownListPipelines.Items.Count; i++)
                {
                    listaPipeline.Add(DropDownListPipelines.Items[i].ToString());
                }
            }

            string detalheRetorno = df.MonitorDataFactory(listaPipeline, lastWeek, today);
            var listaNova = JsonConvert.DeserializeObject<ListaPipelines>(detalheRetorno);
            var listaOrdenada = listaNova.Detalhes.OrderByDescending(x => Convert.ToDateTime(x.PipelineInicio));
            //DataSet listLog = lg.BuscarUsuario();

            StringBuilder sb = new StringBuilder();
            double duracao;

            foreach (var p in listaOrdenada)
            {
                //tratamento status pipeline
                if (p.PipelineStatus.ToString() == "Queued")
                {
                    imgStatusPipeline = "<img src='mobile/img/icon-fila.svg' title='fila' />";
                }
                else if (p.PipelineStatus.ToString() == "Succeeded")
                {
                    imgStatusPipeline = "<img src='mobile/img/icon-success.svg' title='Succeeded' />";
                }
                else if (p.PipelineStatus.ToString() == "InProgress")
                {
                    imgStatusPipeline = "<img src='mobile/img/icon-progress.svg' title='In progress' />";
                }
                else if (p.PipelineStatus.ToString() == "Cancelled")
                {
                    imgStatusPipeline = "<img src='mobile/img/icon-cancel.svg' title='Cancelled' />";
                }
                else if (p.PipelineStatus.ToString() == "Failed")
                {
                    imgStatusPipeline = "<img src='mobile/img/icon-error.svg' title='Failed' />";
                }

                //tratamento inicio pipeline
                DateTime hinicio = DateTime.Parse(p.PipelineInicio);
                string inicio = hinicio.ToString("dd/MM/yy HH:mm");

                //tratamento duração pipeline          
                if (String.IsNullOrEmpty(p.PipelineDuracao))
                {
                    duracao = 0;
                    var objeto = new DuracaoPipeline();
                    DateTime h = DateTime.Parse(p.PipelineInicio);
                    string start = h.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime entrada = DateTime.ParseExact(start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    objeto.HoraEntrada = entrada;
                    objeto.HoraSaida = DateTime.Now.AddHours(-3);
                    //tempo = "00:00:00";
                    tempo = $"{objeto.TempoPermanencia():hh\\:mm\\:ss}";
                }
                else
                {
                    duracao = Convert.ToDouble(p.PipelineDuracao);
                    TimeSpan t = TimeSpan.FromMilliseconds(duracao);
                    tempo = t.ToString(@"hh\:mm\:ss");
                }

                sb.AppendFormat("<div class='my-3 p-3 bg-white rounded box-shadow'>" +
                                "<div class='row text-lowercase'>" +
                                "<div class='col-md-2'>status:&nbsp;&nbsp;"+ imgStatusPipeline.ToString() + "</div>" +
                                "<div class='col-md-4'>name:&nbsp;&nbsp;" + p.PipelineName.ToString() + "</div>" +
                                "<div class='col-md-3'>início:&nbsp;&nbsp;" + inicio.ToString() + "</div>" +
                                "<div class='col-md-3'>duração:&nbsp;&nbsp;" + tempo.ToString() + "</div>" +
                                "</div>" +
                                "</div>");
            }
            div_pipeline.Controls.Add(new LiteralControl(sb.ToString()));

            /*
            foreach (var p in listaOrdenada)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(p.PipelineName.ToString());
                div_pipeline.Controls.Add(new LiteralControl(sb.ToString()));
            }
            */
        }


    }
}