using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;
using PortalOnDemand.model;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using System.IO;

namespace PortalOnDemand
{
    public partial class flatfile : Page
    {
        DataFactory df = new DataFactory();
        Log lg = new Log();
        FlatFile flat = new FlatFile();
        string extension, nome, caminhoDestino, fileName, idProject, msgErro;
        string nameContainer;
        string idRunPipeline;
        string descriptionErrorUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionUserId = Session["usuario"] as string;

            if (String.IsNullOrEmpty(sessionUserId))
            {
                Response.Redirect("default.aspx");
            }

            mensagemOK.Visible = false;
            mensagemErroExtensaoInvalida.Visible = false;
            mensagemErroSemArquivo.Visible = false;
            mensagemErroNaoAutorizado.Visible = false;
            mensagemErroNoCaminho.Visible = false;
            mensagemErroProcessamentoFileFlat.Visible = false;
            ErroDQ.Visible = false;
            mensagemAlertaFlatFile.Visible = false;
            alertaInProgress.Visible = false;

            statusEmProgresso.Visible = false;
            statusOK.Visible = false;
            statusErro.Visible = false;
            statusCancelado.Visible = false;
            statusErroMsg.Visible = false;
            statusEmFile.Visible = false;
            alertaInProgress.Visible = false;
            iconMsgErro.Visible = false;
            lblInicioExecucao.Visible = false;

            if (!Page.IsPostBack)
            {
                //GetPipeline();
                painelMonitor.Visible = false;
            }
        }
       
        protected void Button1_Click1(object sender, EventArgs e)
        {

            ExecuteMetodos();
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
            if (ValidaDataSource() == false)
            {
                mensagemErroNaoAutorizado.Visible = true;
                return false;

            }
            //4º Passo
            if (GetDataSource() == false)
            {
                mensagemErroNoCaminho.Visible = true;
                return false;
            }
            //5º Executa DataQuality
            DataQualityQ1();
            /*if (DataQualityQ1() == false)
            {
                ErroDQ.Visible = true;
                return false;
            }
            */
            //mensagemOK.Visible = true;
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

            if ((extension == ".csv") || (extension == ".txt") || (extension == ".xls") || (extension == ".xlsx"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool ValidaDataSource()
        {
            int cont = 0;
            DataSet list = flat.Buscar(nome, LoginStart.nameUserLogin);

            foreach (DataRow row in list.Tables[0].Rows)
            {
                caminhoDestino = row[8].ToString();
                idProject = row[4].ToString();
                cont++;
                break;
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

        protected bool GetDataSource()
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["AZURE_STORAGE_CONNECTION_STRING"].ConnectionString;
            if (LoginStart.environmentLogin == "dev")
            {
               
                nameContainer = caminhoDestino.Replace("/mnt/amsbradls2a4a", LoginStart.environmentLogin);
                //nameContainer = caminhoDestino.Replace("/mnt/datalakeGen2", LoginStart.environmentLogin);
            }
            else
            {
                nameContainer = caminhoDestino.Replace("/mnt/amsbradls2a4a", LoginStart.environmentLogin);
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

        protected void DataQualityQ1()
        {
            string namePipelineDataQualityQ1 = "flatfileQualityQ1";
            string idUser = "0";
            string environment = LoginStart.environmentLogin;
            DataFactory df = new DataFactory();
            string idPipeline = df.ExecutaPipelineQualityQ1(namePipelineDataQualityQ1, nameContainer, idProject, idUser, environment);
            MonitorADF adf = df.MonitorPipeline(idPipeline);
            lblInicioExecucao.Text = DateTime.Parse(adf.RunStart).ToString("yyyy-MM-dd HH:mm:ss");
            lblPipeline.Text = fileName;
            PainelPipeline(adf);
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

            //lblPipeline.Text = adf.PipelineName;
            lblInicioExecucaoVisual.Text = adf.RunStart;
            lblDuracao.Text = adf.DurationInMs;
            lblIdPipeline.Text = adf.RunId;
            string idJob = adf.RunId;
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

                string funcao = "flatfileUpload";
                string status = "ativo";
                lg.InsertLog(status, Session["usuario"].ToString(), lblPipeline.Text, funcao, LoginStart.environmentLogin, idJob);
            }
            else if (statusPipeline == "Cancelled")
            {
                objeto.HoraEntrada = entrada/*.AddHours(3)*/;
                DateTime fimExecucao = DateTime.Parse(adf.RunEnd);
                objeto.HoraSaida = fimExecucao.AddHours(-3);
                lblDuracao.Text = $"{(objeto.TempoPermanencia().ToString(@"hh\:mm\:ss"))}";

                statusEmProgresso.Visible = false;
                statusCancelado.Visible = true;

                string funcao = "flatfileUpload";
                string status = "ativo";
                lg.InsertLog(status, Session["usuario"].ToString(), lblPipeline.Text, funcao, LoginStart.environmentLogin, idJob);
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

                //string erroPipeline = df.ErroExecucaoPipeline(adf.RunId);
                //var erro = JsonConvert.DeserializeObject<Erro>(erroPipeline);
                //msgErro = erro.ErrorCode.ToString() + "-" + erro.Message.Replace("'", "").ToString();

                //int cont = 0;
                //var sb = new System.Text.StringBuilder();
                
                DataSet listErro = flat.GetErrorDataSourceFlatFile(idJob);
                foreach (DataRow row in listErro.Tables[0].Rows)
                {
                    descriptionErrorUser = row[6].ToString();
                    //cont++;
                    break;
                }


                idMsgErro.Text = descriptionErrorUser;
                iconMsgErro.Visible = true;

                statusEmProgresso.Visible = false;
                statusErro.Visible = true;

                string funcao = "flatfileUpload";
                string status = "ativo";
                lg.InsertLog(status, Session["usuario"].ToString(), lblPipeline.Text, funcao, LoginStart.environmentLogin, idJob);

            }

            lblStatus.Visible = false;
            painelMonitor.Visible = true;


        }

    }
}