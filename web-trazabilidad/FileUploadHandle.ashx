<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;

public class FileUploadHandler : IHttpHandler {

       public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                if (!Directory.Exists(context.Server.MapPath("FotosCorporativos")))
                    Directory.CreateDirectory(context.Server.MapPath("FotosCorporativos"));
                string [] Nombre_Directorio = file.FileName.Split('\\');
                string fname = context.Server.MapPath("FotosCorporativos")+ "\\" + Nombre_Directorio[Nombre_Directorio.Length -1];
                //System.Drawing.Image image = Image.FromStream(file.InputStream);
                //System.Drawing.Bitmap imagen = new System.Drawing.Bitmap(image, new Size(512, 512));

                //imagen.Save(fname);
                using (var fileStrem = File.Create(fname))
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);
                    file.InputStream.CopyTo(fileStrem);
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("File(s) Uploaded Successfully!");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}