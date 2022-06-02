using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Rest;
using Microsoft.Azure.Management.DataFactory.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PortalOnDemand.model
{
    public class DataFactory
    {
        string tenantID = "12a3af23-a769-4654-847f-958f3d479f4a";
        string applicationId = "28b194a1-8ac1-41a4-a39f-061c6c8af681";
        string authenticationKey = "_.=]9dILT7vW56@arIf]:dA3sHS@E39@";
        string subscriptionId = "a6c9f13d-be73-4227-b485-c7a16289be08";
        string resourceGroup = "brazprdatalakemgd-pr-glb-usw2-rg-001";
        string dataFactoryName = "braz-datalake-adf";
        ServiceClientCredentials cred;
        CreateRunResponse runResponse;

        MonitorADF adf = new MonitorADF();

        public DataFactory()
        {
            // Authenticate and create a data factory management client
            var context = new AuthenticationContext("https://login.windows.net/" + tenantID);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync("https://management.azure.com/", cc).Result;
            cred = new TokenCredentials(result.AccessToken);
        }

        public string ExecutaPipeline(string pipelineName)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            CreateRunResponse runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName).Result.Body;
            return runResponse.RunId;
        }

        public string ValidacaoPipelineEmExecucaoPorName(string pipelineName)
        {
            string runID = null;
            string statusPipeline = null;
            PipelineRun pipelineRuns;

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };

            IList<string> pipelineList = new List<string> { pipelineName };
            IList<RunQueryFilter> moreParams = new List<RunQueryFilter>();
            moreParams.Add(new RunQueryFilter
            {
                Operand = RunQueryFilterOperand.PipelineName,
                OperatorProperty = RunQueryFilterOperator.Equals,
                Values = pipelineList
            });
           
            DateTime now = DateTime.Now;
            DateTime lastWeek = now.AddHours(-1);
            DateTime today = DateTime.Now;

            RunFilterParameters filterParams = new RunFilterParameters(lastWeek, today, null, moreParams, null);
            var requiredRuns = client.PipelineRuns.QueryByFactory(resourceGroup, dataFactoryName, filterParams);
            var enumerator = requiredRuns.Value.GetEnumerator();

            for (bool hasMoreRuns = enumerator.MoveNext(); hasMoreRuns;)
            {
                pipelineRuns = enumerator.Current;
                hasMoreRuns = enumerator.MoveNext();

                if (!hasMoreRuns && pipelineRuns.PipelineName == pipelineName)
                {
                    runID = pipelineRuns.RunId;
                    statusPipeline = pipelineRuns.Status;
                }
            }

            if (statusPipeline == "InProgress")
            {
           
                return runID;
            }
            else 
            {
                return null;
            }
        }

        public MonitorADF MonitorPipeline(string idPipeline)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, idPipeline);
            adf.PipelineName = pipelineRun.PipelineName;
            DateTime inicio = Convert.ToDateTime(pipelineRun.RunStart);
            adf.RunStart = inicio.AddHours(-3).ToString(); //pipelineRun.RunStart.ToString();
            adf.DurationInMs = pipelineRun.DurationInMs.ToString();
            adf.Status = pipelineRun.Status;
            adf.RunEnd = pipelineRun.RunEnd.ToString();
            adf.RunId = idPipeline;
            return adf;
        }

        /*
        public string FimExecucaoPipeline(string idPipeline)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, idPipeline);

            RunFilterParameters filterParams = new RunFilterParameters(DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(10));
            ActivityRunsQueryResponse queryResponse = client.ActivityRuns.QueryByPipelineRun(resourceGroup, dataFactoryName, idPipeline, filterParams);


            if (pipelineRun.Status == "Succeeded")
            {
                var t = queryResponse.Value.First().Output;
                object o = queryResponse.Value.First().Output;
                string s = queryResponse.Value.First().Output.ToString();
            }
            else if (pipelineRun.Status == "Failed")
            {
                var t = queryResponse.Value.First().Error;
                object o = queryResponse.Value.First().Error;
                string s = queryResponse.Value.First().Error.ToString();
            }
            else if (pipelineRun.Status == "Cancelled")
            {
                var t = queryResponse.Value.First().Output;
                object o = queryResponse.Value.First().Output;
                string s = queryResponse.Value.First().Output.ToString();
            }
            else
            {
                Console.WriteLine(queryResponse.Value.First().Output);
            }


            var fim = pipelineRun.RunEnd;
            string finalizacao = fim.ToString();
            return finalizacao;
        }
        */

        public string ErroExecucaoPipeline(string idPipeline)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, idPipeline);
            RunFilterParameters filterParams = new RunFilterParameters(DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(10));
            ActivityRunsQueryResponse queryResponse = client.ActivityRuns.QueryByPipelineRun(resourceGroup, dataFactoryName, idPipeline, filterParams);
            string erro;

            try
            {
                erro = queryResponse.Value.First().Error.ToString();
            }
            catch (Exception)
            {

                erro = "{ErrorCode: '99',message:'Erro de Execucao'}";
            }
            return erro;
        }

        /*Executa Pipeline QualityQ1*/
        public string ExecutaPipelineQualityQ1(string pipelineName, string PathFile, string projectID, /*string idPipelineRunId*/ string idUser, string Environment)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "Environment", Environment},
                { "ProjectId", projectID},
                { "PathFile", PathFile },
                { "IdUser", idUser },
                
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }

        /*Executa Pipeline Provisório VDE*/
        public string ExecutaPipelineVDE(string pipelineName, string periodo, bool valor, string Environment)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            
            string projectID = "67";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "par_Environment", "prod"},
                { "par_id_project", projectID},
                { "par_dataref", periodo },
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }


        /*Executa Pipelines com parâmtros*/
        // 1 parametros
        public string ExecutaPipelineComParametros_1(string pipelineName, string name_1, string valor_1, bool valor, string Environment)
        {

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { name_1, valor_1},
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }

        // 2 parametros
        public string ExecutaPipelineComParametros_2(string pipelineName, string name_1, string name_2, string valor_1, string valor_2, bool valor, string Environment)
        {

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { name_1, valor_1},
                { name_2, valor_2},
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }

        // 3 parametros
        public string ExecutaPipelineComParametros_3(string pipelineName, string name_1, string name_2, string name_3, string valor_1, string valor_2, string valor_3, bool valor, string Environment)
        {

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { name_1, valor_1},
                { name_2, valor_2},
                { name_3, valor_3 },
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }


        // 4 parametros
        public string ExecutaPipelineComParametros_4(string pipelineName, string name_1, string name_2, string name_3, string name_4, string valor_1, string valor_2, string valor_3, string valor_4, bool valor, string Environment)
        {

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { name_1, valor_1},
                { name_2, valor_2},
                { name_3, valor_3 },
                { name_4, valor_4 },
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }


        // 5 parametros
        public string ExecutaPipelineComParametros_5(string pipelineName, string name_1, string name_2, string name_3, string name_4, string name_5, string valor_1, string valor_2, string valor_3, string valor_4, string valor_5, bool valor, string Environment)
        {

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {

                { name_1, valor_1},
                { name_2, valor_2},
                { name_3, valor_3 },
                { name_4, valor_4 },
                { name_5, valor_5 },
            };

            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
            return runResponse.RunId;
        }



        public string MonitorPipelineQualityQ1(string idPipeline)
        {
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };
            PipelineRun pipelineRun;
            //pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, idPipeline);
            //return pipelineRun.Status;
            while (true)
            {
                pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, idPipeline);
                if (pipelineRun.Status == "InProgress")
                    System.Threading.Thread.Sleep(10000);
                else
                    break;
            }

            RunFilterParameters filterParams = new RunFilterParameters(DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(10));
            ActivityRunsQueryResponse queryResponse = client.ActivityRuns.QueryByPipelineRun(resourceGroup, dataFactoryName, idPipeline, filterParams);
            return pipelineRun.Status;
        }

        public string MonitorDataFactory(List<string> listaNamePipeline, DateTime lastWeek, DateTime today)
        {
            PipelineRun pipelineRuns;

            List<object> retorno = new List<object>();

            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };

            dynamic outputValues = new JObject();
            JObject datalhes;
            outputValues.Detalhes = new JArray();

            foreach (var item in listaNamePipeline)
            {
                IList<string> pipelineList = new List<string> { item };
                IList<RunQueryFilter> moreParams = new List<RunQueryFilter>();
               
                moreParams.Add(new RunQueryFilter
                {
                    Operand = RunQueryFilterOperand.PipelineName,
                    OperatorProperty = RunQueryFilterOperator.Equals,
                    Values = pipelineList
                });

               

                RunFilterParameters filterParams = new RunFilterParameters(lastWeek, today, null, moreParams, null);
                //PipelineRunsQueryResponse requiredRuns = client.PipelineRuns.QueryByFactory(resourceGroup, dataFactoryName, filterParams);                
                var requiredRuns = client.PipelineRuns.QueryByFactory(resourceGroup, dataFactoryName, filterParams);
                var enumerator = requiredRuns.Value.GetEnumerator();

                string mensagemErro;

                for (bool hasMoreRuns = enumerator.MoveNext(); hasMoreRuns;)
                {
                    pipelineRuns = enumerator.Current;
                    hasMoreRuns = enumerator.MoveNext();

                    if (pipelineRuns.Status == "Failed")
                    {
                        string[] texto = pipelineRuns.Message.ToString().Split(':');
                        mensagemErro = texto[0];
                    }
                    else
                    {
                        mensagemErro = "";
                    }



                    DateTime inicio = Convert.ToDateTime(pipelineRuns.RunStart);

                    string trigger = pipelineRuns.InvokedBy.InvokedByType.ToString();


                    datalhes = JObject.Parse("{ \"PipelineName\" : \"" + pipelineRuns.PipelineName +
                                            "\",  \"PipelineInicio\" : \"" + inicio.AddHours(-3).ToString() +
                                            "\",  \"PipelineDuracao\" : \"" + pipelineRuns.DurationInMs +
                                            "\",  \"PipelineTrigger\" : \"" + trigger +
                                            "\",  \"PipelineStatus\" : \"" + pipelineRuns.Status +
                                            "\",  \"PipelineErro\" : \"" + mensagemErro +
                                             "\",  \"PipelineID\" : \"" + pipelineRuns.RunId +
                                             "\" }");

                    outputValues.Detalhes.Add(datalhes);

                }
               
            }
            string t = Convert.ToString(outputValues);
            return t;
        }
    }
}