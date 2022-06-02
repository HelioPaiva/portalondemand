using PortalOnDemand.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class LoginStart
    {
        Banco bd = new Banco();

        private string sql;
        private string dateRegister;

        public static int idUserLogin { get; set; }
        public static string nameUserLogin { get; set; }
        public static int idProfileUserLogin { get; set; }
        public string emailLogin { get; set; }
        public static string environmentLogin { get; set; }

        public void Buscar()
        {
            sql = "SELECT id, email, idPerfil, dataCadastro FROM " + LoginStart.environmentLogin + ".usuario_ondemand where email = '" + nameUserLogin + "'";
            DataSet ds;

            ds = bd.RetornarDataSet(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Array dados = ds.Tables[0].Rows[0].ItemArray;
                idUserLogin = Convert.ToInt16(dados.GetValue(0));
                nameUserLogin = Convert.ToString(dados.GetValue(1));
                idProfileUserLogin = Convert.ToInt16(dados.GetValue(2));
                emailLogin = Convert.ToString(dados.GetValue(3));
            }
        }
        public void Cadastro(Usuario usu)
        {
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "insert into " + LoginStart.environmentLogin + ".[acesso_ondemand] ([idPerfil],[idUsuario],[dataCadastro]) values (" + usu.idProfileUser + "," + usu.idUser + ",'" + dateRegister + "')";
            bd.ExecutarComando(sql);
        }
        public DataSet BuscarRegistro(Usuario usu)
        {
            sql = "SELECT * FROM " + LoginStart.environmentLogin + ".acesso_ondemand where idUsuario = " + usu.idUser + " AND idPerfil = " + usu.idProfileUser + "";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
        public DataSet BuscaAcesso()
        {
            sql = "SELECT * FROM " + LoginStart.environmentLogin + ".[vwControleDeAcesso] where perfil <> 'ADI' order by 1,2,3 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscaAcessoUsuario(string usuarioAcesso)
        {
            sql = "SELECT distinct email,perfil,pipeline,dataCadastro FROM " + LoginStart.environmentLogin + ".[vwControleDeAcesso] where email = '" + usuarioAcesso + "' order by 1,2,3 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
    }
}