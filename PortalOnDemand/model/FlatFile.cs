using PortalOnDemand.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class FlatFile
    {
        Banco bd = new Banco();
        string sql;

        public DataSet Buscar(string arquivo, string usuario)
        {
            if (LoginStart.idProfileUserLogin == 4)
            {
                sql = "SELECT * FROM " + LoginStart.environmentLogin + ".vwControleDeAcesso where NameDataSource is not null and NameDataSource = '" + arquivo + "'";
            }
            else if (LoginStart.idProfileUserLogin == 6)
            {
                sql = "SELECT * FROM " + LoginStart.environmentLogin + ".vwControleDeAcesso where NameDataSource is not null and email = '" + usuario + "' and NameDataSource = '" + arquivo + "'";
            }

            DataSet ds;
            ds = bd.RetornarDataSet(sql);
            return ds;
        }

        public DataSet GetErrorDataSourceFlatFile(string idJob)
        {
            DataSet ds;
            ds = bd.getErrorFlatfile(idJob);
            return ds;
        }
    }
}