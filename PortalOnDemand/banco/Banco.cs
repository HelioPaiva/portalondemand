using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PortalOnDemand.model;

namespace PortalOnDemand.banco
{
    public class Banco
    {
        string conexao = "Server=tcp:" + ConfigurationManager.AppSettings["AppDBHostName"] + ",1433;Initial Catalog=" + ConfigurationManager.AppSettings["AppDBName"] + ";Persist Security Info=False;User ID=" + ConfigurationManager.AppSettings["AppDBUserName"] + ";Password=" + ConfigurationManager.AppSettings["AppDBPassword"] + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        //Abrir Conexão com o Banco de Dados
        private SqlConnection AbrirBanco()
        {
            SqlConnection cn = new SqlConnection(conexao);
            cn.Open();
            return cn;
        }

        //Fechar Conexão como o Banco de Dados
        public void FecharBanco(SqlConnection cn)
        {
            cn.Close();
        }

        public void ExecuteMigration(string procName,
            string sourceDb,
            string targetDb,
            int projectid,
            string listids,
            string ExecutionId,
            bool ExibeSql,
            string LogUser)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand(procName,cn );

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MigrationSource", SqlDbType.NVarChar).Value = sourceDb;
                cmd.Parameters.Add("@MigrationTarget", SqlDbType.NVarChar).Value = targetDb;
                cmd.Parameters.Add("@ProjectIdSource", SqlDbType.Int).Value = projectid;
                cmd.Parameters.Add("@DataSourceIds", SqlDbType.NVarChar).Value = listids;
                cmd.Parameters.Add("@ExecutionId", SqlDbType.NVarChar).Value = ExecutionId;
                cmd.Parameters.Add("@ExibeSql", SqlDbType.Bit).Value = ExibeSql;
                cmd.Parameters.Add("@LogUser", SqlDbType.NVarChar).Value = LogUser;


                cmd.Connection = cn;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }

        //Executa procedure get_erro_flatfile
        public DataSet getErrorFlatfile(string idJob)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand(LoginStart.environmentLogin + ".sp_getMessageFlatfile", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idJob", SqlDbType.NVarChar).Value = idJob;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }

        //Execução de comando insert, update e delete
        public void ExecutarComando(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }

        //Retorna um objeto DataSet com dados da tabela consultada
        public DataSet RetornarDataSet(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }

        //retornar um DataReader
        public SqlDataReader RetornarDataReader(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }

        //Classe para retornar um Id Numérico
        public int RetornarIdNumerico(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();
                int codigo;
                if (dr.Read())
                    codigo = Convert.ToInt16(dr[0]) + 1;
                else
                    codigo = 1;
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }
    }
}