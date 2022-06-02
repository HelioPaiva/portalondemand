using Newtonsoft.Json;
using PortalOnDemand.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace PortalOnDemand.model
{
    public class Pipeline
    {
        Banco bd = new Banco();
        DataFactory df = new DataFactory();
        MonitorADF adf = new MonitorADF();
        Log lg = new Log();

        string statusPipelineEmExecucao, msgErro;

        private string sql;
        private string dateRegister;

        public int Id { get; set; }
        public string NamePipelineBD { get; set; }
        public int IdPerfil { get; set; }
        public string DataFactory { get; set; }
        public DateTime DataCadastro { get; set; }
        public int idProjeto { get; set; }
        public string ExecucaoManual { get; set; }

        public string temParametro { get; set; }
        public string qtdParametro { get; set; }

        public string nomeParametro_1 { get; set; }
        public string nomeParametro_2 { get; set; }
        public string nomeParametro_3 { get; set; }
        public string nomeParametro_4 { get; set; }
        public string nomeParametro_5 { get; set; }

        public string textAjuda_1 { get; set; }
        public string textAjuda_2 { get; set; }
        public string textAjuda_3 { get; set; }
        public string textAjuda_4 { get; set; }
        public string textAjuda_6 { get; set; }

        public DataSet Buscar()
        {
            if (LoginStart.idProfileUserLogin == 4)
            {
                ////Usuário ADI tem acesso a todos os pipelines cadastrado
                sql = "SELECT pipe.id , lower(pipe.pipeline) as pipeline, isnull(p.perfil,'-') as perfil, pipe.dataCadastro, pipe.idProjeto, sp.Name, pipe.execucaoManual FROM " + LoginStart.environmentLogin + ".pipeline_ondemand pipe LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON pipe.idPerfil = p.id LEFT JOIN " + LoginStart.environmentLogin + ".[tb_segmentproject] sp ON pipe.idProjeto = sp.Id ORDER BY 2 asc";
            }
            else if (LoginStart.idProfileUserLogin == 6)
            {
                sql = "select distinct lower(p.pipeline) as pipeline, p.execucaoManual  from " + LoginStart.environmentLogin + ".acesso_ondemand a LEFT JOIN " + LoginStart.environmentLogin + ".[pipeline_ondemand] p ON a.idPerfil = p.idPerfil LEFT JOIN " + LoginStart.environmentLogin + ".[usuario_ondemand] u ON a.idUsuario = u.id where u.email = '" + LoginStart.nameUserLogin + "' ORDER BY 2 asc";
            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscarMobile()
        {
            sql = "select distinct lower(p.pipeline) as pipeline" +
                  ",p.execucaoManual  " +
                  "from " + LoginStart.environmentLogin + ".acesso_ondemand a " +
                  "LEFT JOIN " + LoginStart.environmentLogin + ".[pipeline_ondemand] p ON a.idPerfil = p.idPerfil " +
                  "LEFT JOIN " + LoginStart.environmentLogin + ".[usuario_ondemand] u ON a.idUsuario = u.id " +
                  "where u.email = '" + LoginStart.nameUserLogin + "' AND p.pipeline in ('pl_master - origenes_op_fechamento','MESTRE_PEC_VENDAS','ORIGENES_FATOS')  ORDER BY 2 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscarPipeline(string nomePipeline)
        {
            sql = "SELECT pipe.id , pipe.pipeline, isnull(p.perfil,'-') as perfil, pipe.dataCadastro, pipe.idProjeto, sp.Name, pipe.execucaoManual FROM " + LoginStart.environmentLogin + ".pipeline_ondemand pipe LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON pipe.idPerfil = p.id LEFT JOIN " + LoginStart.environmentLogin + ".[tb_segmentproject] sp ON pipe.idProjeto = sp.Id WHERE pipe.pipeline = '" + nomePipeline + "' ORDER BY 3 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }


        public DataSet BuscarPipelineExecucao()
        {
            if (LoginStart.idProfileUserLogin == 4)
            {
                //Usuário ADI tem acesso a todos os pipelines cadastrado
                sql = "SELECT pipe.id , lower(pipe.pipeline) as pipeline, isnull(p.perfil,'-') as perfil, pipe.dataCadastro, pipe.idProjeto, sp.Name, pipe.execucaoManual FROM " + LoginStart.environmentLogin + ".pipeline_ondemand pipe LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON pipe.idPerfil = p.id LEFT JOIN " + LoginStart.environmentLogin + ".[tb_segmentproject] sp ON pipe.idProjeto = sp.Id ORDER BY 2 asc";
            }
            else if (LoginStart.idProfileUserLogin == 6)
            {
                sql = "select distinct lower(p.pipeline) as pipeline, p.execucaoManual  from " + LoginStart.environmentLogin + ".acesso_ondemand a LEFT JOIN " + LoginStart.environmentLogin + ".[pipeline_ondemand] p ON a.idPerfil = p.idPerfil LEFT JOIN " + LoginStart.environmentLogin + ".[usuario_ondemand] u ON a.idUsuario = u.id where u.email = '" + LoginStart.nameUserLogin + "' and p.execucaoManual = 'Sim' ORDER BY 1 asc";
            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
        public DataSet ConsultaErroPipelineTratado(string codeErro)
        {
            string codeErroString = codeErro;
            int codeErroBD;
            bool codeErroNumeric = int.TryParse(codeErroString, out _);

            //Verificando se é um número
            if (codeErroNumeric)
            {
                codeErroBD = Convert.ToInt16(codeErro);
            }
            else
            {
                codeErroBD = Convert.ToInt16("99");
            }

            sql = "SELECT * FROM " + LoginStart.environmentLogin + ".TB_DATAFACTORY_ERRORS where CODE_ERROR = '" + codeErroBD + "'";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
        public DataSet BuscarRegistro(string idPipelineValor)
        {
            if (LoginStart.idProfileUserLogin == 4)
            {
                sql = "SELECT pipe.id , pipe.pipeline, p.id as idPerfil, isnull(p.perfil,'-') as perfil, pipe.dataCadastro, pipe.idProjeto, sp.Name, pipe.execucaoManual,pipe.temParametros,pipe.qtdParametro,par.nome_parametro,par.texto_ajuda FROM " + LoginStart.environmentLogin + ".pipeline_ondemand pipe LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON pipe.idPerfil = p.id LEFT JOIN " + LoginStart.environmentLogin + ".[tb_segmentproject] sp ON pipe.idProjeto = sp.Id LEFT JOIN " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] par ON pipe.pipeline = par.pipeline where pipe.id = " + idPipelineValor + " ORDER BY 1 asc";
            }
            else if (LoginStart.idProfileUserLogin == 6)
            {
                sql = "SELECT distinct pipeline, execucaoManual FROM " + LoginStart.environmentLogin + ".pipeline_ondemand where idPerfil = " + LoginStart.idProfileUserLogin + " order by 1 asc";
            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscaParametrosDoPipeline(string namePipelineValor)
        {
            sql = "SELECT pipe.id , pipe.pipeline, p.id as idPerfil, isnull(p.perfil,'-') as perfil, pipe.dataCadastro, pipe.idProjeto, sp.Name, pipe.execucaoManual,pipe.temParametros,isnull(pipe.qtdParametro,0) as qtdParametro,par.nome_parametro,par.texto_ajuda FROM " + LoginStart.environmentLogin + ".pipeline_ondemand pipe LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON pipe.idPerfil = p.id LEFT JOIN " + LoginStart.environmentLogin + ".[tb_segmentproject] sp ON pipe.idProjeto = sp.Id LEFT JOIN " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] par ON pipe.pipeline = par.pipeline where pipe.pipeline = '" + namePipelineValor + "' ORDER BY 1 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }


        public DataSet BuscarProjeto()
        {
            sql = " SELECT id, Name FROM " + LoginStart.environmentLogin + ".tb_segmentproject order by 2 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public void Cadastro()
        {
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_ondemand] ([pipeline],[idPerfil],[dataFactory],[dataCadastro],[idProjeto],[dataAtualizacao],[execucaoManual],[temParametros],[qtdParametro]) values ('" + NamePipelineBD + "'," + IdPerfil + ",'" + DataFactory + "','" + dateRegister + "'," + idProjeto + ",'" + dateRegister + "','" + ExecucaoManual + "','" + temParametro + "'," + qtdParametro + ")";
            bd.ExecutarComando(sql);

            if (idProjeto > 0)
            {
                CadastroLinkTablePipelineDataQuality();
            }

            if (temParametro == "Sim")
            {
                CadastroPipelineParametro();
            }
        }
        public void CadastroLinkTablePipelineDataQuality()
        {
            Thread.Sleep(3000);
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_project_datasource_ondemand] select 2 as id ,pipe.id as idPipeline,pipe.idProjeto as idProject,ISNULL(spds.IdDataSource,0) as idDataSource,1 as status,getdate() as dataCadastro from " + LoginStart.environmentLogin + " .[pipeline_ondemand] pipe left join " + LoginStart.environmentLogin + ".[tb_segmentprojectdatasource] spds ON pipe.idProjeto = spds.idSegmentProject where pipe.idProjeto = " + idProjeto + " and pipe.id =  (select id from " + LoginStart.environmentLogin + ".[pipeline_ondemand] where pipeline = '" + NamePipelineBD + "')";
            bd.ExecutarComando(sql);
        }

        public void CadastroPipelineParametro()
        {
            Thread.Sleep(3000);
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (qtdParametro == "1")
            {
                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_1 + "','" + textAjuda_1 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);
            }
            else if (qtdParametro == "2")
            {
                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_1 + "','" + textAjuda_1 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_2 + "','" + textAjuda_2 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);
            }
            else if (qtdParametro == "3")
            {
                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_1 + "','" + textAjuda_1 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_2 + "','" + textAjuda_2 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_3 + "','" + textAjuda_3 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);
            }
            else if (qtdParametro == "4")
            {
                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_1 + "','" + textAjuda_1 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_2 + "','" + textAjuda_2 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_3 + "','" + textAjuda_3 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_4 + "','" + textAjuda_4 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);
            }
            else if (qtdParametro == "5")
            {
                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_1 + "','" + textAjuda_1 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_2 + "','" + textAjuda_2 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_3 + "','" + textAjuda_3 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_4 + "','" + textAjuda_4 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);

                sql = "insert into " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] ([pipeline],[nome_parametro],[texto_ajuda],[dataCadastro]) values('" + NamePipelineBD + "','" + nomeParametro_5 + "','" + textAjuda_6 + "','" + dateRegister + "')";
                bd.ExecutarComando(sql);
            }

        }

        public void Editar(string idPepilineAtualizacao)
        {
            int cont = 0;
            //Atualiza cadastro pipeline
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "UPDATE " + LoginStart.environmentLogin + ".[pipeline_ondemand] set [pipeline] = '" + NamePipelineBD + "',[idPerfil] = " + IdPerfil + ",[dataAtualizacao] = '" + dateRegister + "',[idProjeto] = " + idProjeto + " ,[execucaoManual] = '" + ExecucaoManual + "', [temParametros] = '" + temParametro + "',[qtdParametro] = '" + qtdParametro + "' WHERE id = " + idPepilineAtualizacao + "";
            bd.ExecutarComando(sql);

            Thread.Sleep(3000);

            if (temParametro == "Sim")
            {
                sql = "delete from " + LoginStart.environmentLogin + ".[pipeline_parametros_ondemand] WHERE [pipeline] = '" + NamePipelineBD + "'";
                bd.ExecutarComando(sql);
            }

            CadastroPipelineParametro();

            //Verificar se o pipeline já esta vinculado ao projeto
            sql = "select * from " + LoginStart.environmentLogin + ".[pipeline_project_datasource_ondemand] where idPipeline = '" + idPepilineAtualizacao + "'";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cont++;
                break;
            }
            if (cont > 0)
            {
                sql = "Delete from " + LoginStart.environmentLogin + ".[pipeline_project_datasource_ondemand] where idPipeline = " + idPepilineAtualizacao + "";
                bd.ExecutarComando(sql);
                CadastroLinkTablePipelineDataQuality();
            }
            else
            {
                CadastroLinkTablePipelineDataQuality();
            }
        }

        public DataSet ConsultaStatusPipelineFinalizado()
        {
            sql = "SELECT * FROM " + LoginStart.environmentLogin + ".log_ondemand where statusPipeline = 'InProgress'";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        //Compara Status Pipeline DataFactory com Status Banco de Dados
        public void AtualizaipelineEmExecucaoFinalizado()
        {
            DataSet listPipelineFinalizado = ConsultaStatusPipelineFinalizado();
            //Verifica status no log banco de dados
            foreach (DataRow row in listPipelineFinalizado.Tables[0].Rows)
            {
                string idPipelineExecucao = row[6].ToString();
                DateTime inicioDaExecucao = DateTime.Parse(row[3].ToString());
                VerificaStatusDataFactory(idPipelineExecucao);

            }
        }

        private void VerificaStatusDataFactory(string _pipeline)
        {
            df.MonitorPipeline(_pipeline);

            statusPipelineEmExecucao = adf.Status;

            if (statusPipelineEmExecucao != "InProgress")
            {
                if (statusPipelineEmExecucao == "Failed")
                {
                    string erroPipeline = df.ErroExecucaoPipeline(_pipeline);


                    var erro = JsonConvert.DeserializeObject<Erro>(erroPipeline);
                    //Consulta codigo de erro para apresentar para o usuario
                    int cont = 0;

                    DataSet listErroPipelineTratado = ConsultaErroPipelineTratado(erro.ErrorCode.ToString());

                    //Verifica status no log banco de dados
                    foreach (DataRow row in listErroPipelineTratado.Tables[0].Rows)
                    {
                        string msgErroTratado = row[2].ToString();
                        msgErro = erro.ErrorCode.ToString() + "-" + msgErroTratado.ToString();
                        cont++;
                    }
                    if (cont == 0)
                    {
                        msgErro = erro.ErrorCode.ToString() + "-" + erro.Message.Replace("'", "").ToString();
                    }

                    lg.AtualizarStatusLogPipeline(LoginStart.environmentLogin, statusPipelineEmExecucao.ToString(), _pipeline, msgErro);
                }
                else
                {
                    lg.AtualizarStatusLogPipeline(LoginStart.environmentLogin, statusPipelineEmExecucao.ToString(), _pipeline, null);
                }
            }
        }

    }
}