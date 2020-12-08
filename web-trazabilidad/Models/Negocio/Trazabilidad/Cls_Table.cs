using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Table
    {
        public string TableName { get; set; }
        public bool IsTableReference { get; set; }
        public string Filter { get; set; }
        public string RowsJson { get; set; }
        public int Empresa_ID { get; set; }
        public int Sucursal_ID { get; set; }
        public string Usuario { get; set; }
    }
}