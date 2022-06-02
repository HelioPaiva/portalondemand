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
    public partial class editar_pipeline : System.Web.UI.Page
    {
        string idPipelineValor;
        Pipeline pipe = new Pipeline();
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

            idPipelineValor = Request.QueryString["idPipelineValor"];

            mensagemOk.Visible = false;
            mensagemErro.Visible = false;
            mensagemErroConexao.Visible = false;
            mensagemErroPerfil.Visible = false;
            mensagemErroPipelineCadastrado.Visible = false;
            mensagemErroExecucao.Visible = false;
            mensagemErroExecucaoParametro.Visible = false;

            if (!Page.IsPostBack)
            {
                PreencheDados();
                DropDownListQtdParametros.Visible = false;
                param_1.Visible = false;
                param_2.Visible = false;
                param_3.Visible = false;
                param_4.Visible = false;
                param_5.Visible = false;

                if(temParametros == "Sim")
                {
                    DropDownListQtdParametros.Visible = true;
                    if (qtdParametro == 1)
                    {
                        param_1.Visible = true;
                    }
                    else if (qtdParametro == 2)
                    {
                        param_1.Visible = true;
                        param_2.Visible = true;
                    }
                    else if (qtdParametro == 3)
                    {
                        param_1.Visible = true;
                        param_2.Visible = true;
                        param_3.Visible = true;
                    }
                    else if (qtdParametro == 4)
                    {
                        param_1.Visible = true;
                        param_2.Visible = true;
                        param_3.Visible = true;
                        param_4.Visible = true;
                    }
                    else if (qtdParametro == 5)
                    {
                        param_1.Visible = true;
                        param_2.Visible = true;
                        param_3.Visible = true;
                        param_4.Visible = true;
                        param_5.Visible = true;
                    }
                }
            }
        }
        protected void PreencheDados()
        {

            DataSet listValorPipelines = pipe.BuscarRegistro(idPipelineValor);

            int contadorParametros = 1; 
            foreach (DataRow row in listValorPipelines.Tables[0].Rows)
            {
                pipelineRetornoBanco = row[1].ToString();
                idPerfilRetornoBanco = row[2].ToString();
                nomePerfilRetornoBanco = row[3].ToString();
                idProjetoRetornoBanco = row[5].ToString();
                nomeProjetoRetornoBanco = row[6].ToString();
                execucaoManualBanco = row[7].ToString();
                temParametros = row[8].ToString();

                if (temParametros == "Sim")
                {
                    qtdParametro = Convert.ToInt32(row[9]);

                    if (contadorParametros == 1)
                    {
                        nome_parametro_1 = row[10].ToString();
                        texto_ajuda_1 = row[11].ToString();
                    }
                    else if (contadorParametros == 2)
                    {
                        nome_parametro_2 = row[10].ToString();
                        texto_ajuda_2 = row[11].ToString();
                    }
                    else if (contadorParametros == 3)
                    {
                        nome_parametro_3 = row[10].ToString();
                        texto_ajuda_3 = row[11].ToString();
                    }
                    else if (contadorParametros == 4)
                    {
                        nome_parametro_4 = row[10].ToString();
                        texto_ajuda_4 = row[11].ToString();
                    }
                    else if (contadorParametros == 5)
                    {
                        nome_parametro_5 = row[10].ToString();
                        texto_ajuda_5 = row[11].ToString();
                    }
                }
                contadorParametros++;
            }

            //preenche tem paramentro
            //txtTemParametros.Text = temParametros;

            //preenche qtd parametros
            //txtQtdParametros.Text = qtdParametro.ToString();

            //preenche parametro_1 e ajuda_1
            TextBoxParametro_1_nome.Text = nome_parametro_1;
            TextBoxParametro_1_ajuda.Text = texto_ajuda_1;

            //preenche parametro_2 e ajuda_2
            TextBoxParametro_2_nome.Text = nome_parametro_2;
            TextBoxParametro_2_ajuda.Text = texto_ajuda_2;

            //preenche parametro_3 e ajuda_3
            TextBoxParametro_3_nome.Text = nome_parametro_3;
            TextBoxParametro_3_ajuda.Text = texto_ajuda_3;

            //preenche parametro_4 e ajuda_4
            TextBoxParametro_4_nome.Text = nome_parametro_4;
            TextBoxParametro_4_ajuda.Text = texto_ajuda_4;

            //preenche parametro_5 e ajuda_5
            TextBoxParametro_5_nome.Text = nome_parametro_5;
            TextBoxParametro_5_ajuda.Text = texto_ajuda_5;

            //Preenche nome do pipeline
            txtPipeline.Text = pipelineRetornoBanco;

            //Preenche combo perfil com retorno do banco
            int cont = 0;
            Perfil perfil = new Perfil();
            DataSet listPerfil = perfil.Buscar("pipeline");

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

            //Preenche combo Execução Manual
            cont = 0;
            DataSet listExecucaoManual = pipe.Buscar();

            foreach (DataRow row in listExecucaoManual.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                /*DropDownListExecucao.DataSource = listExecucaoManual;
                DropDownListExecucao.DataTextField = "execucaoManual";
                DropDownListExecucao.DataValueField = "execucaoManual";
                DropDownListExecucao.DataBind();*/
                DropDownListExecucao.Items.Insert(0, new ListItem(execucaoManualBanco, execucaoManualBanco));
            }

            //Preenche combo Projeto
            cont = 0;
            DataSet listProjeto = pipe.BuscarProjeto();

            foreach (DataRow row in listProjeto.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                DropDownListProjeto.DataSource = listProjeto;
                DropDownListProjeto.DataTextField = "Name";
                DropDownListProjeto.DataValueField = "id";
                DropDownListProjeto.DataBind();
                DropDownListProjeto.Items.Insert(0, new ListItem(nomeProjetoRetornoBanco, idProjetoRetornoBanco));
            }

            //Preenche combo tem parâmetros
            cont = 0;
            DataSet listTemParametros = pipe.Buscar();

            foreach (DataRow row in listTemParametros.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
                if (temParametros != "")
                {
                    DropDownListParametros.Items.Insert(0, new ListItem(temParametros, temParametros));
                }
                else
                {
                    DropDownListParametros.Items.Insert(0, new ListItem("Não", "Não"));
                }
                
            }

            //Preenche combo qtd parâmetros
            cont = 0;
            DataSet listQtdParametros = pipe.Buscar();

            foreach (DataRow row in listQtdParametros.Tables[0].Rows)
            {
                cont++;
            }

            if (cont > 0)
            {
         
                DropDownListQtdParametros.Items.Insert(0, new ListItem(qtdParametro.ToString(), qtdParametro.ToString()));
            }
            

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            mensagemOk.Visible = false;
            mensagemErro.Visible = false;
            mensagemErroConexao.Visible = false;
            mensagemErroPerfil.Visible = false;
            mensagemErroPipelineCadastrado.Visible = false;


            string pipeline = txtPipeline.Text;
            string p = DropDownListPerfil.SelectedValue.ToString();
            string idProjeto = DropDownListProjeto.SelectedValue.ToString();
            string pipelineParaExecucao = DropDownListExecucao.SelectedValue.ToString();
            mensagemErroExecucao.Visible = false;
            string _temParametro = DropDownListParametros.SelectedValue.ToString();
            string _qtdParametro = DropDownListQtdParametros.SelectedValue.ToString();

            if (p == "Selecione")
            {
                mensagemErroPerfil.Visible = true;
            }
            else if (pipelineParaExecucao == "Selecione")
            {
                mensagemErroExecucao.Visible = true;
            }
            //else if (_temParametro == "Selecione")
            //{
            //    mensagemErroExecucaoParametro.Visible = true;
            //}
            else
            {
                //if (ValidaPipelineCadastrado(pipeline) == false)
                //{
                    pipe.NamePipelineBD = pipeline;
                    pipe.Id = Convert.ToInt16(idPipelineValor);
                    pipe.IdPerfil = Convert.ToInt16(p);
                    pipe.DataFactory = "braz-datalake-adf";
                    pipe.ExecucaoManual = pipelineParaExecucao;
                    pipe.temParametro = _temParametro;
                if (_temParametro != "Sim")
                {
                    pipe.qtdParametro = "0";
                }
                else
                {
                    pipe.qtdParametro = _qtdParametro;
                }
                    
                    if (idProjeto == "Selecione" || idProjeto == null || idProjeto == "")
                    {
                        codProjeto = 0;
                    }
                    else
                    {
                        codProjeto = Convert.ToInt16(idProjeto);
                    }
                    pipe.idProjeto = Convert.ToInt16(codProjeto);
                
                //Captura parametros
                if (_qtdParametro == "1")
                {
                    pipe.nomeParametro_1 = TextBoxParametro_1_nome.Text;
                    pipe.textAjuda_1 = TextBoxParametro_1_ajuda.Text;
                }
                else if (_qtdParametro == "2")
                {
                    pipe.nomeParametro_1 = TextBoxParametro_1_nome.Text;
                    pipe.nomeParametro_2 = TextBoxParametro_2_nome.Text;

                    pipe.textAjuda_1 = TextBoxParametro_1_ajuda.Text;
                    pipe.textAjuda_2 = TextBoxParametro_2_ajuda.Text;
                }
                else if (_qtdParametro == "3")
                {
                    pipe.nomeParametro_1 = TextBoxParametro_1_nome.Text;
                    pipe.nomeParametro_2 = TextBoxParametro_2_nome.Text;
                    pipe.nomeParametro_3 = TextBoxParametro_3_nome.Text;

                    pipe.textAjuda_1 = TextBoxParametro_1_ajuda.Text;
                    pipe.textAjuda_2 = TextBoxParametro_2_ajuda.Text;
                    pipe.textAjuda_3 = TextBoxParametro_3_ajuda.Text;
                }
                else if (_qtdParametro == "4")
                {
                    pipe.nomeParametro_1 = TextBoxParametro_1_nome.Text;
                    pipe.nomeParametro_2 = TextBoxParametro_2_nome.Text;
                    pipe.nomeParametro_3 = TextBoxParametro_3_nome.Text;
                    pipe.nomeParametro_4 = TextBoxParametro_4_nome.Text;

                    pipe.textAjuda_1 = TextBoxParametro_1_ajuda.Text;
                    pipe.textAjuda_2 = TextBoxParametro_2_ajuda.Text;
                    pipe.textAjuda_3 = TextBoxParametro_3_ajuda.Text;
                    pipe.textAjuda_4 = TextBoxParametro_4_ajuda.Text;
                }
                else if (_qtdParametro == "5")
                {
                    pipe.nomeParametro_1 = TextBoxParametro_1_nome.Text;
                    pipe.nomeParametro_2 = TextBoxParametro_2_nome.Text;
                    pipe.nomeParametro_3 = TextBoxParametro_3_nome.Text;
                    pipe.nomeParametro_4 = TextBoxParametro_4_nome.Text;
                    pipe.nomeParametro_5 = TextBoxParametro_5_nome.Text;


                    pipe.textAjuda_1 = TextBoxParametro_1_ajuda.Text;
                    pipe.textAjuda_2 = TextBoxParametro_2_ajuda.Text;
                    pipe.textAjuda_3 = TextBoxParametro_3_ajuda.Text;
                    pipe.textAjuda_4 = TextBoxParametro_4_ajuda.Text;
                    pipe.textAjuda_6 = TextBoxParametro_5_ajuda.Text;
                }



                pipe.Editar(idPipelineValor);
                    Response.Redirect("consulta-pipeline.aspx?status=ok");
                    //mensagemOk.Visible = true;
                //}
                //else
                //{
                //    mensagemErroPipelineCadastrado.Visible = true;
                //}

            }
        }

        protected bool ValidaPipelineCadastrado(string _pipeline)
        {
            int cont = 0;
            pipe.NamePipelineBD = _pipeline;
            DataSet listPipelines = pipe.Buscar();

            foreach (DataRow row in listPipelines.Tables[0].Rows)
            {
                string pp = row[1].ToString();
                if (pp == _pipeline)
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

        protected void DropDownListParametros_SelectedIndexChanged(object sender, EventArgs e)
        {
            string responstaSelecionada = DropDownListParametros.SelectedValue.ToString();

            if (responstaSelecionada == "Sim")
            {
                DropDownListQtdParametros.Visible = true;
            }
            else
            {
                DropDownListQtdParametros.Visible = false;
                param_1.Visible = false;
                param_2.Visible = false;
                param_3.Visible = false;
                param_4.Visible = false;
                param_5.Visible = false;
                DropDownListQtdParametros.SelectedValue = "0";
            }
        }

        protected void DropDownListQtdParametros_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qtdParametroSelecionado = DropDownListQtdParametros.SelectedValue.ToString();
            //int qtdParam = Int32.Parse(qtdParametroSelecionado);

            if (qtdParametroSelecionado == "1")
            {
                param_1.Visible = true;
                param_2.Visible = false;
                param_3.Visible = false;
                param_4.Visible = false;
                param_5.Visible = false;

            }
            else if (qtdParametroSelecionado == "2")
            {
                param_1.Visible = true;
                param_2.Visible = true;
                param_3.Visible = false;
                param_4.Visible = false;
                param_5.Visible = false;
            }
            else if (qtdParametroSelecionado == "3")
            {
                param_1.Visible = true;
                param_2.Visible = true;
                param_3.Visible = true;
                param_4.Visible = false;
                param_5.Visible = false;
            }
            else if (qtdParametroSelecionado == "4")
            {
                param_1.Visible = true;
                param_2.Visible = true;
                param_3.Visible = true;
                param_4.Visible = true;
                param_5.Visible = false;
            }
            else if (qtdParametroSelecionado == "5")
            {
                param_1.Visible = true;
                param_2.Visible = true;
                param_3.Visible = true;
                param_4.Visible = true;
                param_5.Visible = true;
            }
        }


    }
}