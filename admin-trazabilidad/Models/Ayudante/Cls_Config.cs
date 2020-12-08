using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace admin_trazabilidad.Models.Ayudante
{
    public class Cls_Config
    {
        public static string UrlApp
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["UrlApp"];
            }
        }

        public static string FolderUploads
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["FolderUploads"];
            }
        }
    }
}