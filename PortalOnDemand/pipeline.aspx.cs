using Newtonsoft.Json;
using PortalOnDemand.model;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PortalOnDemand
{
    public partial class pipeline : Page
    {
        DataFactory df = new DataFactory();
        Log lg = new Log();
        Pipeline pipe = new Pipeline();

        string idRunPipeline;
        string pipelineSelecionado;
        string msgErro;
        string retornoPipeline;

        string pipelineRetornoBanco;
        string idPerfilRetornoBanco;
        string idProjetoRetornoBanco;
        string nomeProjetoRetornoBanco;
        string nomePerfilRetornoBanco;
        string execucaoManualBanco;
        int codProjeto;
        string temParametros;
        int qtdParametro;
        string nome_parametro_1;
        string texto_ajuda_1;
        string nome_parametro_2;
        string texto_ajuda_2;
        string nome_parametro_3;
        string texto_ajuda_3;
        string nome_parametro_4;
        string texto_ajuda_4;
        string nome_parametro_5;
        string texto_ajuda_5;
        string strParametros;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionUserId = Session["usuario"] as string;

            if (String.IsNullOrEmpty(sessionUserId))
            {
                Response.Redirect("default.aspx");
            }

            mensagemOkPipeline.Visible = false;
            mensagemAlertaPipeline.Visible = false;
            mensagemErroListaPipeline.Visible = false;
            mensagemErroProcessamentoPipeline.Visible = false;
            mensagemErroConexao.Visible = false;
            statusEmProgresso.Visible = false;
            statusOK.Visible = false;
            statusErro.Visible = false;
            statusCancelado.Visible = false;
            statusErroMsg.Visible = false;
            statusEmFile.Visible = false;
            alertaInProgress.Visible = false;
            iconMsgErro.Visible = false;
            lblInicioExecucao.Visible = false;
            //dataVDE.Visible = false;
            PeridoVDE.Visible = false;

            alertaParametro.Visible = false;

            if (!Page.IsPostBack)
            {
                GetPipeline();
                painelMonitor.Visible = false;
                dataVDE.Visible = false;
                dataSelloutDataLab.Visible = false;

                lineParam0.Visible = false;
                lineParam1.Visible = false;
                lineParam2.Visible = false;
                lineParam3.Visible = false;
                lineParam4.Visible = false;
                lineParam5.Visible = false;
            }
        }


        protected void GetPipeline()
        {
            int cont = 0;
            Pipeline pipeline = new Pipeline();
            DataSet listPipeline = pipeline.BuscarPipelineExecucao();

            foreach (DataRow row in listPipeline.Tables[0].Rows)
            {
                retornoPipeline = row[0].ToString();
                cont++;
            }

            if (cont > 0 && !String.IsNullOrEmpty(retornoPipeline))
            {
                DropDownListPipeline.DataSource = listPipeline;
                DropDownListPipeline.DataTextField = "pipeline";
                DropDownListPipeline.DataValueField = "pipeline";
                DropDownListPipeline.DataBind();
                DropDownListPipeline.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
            }
            else
            {
                DropDownListPipeline.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
                mensagemAlertaPipeline.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            pipelineSelecionado = DropDownListPipeline.SelectedValue.ToString();

            string qtdParam1 = Param_1.Text;
            string qtdParam2 = Param_2.Text;
            string qtdParam3 = Param_3.Text;
            string qtdParam4 = Param_4.Text;
            string qtdParam5 = Param_5.Text;

            //Valida se algums pipeline foi selecionado
            if (pipelineSelecionado != "Selecione")
            {
                //Se o pipeline não estiver em execução executa.
                idRunPipeline = df.ValidacaoPipelineEmExecucaoPorName(pipelineSelecionado);

                if (idRunPipeline == null)
                {

                    //Verifica se pipeline tem parâmetros

                    if (verificaTemParametros() == true)
                    {
                        //criar array com paramtros
                        string[] nameParametrosADF = new string[qtdParametro];
                        string[] parametrosADF = new string[qtdParametro];

                        if (qtdParametro == 1)
                        {
                            if (String.IsNullOrEmpty(Param_1.Text))
                            {
                                alertaParametro.Visible = true;
                            }
                            else
                            {
                                nameParametrosADF[0] = namePara1.Text;
                                parametrosADF[0] = Param_1.Text;


                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineComParametros_1(pipelineSelecionado, nameParametrosADF[0], parametrosADF[0], true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }
                        }
                        else if (qtdParametro == 2)
                        {
                            if (String.IsNullOrEmpty(Param_1.Text) || String.IsNullOrEmpty(Param_2.Text))
                            {
                                alertaParametro.Visible = true;
                                parametrosADF[0] = Param_1.Text;
                            }
                            else
                            {
                                nameParametrosADF[0] = namePara1.Text;
                                nameParametrosADF[1] = namePara2.Text;

                                parametrosADF[0] = Param_1.Text;
                                parametrosADF[1] = Param_2.Text;

                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineComParametros_2(pipelineSelecionado, nameParametrosADF[0], nameParametrosADF[1], parametrosADF[0], parametrosADF[1], true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }
                        }
                        else if (qtdParametro == 3)
                        {
                            if (String.IsNullOrEmpty(Param_1.Text) || String.IsNullOrEmpty(Param_2.Text) || String.IsNullOrEmpty(Param_3.Text))
                            {
                                alertaParametro.Visible = true;
                            }
                            else
                            {
                                nameParametrosADF[0] = namePara1.Text;
                                nameParametrosADF[1] = namePara2.Text;
                                nameParametrosADF[2] = namePara3.Text;

                                parametrosADF[0] = Param_1.Text;
                                parametrosADF[1] = Param_2.Text;
                                parametrosADF[2] = Param_3.Text;

                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineComParametros_3(pipelineSelecionado, nameParametrosADF[0], nameParametrosADF[1], nameParametrosADF[2], parametrosADF[0], parametrosADF[1], parametrosADF[2], true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }
                        }
                        else if (qtdParametro == 4)
                        {
                            if (String.IsNullOrEmpty(Param_1.Text) || String.IsNullOrEmpty(Param_2.Text) || String.IsNullOrEmpty(Param_3.Text) || String.IsNullOrEmpty(Param_4.Text))
                            {
                                alertaParametro.Visible = true;
                            }
                            else
                            {
                                nameParametrosADF[0] = namePara1.Text;
                                nameParametrosADF[1] = namePara2.Text;
                                nameParametrosADF[2] = namePara3.Text;
                                nameParametrosADF[3] = namePara4.Text;

                                parametrosADF[0] = Param_1.Text;
                                parametrosADF[1] = Param_2.Text;
                                parametrosADF[2] = Param_3.Text;
                                parametrosADF[3] = Param_4.Text;

                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineComParametros_4(pipelineSelecionado, nameParametrosADF[0], nameParametrosADF[1], nameParametrosADF[2], nameParametrosADF[3], parametrosADF[0], parametrosADF[1], parametrosADF[2], parametrosADF[3], true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }
                        }
                        else if (qtdParametro == 5)
                        {
                            if (String.IsNullOrEmpty(Param_1.Text) || String.IsNullOrEmpty(Param_2.Text) || String.IsNullOrEmpty(Param_3.Text) || String.IsNullOrEmpty(Param_4.Text) || String.IsNullOrEmpty(Param_5.Text))
                            {
                                alertaParametro.Visible = true;
                            }
                            else
                            {
                               

                                nameParametrosADF[0] = namePara1.Text;
                                nameParametrosADF[1] = namePara2.Text;
                                nameParametrosADF[2] = namePara3.Text;
                                nameParametrosADF[3] = namePara4.Text;
                                nameParametrosADF[4] = namePara5.Text;

                                parametrosADF[0] = Param_1.Text;
                                parametrosADF[1] = Param_2.Text;
                                parametrosADF[2] = Param_3.Text;
                                parametrosADF[3] = Param_4.Text;
                                parametrosADF[4] = Param_5.Text;

                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineComParametros_5(pipelineSelecionado, nameParametrosADF[0], nameParametrosADF[1], nameParametrosADF[2], nameParametrosADF[3], nameParametrosADF[4], parametrosADF[0], parametrosADF[1], parametrosADF[2], parametrosADF[3], parametrosADF[4], true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }
                        }
                    }
                    else {


                        //Provisório para o VDE solicitado pelo André Santos
                        /*
                        if (pipelineSelecionado == "VDE_MESTRE" || pipelineSelecionado == "pl_master_sellout-b2c_online")
                        {

                        if (pipelineSelecionado == "VDE_MESTRE")
                        {
                            if (String.IsNullOrEmpty(txtData.Text))
                            {
                                PeridoVDE.Visible = true;
                            }
                            else
                            {
                                string periodo = txtData.Text;
                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineVDE(pipelineSelecionado, periodo, true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }

                        }

                        if (pipelineSelecionado == "pl_master_sellout-b2c_online")
                        {
                            if (String.IsNullOrEmpty(txtDataSellout.Text))
                            {
                                PeridoVDE.Visible = true;
                            }
                            else
                            {
                                string periodo = txtDataSellout.Text;
                                string environment = LoginStart.environmentLogin;
                                idRunPipeline = df.ExecutaPipelineVDE(pipelineSelecionado, periodo, true, environment);
                                string funcao = "pipeline";
                                string status = "ativo";
                                lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                                MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                                lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                                PainelPipeline(adf);
                            }

                        }
                    }
                    */
                        //else
                        //{
                        //Executa pipeline selecionado
                        idRunPipeline = df.ExecutaPipeline(pipelineSelecionado);
                        //Inseri log de execução
                        string funcao = "pipeline";
                        string status = "ativo";
                        lg.InsertLog(status, LoginStart.nameUserLogin, pipelineSelecionado, funcao, LoginStart.environmentLogin, idRunPipeline);
                        MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                        //lblInicioExecucao.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                        PainelPipeline(adf);
                    }
                }
                else
                {
                    MonitorADF adf = df.MonitorPipeline(idRunPipeline);
                    alertaInProgress.Visible = true;
                    lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
                    PainelPipeline(adf);
                }
            }
            else
            {
                mensagemErroListaPipeline.Visible = true;
            }
        }

        protected void Atualizar_Click(object sender, EventArgs e)
        {
            idRunPipeline = lblIdPipeline.Text;
            MonitorADF adf = df.MonitorPipeline(idRunPipeline);
            alertaInProgress.Visible = true;
            PainelPipeline(adf);
            Thread.Sleep(2000);
        }

        protected void PainelPipeline(MonitorADF adf)
        {

            lblPipeline.Text = adf.PipelineName;
            lblInicioExecucaoVisual.Text = adf.RunStart;
            lblDuracao.Text = adf.DurationInMs;
            lblIdPipeline.Text = adf.RunId;
            string statusPipeline = adf.Status;
            string startdate = lblInicioExecucao.Text;
            DateTime entrada = DateTime.ParseExact(startdate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            var objeto = new DuracaoPipeline();

            if (statusPipeline == "InProgress")
            {
                objeto.HoraEntrada = entrada;
                objeto.HoraSaida = DateTime.Now.AddHours(-3);
                lblDuracao.Text = $"{objeto.TempoPermanencia():hh\\:mm\\:ss}";

                statusEmProgresso.Visible = true;
            }
            else if (statusPipeline == "Succeeded")
            {
                objeto.HoraEntrada = entrada/*.AddHours(3)*/;
                DateTime fimExecucao = DateTime.Parse(adf.RunEnd);
                objeto.HoraSaida = fimExecucao.AddHours(-3);
                lblDuracao.Text = $"{(objeto.TempoPermanencia().ToString(@"hh\:mm\:ss"))}";

                statusEmProgresso.Visible = false;
                statusOK.Visible = true;
            }
            else if (statusPipeline == "Cancelled")
            {
                objeto.HoraEntrada = entrada/*.AddHours(3)*/;
                DateTime fimExecucao = DateTime.Parse(adf.RunEnd);
                objeto.HoraSaida = fimExecucao.AddHours(-3);
                lblDuracao.Text = $"{(objeto.TempoPermanencia().ToString(@"hh\:mm\:ss"))}";

                statusEmProgresso.Visible = false;
                statusCancelado.Visible = true;
            }
            else if (statusPipeline == "Queued")
            {
                statusEmProgresso.Visible = false;
                statusEmFile.Visible = true;
            }
            else if (statusPipeline == "Failed")
            {
                objeto.HoraEntrada = entrada/*.AddHours(3)*/;
                DateTime fimExecucao = DateTime.Parse(adf.RunEnd);
                objeto.HoraSaida = fimExecucao.AddHours(-3);
                lblDuracao.Text = $"{(objeto.TempoPermanencia().ToString(@"hh\:mm\:ss"))}";

                string erroPipeline = df.ErroExecucaoPipeline(adf.RunId);
                var erro = JsonConvert.DeserializeObject<Erro>(erroPipeline);
                msgErro = erro.ErrorCode.ToString() + "-" + erro.Message.Replace("'", "").ToString();
                idMsgErro.Text = msgErro.ToString();
                iconMsgErro.Visible = true;

                statusEmProgresso.Visible = false;
                statusErro.Visible = true;
            }
            lblStatus.Visible = false;
            painelMonitor.Visible = true;
        }

        protected void DropDownListPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            lineParam0.Visible = false;
            lineParam1.Visible = false;
            lineParam2.Visible = false;
            lineParam3.Visible = false;
            lineParam4.Visible = false;
            lineParam5.Visible = false;

            pipelineSelecionado = DropDownListPipeline.SelectedValue.ToString();

            if(verificaTemParametros() == true)
            {
                idTableParametro.Visible = true;
                PreencheDadosParametros();
            }

        

            if (pipelineSelecionado == "VDE_MESTRE")
            {
                dataVDE.Visible = true;
            }
            else
            {
                dataVDE.Visible = false;
            }


            if (pipelineSelecionado == "pl_master_sellout-b2c_online")
            {
                dataSelloutDataLab.Visible = true;
            }
            else
            {
                dataSelloutDataLab.Visible = false;
            }
        }

        protected bool verificaTemParametros()
        {
            DataSet listValorPipelines = pipe.BuscaParametrosDoPipeline(pipelineSelecionado);
            foreach (DataRow row in listValorPipelines.Tables[0].Rows)
            {
                temParametros = row[8].ToString();
                qtdParametro = Convert.ToInt32(row[9]);
                break;
            }

            if(temParametros == "Sim")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void PreencheDadosParametros()
        {

            DataSet listValorPipelines = pipe.BuscaParametrosDoPipeline(pipelineSelecionado);

            int contadorParametros = 1;
            foreach (DataRow row in listValorPipelines.Tables[0].Rows)
            {
                
                
                temParametros = row[8].ToString();
                qtdParametro = Convert.ToInt32(row[9]);

                if (temParametros == "Sim")
                {
                    qtdParametro = Convert.ToInt32(row[9]);

                    lineParam0.Visible = true;

                    if (contadorParametros == 1)
                    {
                        namePara1.Text = row[10].ToString();
                        ajudaPara1.Text = row[11].ToString();
                        lineParam1.Visible = true;


                        //parametro_1.Visible = true;

                        //nome_parametro_1 = row[10].ToString();
                        //texto_ajuda_1 = row[11].ToString();

                    }
                    else if (contadorParametros == 2)
                    {
                        namePara2.Text = row[10].ToString();
                        ajudaPara2.Text = row[11].ToString();
                        lineParam2.Visible = true;
                        //parametro_2.Visible = true;

                        //nome_parametro_2 = row[10].ToString();
                        //texto_ajuda_2 = row[11].ToString();
                    }
                    else if (contadorParametros == 3)
                    {
                        namePara3.Text = row[10].ToString();
                        ajudaPara3.Text = row[11].ToString();
                        lineParam3.Visible = true;
                        //parametro_3.Visible = true;

                        //nome_parametro_3 = row[10].ToString();
                        //texto_ajuda_3 = row[11].ToString();
                    }
                    else if (contadorParametros == 4)
                    {
                        namePara4.Text = row[10].ToString();
                        ajudaPara4.Text = row[11].ToString();
                        lineParam4.Visible = true;
                        //parametro_4.Visible = true;

                        //nome_parametro_4 = row[10].ToString();
                        //texto_ajuda_4 = row[11].ToString();
                    }
                    else if (contadorParametros == 5)
                    {
                        namePara5.Text = row[10].ToString();
                        ajudaPara5.Text = row[11].ToString();
                        lineParam5.Visible = true;
                        //parametro_5.Visible = true;

                        //nome_parametro_5 = row[10].ToString();
                        //texto_ajuda_5 = row[11].ToString();
                    }
                }
                contadorParametros++;
            }

            //preenche parametro_1 e ajuda_1
            Param_1.Text = nome_parametro_1;
            //TextBoxParametro_1_ajuda.Text = texto_ajuda_1;

            //preenche parametro_2 e ajuda_2
            Param_2.Text = nome_parametro_2;
            //TextBoxParametro_2_ajuda.Text = texto_ajuda_2;

            //preenche parametro_3 e ajuda_3
            Param_3.Text = nome_parametro_3;
            //TextBoxParametro_3_ajuda.Text = texto_ajuda_3;

            //preenche parametro_4 e ajuda_4
            Param_4.Text = nome_parametro_4;
            //TextBoxParametro_4_ajuda.Text = texto_ajuda_4;

            //preenche parametro_5 e ajuda_5
            Param_5.Text = nome_parametro_5;
            //TextBoxParametro_5_ajuda.Text = texto_ajuda_5;

            //Preenche nome do pipeline
            //txtPipeline.Text = pipelineRetornoBanco;

        }
    }
}