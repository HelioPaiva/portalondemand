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
    public partial class cadastro_pipeline : System.Web.UI.Page
    {
        Pipeline pipe = new Pipeline();
        int codProjeto;

        public object TextBoxParametro_5_ajuda { get; private set; }

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
            mensagemErroPipelineCadastrado.Visible = false;
            mensagemErroExecucao.Visible = false;
            mensagemErroExecucaoParametro.Visible = false;

            if (!Page.IsPostBack)
            {
                GetPerfil();
                GetProjeto();
                qtdParametros.Visible = false;
                param_1.Visible = false;
                param_2.Visible = false;
                param_3.Visible = false;
                param_4.Visible = false;
                param_5.Visible = false;


            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string pipeline = txtPipeline.Text;
            string p = DropDownListPerfil.SelectedValue.ToString();
            string idProjeto = DropDownListProjeto.SelectedValue.ToString();
            string pipelineParaExecucao = DropDownListExecucao.SelectedValue.ToString();
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
            else if (_temParametro == "Selecione")
            {
                mensagemErroExecucaoParametro.Visible = true;
            }
            else
            {
                if (ValidaPipelineCadastrado(pipeline) == false)
                {
                    pipe.NamePipelineBD = pipeline;
                    pipe.IdPerfil = Convert.ToInt16(p);
                    pipe.DataFactory = "braz-datalake-adf";
                    pipe.ExecucaoManual = pipelineParaExecucao;
                    if (idProjeto == "Selecione")
                    {
                        codProjeto = 0;
                    }
                    else
                    {
                        codProjeto = Convert.ToInt16(idProjeto);
                    }

                    if (_temParametro == "Não")
                    {
                        _qtdParametro = "0";
                    }
                    else
                    {
                        if(_qtdParametro == "1")
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
                            pipe.textAjuda_6 = TextBoxParametro_6_ajuda.Text;
                        }

                    }
                    

                    pipe.idProjeto = Convert.ToInt16(codProjeto);
                    pipe.temParametro = _temParametro;
                    pipe.qtdParametro = _qtdParametro;
                    pipe.Cadastro();
                    Response.Redirect("consulta-pipeline.aspx?status=ok");
                    //mensagemOk.Visible = true;
                }
                else
                {
                    mensagemErroPipelineCadastrado.Visible = true;
                }

            }
        }
        protected void GetPerfil()
        {
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
                DropDownListPerfil.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
            }
        }
        protected void GetProjeto()
        {
            int cont = 0;
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
                DropDownListProjeto.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
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
        protected bool ValidaPorjetoDQCadastrado(string _pipeline)
        {
            int cont = 0;
            pipe.NamePipelineBD = _pipeline;
            DataSet listPipelines = pipe.Buscar();

            foreach (DataRow row in listPipelines.Tables[0].Rows)
            {
                string pp = row[0].ToString();
                if (pp == _pipeline)
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

        protected void DropDownListParametros_SelectedIndexChanged(object sender, EventArgs e)
        {
            string responstaSelecionada = DropDownListParametros.SelectedValue.ToString();

            if (responstaSelecionada == "Sim")
            {
                qtdParametros.Visible = true;
            }
            else
            {
                qtdParametros.Visible = false;
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