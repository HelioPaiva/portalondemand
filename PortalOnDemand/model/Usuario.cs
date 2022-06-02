using PortalOnDemand.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PortalOnDemand.model
{
    public class Usuario
    {
        Banco bd = new Banco();

        private string sql;
        private string dateRegister;

        public int idUser { get; set; }
        public string nameUser { get; set; }
        public int idProfileUser { get; set; }
        public string email { get; set; }

        public DataSet Buscar(string tabela)
        {
            if (tabela == "acesso")
            {
                sql = "SELECT u.id, lower(u.email) as email, u.idPerfil, u.dataCadastro,p.perfil FROM " + LoginStart.environmentLogin + ".usuario_ondemand u LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON u.idPerfil = p.id  where p.id <> 4 ORDER BY 3 asc";
            }
            else
            {
                sql = "SELECT u.id, lower(u.email) as email, u.idPerfil, u.dataCadastro,p.perfil FROM " + LoginStart.environmentLogin + ".usuario_ondemand u LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON u.idPerfil = p.id ORDER BY 2 asc";
            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet BuscarUsuario(string usuarioEmail)
        {
            sql = "SELECT u.*,p.perfil FROM " + LoginStart.environmentLogin + ".usuario_ondemand u LEFT JOIN " + LoginStart.environmentLogin + ".perfil_ondemand p ON u.idPerfil = p.id WHERE u.email = '" + usuarioEmail + "' ORDER BY 3 asc";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }



        public void Cadastro()
        {
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "insert into " + LoginStart.environmentLogin + ".[usuario_ondemand] ([email],[idPerfil],[dataCadastro]) values ('" + email + "'," + idProfileUser + ",'" + dateRegister + "')";
            bd.ExecutarComando(sql);

        }
        public DataSet BuscarRegistro()
        {
            sql = "SELECT * FROM " + LoginStart.environmentLogin + ".usuario_ondemand where email = '" + nameUser + "'";
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
        public DataSet BuscarRegistroEmail(string idUsuarioValor)
        {
            if (LoginStart.idProfileUserLogin == 4)
            {
                sql = "SELECT distinct u.email, u.idPerfil, p.perfil FROM " + LoginStart.environmentLogin + ".usuario_ondemand u left join " + LoginStart.environmentLogin + ".perfil_ondemand p on u.idPerfil = p.id  where u.id = " + idUsuarioValor + " order by 1 asc";

            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public void Editar(string idUsuarioAtualizacao)
        {
            int cont = 0;
            //Atualiza cadastro pipeline
            dateRegister = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = "UPDATE " + LoginStart.environmentLogin + ".[usuario_ondemand] set [email] = '" + nameUser + "',[idPerfil] = " + idProfileUser + "  WHERE id = " + idUsuarioAtualizacao + "";
            bd.ExecutarComando(sql);
        }
    }
}