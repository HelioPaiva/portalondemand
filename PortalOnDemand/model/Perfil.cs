using PortalOnDemand.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class Perfil
    {
        Banco bd = new Banco();

        private string sql;
        private string dateRegister;

        public int idPerfil { get; set; }
        public static string namePerfil { get; set; }

        public DataSet Buscar(string tabela)
        {
            if (tabela == "usuario")
            {
                sql = "SELECT * FROM " + LoginStart.environmentLogin + ".perfil_ondemand where id in (4,6) order by 1 asc";
            }
            else
            {
                sql = "SELECT * FROM " + LoginStart.environmentLogin + ".perfil_ondemand where id <> 4 and id <> 6 order by 1 asc";
            }
            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }
    }
}