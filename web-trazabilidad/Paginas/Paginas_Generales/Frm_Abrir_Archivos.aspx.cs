using System;
using System.IO;
using System.Text;

namespace Of_Virtual_RedNatura.Paginas.Paginas_Generales
{
    public partial class Frm_Abrir_Archivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String Ruta_Documento_Servidor = String.Empty;

            if (Request.QueryString["Documento"] != null)
            {
                Ruta_Documento_Servidor = Request.QueryString["Documento"];
                Abrir_Documento_Anexado(Ruta_Documento_Servidor);
            }
        }

        protected void Abrir_Documento_Anexado(String Ruta_Documento_Servidor)
        {
            String Nombre_Archivo = String.Empty;
            String Extensión_Archivo = String.Empty;
            String Tipo_Archivo = String.Empty;

            Nombre_Archivo = Path.GetFileName(Ruta_Documento_Servidor);
            Extensión_Archivo = Path.GetExtension(Ruta_Documento_Servidor);

            if (!String.IsNullOrEmpty(Extensión_Archivo))
                Extensión_Archivo = Extensión_Archivo.Trim().ToLower();

            switch (Extensión_Archivo)
            {
                case ".html": Tipo_Archivo = "text/html"; break;
                case ".htm": Tipo_Archivo = "text/html"; break;
                case ".txt": Tipo_Archivo = "text/plain"; break;
                case ".xml": Tipo_Archivo = "application/xml"; break;

                case ".doc": Tipo_Archivo = "Application/msword"; break;
                case ".docm": Tipo_Archivo = "application/vnd.ms-word.document.macroEnabled.12"; break;
                case ".dotx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.wordprocessingml.template"; break;
                case ".dotm": Tipo_Archivo = "application/vnd.ms-word.template.macroEnabled.12"; break;
                case ".docx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; break;

                case ".xls": Tipo_Archivo = "application/vnd.ms-excel"; break;
                case ".xlsx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; break;
                case ".xlsm": Tipo_Archivo = "application/vnd.ms-excel.sheet.macroEnabled.12"; break;
                case ".xltx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.template"; break;

                case ".pdf": Tipo_Archivo = "Application/pdf"; break;
                case ".zip": Tipo_Archivo = "application/zip"; break;
                case ".rar": Tipo_Archivo = "application/x-rar-compressed"; break;

                case ".ppt": Tipo_Archivo = "application/vnd.ms-powerpoint"; break;
                case ".pot": Tipo_Archivo = "application/vnd.ms-powerpoint"; break;
                case ".pps": Tipo_Archivo = "application/vnd.ms-powerpoint"; break;
                case ".ppa": Tipo_Archivo = "application/vnd.ms-powerpoint"; break;
                case ".pptx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.presentationml.presentation"; break;
                case ".potx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.presentationml.template"; break;
                case ".ppsx": Tipo_Archivo = "application/vnd.openxmlformats-officedocument.presentationml.slideshow"; break;
                case ".ppam": Tipo_Archivo = "application/vnd.ms-powerpoint.addin.macroEnabled.12"; break;
                case ".pptm": Tipo_Archivo = "application/vnd.ms-powerpoint.presentation.macroEnabled.12"; break;
                case ".potm": Tipo_Archivo = "application/vnd.ms-powerpoint.template.macroEnabled.12"; break;
                case ".ppsm": Tipo_Archivo = "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"; break;

                case ".wma": Tipo_Archivo = "audio/x-ms-wma"; break;
                case ".mp4": Tipo_Archivo = "video/mp4"; break;

                default:
                    Tipo_Archivo = "text/plain";
                    break;
            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = Tipo_Archivo;
            Response.AddHeader("content-disposition", "attachment; filename=" + Nombre_Archivo);
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = "UTF-8";
            Response.WriteFile(Ruta_Documento_Servidor);
            Response.Flush();
            Response.End();
        }
    }
}