using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;
using PortalOnDemand.model;
using PortalOnDemand.banco;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;


namespace PortalOnDemand
{
    public partial class upload_dq : System.Web.UI.Page
    {
        Log lg = new Log();
        string extension, nome, caminhoDestino, fileName, idProject, msgErro;
        string nameContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            mensagemOK.Visible = false;
            mensagemErroExtensaoInvalida.Visible = false;
            mensagemErroSemArquivo.Visible = false;
            mensagemErroNaoAutorizado.Visible = false;
            mensagemErroNoCaminho.Visible = false;
            mensagemErroProcessamentoFileFlat.Visible = false;
            ErroDQ.Visible = false;
            mensagemAlertaFlatFile.Visible = false;
            divDropDownDev4prod.Visible = false;
            divSentMigration.Visible = false;
            PnlDatasource.Visible = false;
        }

        [WebMethod]
        public static string submitMigration(int projectid, string listids, string sourceDb, string targetDb)
        {
            Banco conn = new Banco();
            try
            {

                conn.ExecuteMigration("dev.sp_LoadDatasourceAutomation", sourceDb, targetDb, projectid, listids, "", false, "");
                return "Migração executada com sucesso";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        protected void loadDatasource(object sender, EventArgs e)
        {
            bool migrationFullcheked;
            bool canChange;
            divDropDownDev4prod.Visible = true;
            migrationMessage.Visible = true;

            if (checkBoxMigrationFull.Checked == true)
            {
                migrationFullcheked = true;
                canChange = false;
            }
            else
            {
                migrationFullcheked = false;
                canChange = true;
            }
            Banco conn = new Banco();
            string db = SourceDB.Text.Split(',')[0];
            string id = DropDownListProject.SelectedValue;
            DataSet ds = conn.RetornarDataSet("select distinct ds.id , ds.Name from " + db + ".tb_segmentprojectDatasource spd inner join " + db + ".tb_datasource ds on spd.iddatasource = ds.id WHERE spd.idsegmentProject =" + id);
            if (db == "dev")
            {
                migrationMessage.InnerText = "Migração será de desenvolvimento para produção";
            }
            else
            {
                migrationMessage.InnerText = "Migração será de produção para desenvolvimento";
            }
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    CheckBox checkDatasource = new CheckBox();
                    checkDatasource.Text = dr["name"].ToString();
                    checkDatasource.ID = "ckeckDatasource_"+dr["id"].ToString();
                    checkDatasource.Enabled = canChange;
                    checkDatasource.Checked = migrationFullcheked;
                    PnlDatasource.Controls.Add(checkDatasource);
                    PnlDatasource.Controls.Add(new LiteralControl("<br />"));
                }
            }
            PnlDatasource.Visible = true;
           
            divSentMigration.Visible = true;

        }

        protected void move_db(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string dbs = btn.CommandArgument;
            string SourceEnv = dbs.Split(',')[0];
            Banco conn = new Banco(); 
            SourceDB.Text = dbs;

            {
                DataSet ds = conn.RetornarDataSet("select id, concat(id, ' - ', Name) name from " + SourceEnv + ".tb_segmentproject");
                DataTable dt = new DataTable();
                DropDownListProject.DataSource = ds;
                DropDownListProject.DataTextField = "Name";
                DropDownListProject.DataValueField = "id";
                DropDownListProject.DataBind();
                DropDownListProject.Items.Insert(0, new ListItem("Selecione..", "Selecione"));
            }
            migrationMessage.Visible = false;
            divDropDownDev4prod.Visible = true;
        }

        // Parte Antiga
        protected void Button1_Click1(object sender, EventArgs e)
        {
            ExecuteMetodos();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("download.aspx");
        }
        protected bool ExecuteMetodos()
        {
            //1º Passo
            if (ValidaTemArquivo() == false)
            {
                mensagemErroSemArquivo.Visible = true;
                return false;
            }
            //2º Passo
            if (ValidaExtensaoArquivo() == false)
            {
                mensagemErroExtensaoInvalida.Visible = true;
                return false;
            }
            //3º Passo
            /*if (ValidaDataSource() == false)
            {
                mensagemErroNaoAutorizado.Visible = true;
                return false;

            }
            */
            //4º Passo
            if (GetDataSource() == false)
            {
                mensagemErroNoCaminho.Visible = true;
                return false;
            }
            //5º Executa DataQuality
            
            if (DataQualityQ1() == false)
            {
                ErroDQ.Visible = true;
                return false;
            }
            
            mensagemOK.Visible = true;
            return true;
        }
        protected bool ValidaTemArquivo()
        {
            if (FileUpload1.HasFile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool ValidaExtensaoArquivo()
        {
            fileName = Server.HtmlEncode(FileUpload1.FileName);
            extension = System.IO.Path.GetExtension(fileName);
            nome = System.IO.Path.GetFileNameWithoutExtension(fileName);

            if ((extension == ".xls") || (extension == ".xlsx"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool GetDataSource()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["AZURE_STORAGE_CONNECTION_STRING"].ConnectionString;
            if (LoginStart.environmentLogin == "dev")
            {
                //nameContainer = caminhoDestino.Replace("/mnt/datalakeGen2", LoginStart.environmentLogin);
                nameContainer = "dev/inbound/flatfile/";
            }
            else
            {
                nameContainer = "prod/inbound/flatfile/";
                //nameContainer = caminhoDestino.Replace("/mnt/datalakeGen2Prod", LoginStart.environmentLogin);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(nameContainer);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(FileUpload1.FileName);
            try
            {
                using (FileUpload1.PostedFile.InputStream)
                {
                    blockBlob.UploadFromStream(FileUpload1.PostedFile.InputStream);
                }
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;
        }
        protected bool DataQualityQ1()
        {
            string namePipelineDataQualityQ1 = "pl_common_dqautomation_setupdatasource";
            /*string idPipelineRunId*/
            string idUser = LoginStart.idUserLogin.ToString();
            string environment = LoginStart.environmentLogin;
            string caminho = nameContainer + fileName;
            DataFactory df = new DataFactory();
            string idPipeline = df.ExecutaPipelineQualityQ1(namePipelineDataQualityQ1, caminho, idProject, idUser, environment);
            string statusPipeline = df.MonitorPipelineQualityQ1(idPipeline);

            if (statusPipeline == "Failed")
            {
                string erroPipeline = df.ErroExecucaoPipeline(idPipeline);
                var erro = JsonConvert.DeserializeObject<Erro>(erroPipeline);

                //Consulta codigo de erro para apresentar para o usuario
                int cont = 0;
                model.Pipeline pipeline = new model.Pipeline();
                DataSet listErroPipelineTratado = pipeline.ConsultaErroPipelineTratado(erro.ErrorCode.ToString());

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

                LabelErroDQ.Text = "Erro: " + msgErro.ToString() + " aconteceu na validação dos dados";
                string funcao = "setup";
                string status = "ativo";
                lg.InsertLog(status, Session["usuario"].ToString(), fileName, funcao, LoginStart.environmentLogin, idPipeline);
                return false;
            }
            else
            {
                string funcao = "setup";
                string status = "ativo";
                lg.InsertLog(status, Session["usuario"].ToString(), fileName, funcao, LoginStart.environmentLogin, idPipeline);
                return true;
            }
        }
    }
}