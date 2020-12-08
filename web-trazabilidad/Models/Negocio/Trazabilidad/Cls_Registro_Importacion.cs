using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Registro_Importacion
    {
        public Row Row { get; set; }
    }

    public class Row
    {
        public string Token { get; set; }
        public bool IsValid { get; set; }
        public List<Cls_Column_Table> Columns { get; set; }
        public bool IsNewRegister { get; set; }
        public bool IsSaved { get; set; }
        public string Message { get; set; }
    }
}