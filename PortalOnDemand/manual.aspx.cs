using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace PortalOnDemand
{
    public partial class manual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FilePath = Server.MapPath("Manual do DataOwner v1.4 - Portal ADI On-Demand.pdf");
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }
    }
}