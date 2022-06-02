using PortalOnDemand.banco;
using System;
using System.Data;

namespace PortalOnDemand.model
{
    public class Log
    {
        Banco bd = new Banco();
        private string sql;
        string usuarioLog;

        //Inserção de logs
        public void InsertLog(string status, string usuarioLog, string p, string funcao, string amb, string idPipeline)
        {
            string localDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = "insert into " + amb + ".[log_ondemand] ([usuario],[nome],[dataExecucao],status,funcao,idPipeline) values ('" + usuarioLog + "','" + p + "','" + localDate + "','" + status + "','" + funcao + "','" + idPipeline + "')";
            bd.ExecutarComando(sql);
        }

        public void AtualizarStatusLogPipeline(string amb, string statusPipeline, string idPipeline, string erroPipeline)
        {
            string sql = "update " + amb + ". [log_ondemand] set statusPipeline = '" + statusPipeline + "', erroPipeline = '" + erroPipeline + "' where idPipeline = '" + idPipeline + "'";
            bd.ExecutarComando(sql);
        }
        public DataSet Buscar()
        {
            sql = "SELECT l.id, l.usuario,l.nome,DATEADD(HOUR, -3, l.dataExecucao) as dataExecucao,l.funcao,l.[idPipeline] FROM " + LoginStart.environmentLogin + ".[log_ondemand] l where l.dataExecucao >= DATEADD(DAY, -30, getdate())  ORDER BY 1 DESC";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscarUsuarioLog(string usuarioLog)
        {
            sql = "SELECT l.id, l.usuario,l.nome,DATEADD(HOUR, -3, l.dataExecucao) as dataExecucao,l.funcao,l.[idPipeline] FROM " + LoginStart.environmentLogin + ".[log_ondemand] l where l.dataExecucao >= DATEADD(DAY, -30, getdate()) and l.usuario = '" + usuarioLog + "'  ORDER BY 1 DESC";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscarUsuario()
        {

            sql = " SELECT idPipeline, usuario FROM "+ LoginStart.environmentLogin +".[log_ondemand]";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

    }
}