using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Exportacion
    {
        public string TableName { get; set; }
        public string TableNameAlias { get; set; }
        public string ColumnNames { get; set; }
        public string References { get; set; }
    }
}