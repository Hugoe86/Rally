using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante
{
    public class Cls_Respuesta_Timbrado
    {
        public string EstatusRespuesta { get; set; }
        public string FolioFiscal { get; set; }
        public string IdValidacion { get; set; }
        public string MensajeValidacion { get; set; }
        public string RutaPDF { get; set; }
        public string Sugerencias3B { get; set; }
        public string Validacion3B { get; set; }
        public string XMLTimbrado { get; set; }
    }
}