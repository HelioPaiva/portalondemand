using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalOnDemand
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string FilePath = Server.MapPath("Template_projeto_v1.3.xlsx");
            
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            /*
            if (FileBuffer != null)
            {
                Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
            */
            DownloadExcel(FileBuffer, FilePath);

        }
        public void DownloadExcel(byte[] buffer, string nameFile)
        {

            // Verify invalid chars on nameArchive parameter:
            var preparedName = new String(nameFile.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());

            if (String.IsNullOrEmpty(preparedName))
                preparedName = "DefaultName";

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}.xlsx", "Template_projeto_v1.3"));
            response.AddHeader("Content-Length", buffer.Length.ToString());
            response.BinaryWrite(buffer);
            response.End();
        }
    }
}